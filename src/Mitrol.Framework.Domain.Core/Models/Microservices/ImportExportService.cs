// ***********************************************************************
// Assembly         : Mitrol.Framework.Domain.Core
// Author           : giovanni.dantonio
// Created          : 09-17-2021
//
// Last Modified By : giovanni.dantonio
// Last Modified On : 10-08-2021
// ***********************************************************************
// <copyright file="ImportExportService.cs" company="Mitrol S.r.l.">
//     Copyright © 2021
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Bus;
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Licensing;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.SignalR.Gateway;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    public interface IImportExportDataProvider : IApplicationService
    {
        Result<List<IExportObject>> GetObjectIdentifiersToExport(object Filter, CumulativeCancellationToken token);

        Result<List<IExportObject>> ExportToJsonService(object Filter, CumulativeCancellationToken token, StatusEvent statusEvent);

        Result<long> ImportFromJsonService(object objectToImport, CumulativeCancellationToken token, StatusEvent statusEvent, IUnitOfWork unitOfWork, IUnitOfWorkFactory unitOfFactory);

        Result<long> InizializeImport(object objectToImport, CumulativeCancellationToken token, IUnitOfWork unitOfWork, IUnitOfWorkFactory unitOfFactory);

        Task<Result<long>> FinalizeImport(object objectToImport, CumulativeCancellationToken token, IUnitOfWork unitOfWork, IUnitOfWorkFactory unitOfFactory);
    }

    public interface IImportExportService : IApplicationService
    {
        Result<TaskInfo> ExportToJsonAsync(ImportExportFilter filter, string extension, List<IExportObject> objectsToExport); 

        Result<TaskInfo> ImportFromJsonAsync(string json, string validation, bool validateFileContent);

        Result<TaskInfo> ImportFromJsonAsync(ImportExportFilter filter, string extension, bool validateFileContent); 

        Task<Result> AbortImportExportOperations(List<string> guids);
        
        Result ValidatePath(string path, IEnumerable<ImportExportPath> pathsEnabled);

        Result ValidateFileSystemContent(ImportExportFilter filter, string extension, List<IExportObject> masterObjectsToExport);

        public Result<List<IExportObject>> GetObjectIdentifiersToExport(ImportExportFilter filter);

    }

    /// <summary>
    /// Class ImportExportService.
    /// Implements the <see cref="Mitrol.Framework.Domain.Core.Models.Microservices.InternalBaseService" />
    /// Implements the <see cref="Mitrol.Framework.Domain.Interfaces.IImportExportService" />
    /// </summary>
    /// <seealso cref="Mitrol.Framework.Domain.Core.Models.Microservices.InternalBaseService" />
    /// <seealso cref="Mitrol.Framework.Domain.Interfaces.IImportExportService" />
    public class ImportExportService : InternalBaseService, IImportExportService
    {
       
        public ICancellableBackgroundTaskQueue CancellableBackgroundTaskQueue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportService"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public ImportExportService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
            CancellableBackgroundTaskQueue = serviceFactory.GetService<ICancellableBackgroundTaskQueue>();
        }

        /// <summary>
        /// Gets the service required.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>IApplicationService.</returns>
        private IImportExportDataProvider ResolveImportExportDataProvider(IServiceFactory serviceFactory, Type serviceType)
        {
            if (serviceFactory.GetService(serviceType) is not IImportExportDataProvider applicationService)
            {
                var resolverType = typeof(IResolver<>).MakeGenericType(serviceType);
                var resolver = serviceFactory.GetService(resolverType) as IResolver;
                applicationService = resolver.Resolve() as IImportExportDataProvider;
            }
            applicationService?.SetSession(UserSession);
            return applicationService;
        }

        // Fornisce il tipo di oggetto e l'attributo neessario all'import in base al nome passato
        /// <summary>
        /// Gets the type of the object name.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns>KeyValuePair&lt;Type, ImportExportAttribute&gt;.</returns>
        private static KeyValuePair<Type, ImportExportAttribute> RetrieveTypeWithImportAttributeByName(string objectName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                //cerco il tipo di oggetto da utilizzare per Import/Export
                var objectFound = GetObjectTypeInAssembly(assembly, objectName);
                if (!objectFound.Equals(default(KeyValuePair<Type, ImportExportAttribute>)))
                    return objectFound;
            }
            //Oggetto non trovato
            return default;
        }

        //Recupera il type dell'oggetto da importare, all'interno dell'assembly, che ha come attributo [objectName]
        /// <summary>
        /// Gets the object type to import.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="objectName">Name of the object.</param>
        /// <returns>IEnumerable&lt;KeyValuePair&lt;Type, ImportExportAttribute&gt;&gt;.</returns>
        private static KeyValuePair<Type, ImportExportAttribute> GetObjectTypeInAssembly(Assembly assembly, string objectName)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(ImportExportAttribute), true).Length > 0)
                {
                    var attributeFound = type.GetCustomAttributes(typeof(ImportExportAttribute), true)
                        .SingleOrDefault(attr => String.Compare(((ImportExportAttribute)attr).ObjectName.ToString(), objectName) == 0);
                    if (attributeFound != null)
                        return new KeyValuePair<Type, ImportExportAttribute>(type, attributeFound as ImportExportAttribute);
                }
            }
            return default;
        }

        public Result ValidateFileSystemContent(ImportExportFilter filter, string extension,List<IExportObject> masterObjectsToExport)
        { 
            List<OperationInfo> exportInformations = new();
            // Gestione dei file dell'export
            ExportFileManagement fileManagement = new(filter, extension);

            // Validazione dei nomi dei file da creare.         
            var errors = new Dictionary<string, string>();
            foreach (var identifier in masterObjectsToExport)
            {
                var objectToExp= (ExportImportObject)identifier;
                var filename = fileManagement.GetFullFileName(objectToExp.Identifier);
                //se non è forzata la sovrascrittura devo controllare che esista e mandare un messaggio d'errore
                if (File.Exists(filename) && !errors.ContainsKey(filename))
                    errors.Add(objectToExp.Id.ToString(), ErrorCodesEnum.ERR_GEN003.ToString());
            }
            if (errors.Count() > 0) // Fallisco perchè il file esiste già 
                return Result.Fail(errors.Select(err => new ErrorDetail(err.Key, err.Value)));

            return Result.Ok();
        }

        /// <summary>
        /// Exports to json asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Task&lt;Result&lt;System.Object&gt;&gt;.</returns>
        public Result<TaskInfo> ExportToJsonAsync(ImportExportFilter filter, string extension, List<IExportObject> objectsToExport)
        {
             List<OperationInfo> exportInformations = new();
            // Gestione dei file dell'export
            ExportFileManagement fileManagement = new(filter, extension);

            // Lista delle operazioni di export che si stanno per avviare.        
            
            foreach (var identifier in objectsToExport)
            {
                var objectToExport=(ExportImportObject)identifier;
                var task = new OperationInfo()
                {
                    Identifier = objectToExport.Id,
                    Content = objectToExport
                };
                task.SetStatus(GenericEventStatusEnum.Waiting);
                exportInformations.Add(task);
            }
           
            // Recupero il guid associato al task impostato (qui arrivo se non ho nessun problema)
            var taskInfo = CancellableBackgroundTaskQueue.QueueWorkItem((scope, operationTask) => 
                    ExportToJson(scope, operationTask, fileManagement), exportInformations);

            return Result.Ok(taskInfo);
        }

        /// <summary>
        /// Funzione che restituisce gli oggetti da esportare in base al filtro applicato e all'enumerativo degli oggetti, in stringa
        /// </summary>
        /// <param name="objectTypename">The object typename.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        private Result<List<IExportObject>> GetObjectsToExport(string objectTypename
                                                            , ImportExportFilter filter
                                                            , IServiceFactory serviceFactory
                                                            , CumulativeCancellationToken token
                                                            , StatusEvent statusEvent)
        {
            // Recupero il tipo di oggetto e i suoi attributi necessari all'esportazione 
            var type = RetrieveTypeWithImportAttributeByName(objectTypename);
            if (!type.Equals(default(KeyValuePair<Type, ImportExportAttribute>))) //diverso da null
            {
                // Recupero il servizio che si deve occupare di esportare l'oggetto
                var service = ResolveImportExportDataProvider(serviceFactory, type.Value.ServiceToBeUsed);
                if (service == null)
                    return Result.Fail<List<IExportObject>>(ErrorCodesEnum.ERR_GEN002.ToString());

                // Recupero il tipo di filtro per l'oggetto da esportare
                Type filterType = type.Value.FilterType;

                if (filterType.IsInterface)
                {
                    // Recupero il filtro da utilizzare (non so quale tipo di filtro si applichi) per ottenere l'oggetto nel caso in cui sia un'interfaccia(Profilo)
                    var resolverType = typeof(IResolver<>).MakeGenericType(filterType);
                    var resolver = serviceFactory.GetService(resolverType) as IResolver;
                    var serviceResolved = resolver.Resolve();
                    filterType = serviceResolved.GetType();
                }
                // Crea un oggetto del tipo indicato dall'attributo  
                var filterObject = Activator.CreateInstance(filterType);
                // Recupera il metodo per convertire da ExportFilter nel filtro indicato dall'attributo
                var ConvertFilter = filterType.GetMethod("ConvertFilter");
                if (ConvertFilter != null)
                {
                    if (token.IsCancellationRequested())
                        return Result.Abort<List<IExportObject>>();

                    // Converte la ExportFilter nel filtro valido per quel servizio
                    ConvertFilter.Invoke(filterObject, new object[] { filter });

                    // Recupero l'oggetto da mettere sul file
                    return service.ExportToJsonService(filterObject, token,statusEvent);
                }
            }
            return Result.Fail<List<IExportObject>>(ErrorCodesEnum.ERR_GEN002.ToString());
        }

        public Result<List<IExportObject>> GetObjectIdentifiersToExport(ImportExportFilter filter)
        {
            // Recupero gli identificatori degli oggetti da esportare (id,Identificatori) .   
            return GetObjectIdentifiersToExport(filter, ServiceFactory, null);
        }


        /// <summary>
        /// Funzione che restituisce gli oggetti da esportare in base al filtro applicato e all'enumerativo degli oggetti, in stringa
        /// </summary>
        /// <param name="objectTypename">The object typename.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        private Result<List<IExportObject>> GetObjectIdentifiersToExport(ImportExportFilter filter
                                                            , IServiceFactory serviceFactory
                                                            , CumulativeCancellationToken token)
        {
            // Recupero il tipo di oggetto e i suoi attributi necessari all'esportazione 
            var type = RetrieveTypeWithImportAttributeByName(filter.ObjectType.ToString());
            if (!type.Equals(default(KeyValuePair<Type, ImportExportAttribute>))) //diverso da null
            {
                // Recupero il servizio che si deve occupare di esportare l'oggetto
                var service = ResolveImportExportDataProvider(serviceFactory, type.Value.ServiceToBeUsed);
                if (service == null)
                    return Result.Fail<List<IExportObject>>(ErrorCodesEnum.ERR_GEN002.ToString());

                // Recupero il tipo di filtro per l'oggetto da esportare
                Type filterType = type.Value.FilterType;

                if (filterType.IsInterface)
                {
                    // Recupero il filtro da utilizzare (non so quale tipo di filtro si applichi) per ottenere l'oggetto nel caso in cui sia un'interfaccia(Profilo)
                    var resolverType = typeof(IResolver<>).MakeGenericType(filterType);
                    var resolver = serviceFactory.GetService(resolverType) as IResolver;
                    var serviceResolved = resolver.Resolve();
                    filterType = serviceResolved.GetType();
                }
                // Crea un oggetto del tipo indicato dall'attributo  
                var filterObject = Activator.CreateInstance(filterType);
                // Recupera il metodo per convertire da ExportFilter nel filtro indicato dall'attributo
                var ConvertFilter = filterType.GetMethod("ConvertFilter");
                if (ConvertFilter != null)
                {
                    // Converte la ExportFilter nel filtro valido per quel servizio
                    ConvertFilter.Invoke(filterObject, new object[] { filter });

                    // Recupero gli identificatori dell'oggetto da mettere sul file
                    return service.GetObjectIdentifiersToExport(filterObject, token);
                }
            }
            return Result.Fail<List<IExportObject>>(ErrorCodesEnum.ERR_GEN002.ToString());
        }

        //Dato un oggetto trova tutte le proprietà (CustomAttribute,valore) degli oggetti da cui dipende 
        /// <summary>
        /// Gets the dependency object value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>List&lt;KeyValuePair&lt;DependencyObjectForExportAttribute, System.Int64&gt;&gt;.</returns>
        public List<DependencyObjectForExportAttribute> GetDependenciesForObjectType(string typeName)
        {
            List<DependencyObjectForExportAttribute> dependencyAttributes = new List<DependencyObjectForExportAttribute>();
            if (typeName == null) return dependencyAttributes;

            var objType = RetrieveTypeWithImportAttributeByName(typeName).Key;
            Attribute[] attributes = objType.GetCustomAttributes().ToArray();
            foreach (var attribute in attributes)
            {
                if (attribute.GetType() == typeof(DependencyObjectForExportAttribute))
                    dependencyAttributes.Add((DependencyObjectForExportAttribute)attribute);
            }
            return dependencyAttributes;
        }

        /// <summary>
        /// Exports to json.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Result&lt;System.Object&gt;.</returns>

        private async Task ExportToJson(IServiceScope scope, IdentifiedTask identTask, ExportFileManagement fileManagement)
        {
            var serviceFactory = scope.ServiceProvider.GetRequiredService<IServiceFactory>();
            var progressEventHubClient = serviceFactory.GetService<IEventHubClient>();
            CumulativeCancellationToken token = identTask.CumulativeCancellationToken;
            var objectToExports = identTask.Operations;
            var iterator = new ImportExportItemsEnumerator(objectToExports, progressEventHubClient);

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    // Ciclo su tutti gli oggetti che devono essere esportati
                    for (OperationInfo taskToExport = iterator.GetNextObjectToExport(); taskToExport != null; taskToExport = iterator.GetNextObjectToExport())
                    {
                        if (token.IsCancellationRequested())
                            iterator.CurrentObjectAborted();
                        if (taskToExport.Status == GenericEventStatusEnum.Running) //escludo il taskToImport.status= Failed
                        {
                            ExportImportObject objectToExport = (ExportImportObject)taskToExport.Content;

                            int objectExportCounter = 1;
                            int totalObjectsToExport = 1;
                            if (objectToExport.Object != null)
                                totalObjectsToExport = objectToExport.Object.Dependencies.Count + 1;

                            StatusEvent statusEvent = new StatusEvent(progress: objectExportCounter
                                        , status: taskToExport.Status
                                        , total: totalObjectsToExport
                                        , Id: taskToExport.Identifier);
                            Debug.WriteLine($"{taskToExport.Identifier} {taskToExport.Status} {objectExportCounter}/{ totalObjectsToExport}");
                            // Notifico inizio  export (Running)
                            progressEventHubClient.StatusEvent(statusEvent);

                            // Genero un nuovo filtro per andare a prendere un solo master (filtro con solo l'indice dell'oggetto cercato)
                            var filter = new ImportExportFilter() { ObjectType = objectToExport.Type, ObjectIdentifiers = new List<object>() { objectToExport.Id } };
                            // Recupero l'oggetto "Master" da esportare
                            var resultObject = GetObjectsToExport(objectToExport.Type.ToString(), filter, serviceFactory, token, statusEvent);
                            if (resultObject.Success)
                            {
                                // In questo algoritmo viene sono restituito un solo master perchè il filtro è stato impostato con l'indice di un oggetto. 
                                // Ho solo il master senza le dipendenze
                                var masterObjectToExport = resultObject.Value.FirstOrDefault();
                                // Recupero tutte le dipendenze 

                                resultObject = RetrieveMasterDepedencies(masterObjectToExport, serviceFactory, token, statusEvent);
                                if (resultObject.Success)
                                {
                                    iterator.CurrentObjectCompleted();
                                    // Aggiungo l'oggetto perchè venga salvato su file

                                    fileManagement.SaveObjectToFile(masterObjectToExport.GetObjectIdentifier(), OptimizeExportStruct(resultObject.Value));
                                    if (token.IsCancellationRequested())
                                        iterator.CurrentObjectAborted();
                                }
                            }

                            if (resultObject.Failure && !resultObject.Aborted)
                                iterator.CurrentObjectFailed(resultObject.Error.ToString());
                            else if (resultObject.Aborted)
                                iterator.CurrentObjectAborted();
                        }
                    }
                    Debug.WriteLine("Export ended");
                    return Result.Ok();
                }
                catch (Exception exception)
                {
                    // Evento di progressione della fase di Export
                    progressEventHubClient.StatusEvent(new StatusEvent(progress: 0, status: GenericEventStatusEnum.Failed, localizationKey: exception.InnerException?.Message ?? exception.Message));
                    return Result.Fail(exception.InnerException?.Message ?? exception.Message);
                }
            });
            //, cancellationToken: token.ShutdownToken
            //, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning
            //, TaskScheduler.Default);
        }
        private Result<List<IExportObject>> RetrieveMasterDepedencies(IExportObject masterObjectToExport,
                                                                      IServiceFactory serviceFactory,
                                                                      CumulativeCancellationToken token,
                                                                      StatusEvent statusEvent)
        {
            var progressEventHubClient = serviceFactory.GetService<IEventHubClient>();
            // Per ogni oggetto da esportare richiesto devo recuperare le sue dipendenze
            List<IExportObject> dependencyObjectsToSerialize = new List<IExportObject>();
            // Per ogni oggetto da esportare trovo tutti gli oggetti correlati (dependencyObjects = dipendenze dirette)
            var dependencyObjects = masterObjectToExport.GetObjectToExport().Dependencies;
            
            // La progressione parz/tot è fatta sulle dipendenze dirette 
            // Se ci sono oggetti da cui l'oggetto da esportare dipende devo esportare anche quelli  e non ho avuto fallimento 
            for (int i = 0; i < dependencyObjects.Count; i++)
            {
                var objEnum = dependencyObjects[i];
                statusEvent = new StatusEvent(progress:statusEvent.GetProgress()+1
                                     , status: statusEvent.GetStatus()
                                     , total: statusEvent.GetTotal()
                                     , Id: statusEvent.GetId());
                // Notifico inizio  export (Running)
                progressEventHubClient.StatusEvent(statusEvent);
                var slave = new List<KeyValuePair<ImportExportObjectEnum, long>>();
                // Per ogni dipendenza diretta devo individuare se ci sono ulteriori dipendenze (dipendenze indirette, fatte solitamente da piccoli oggetti,
                // che vanno esportati comunque ma sono trasparenti nel prograsso dell'utente)
                // [Il giro originale è stato complicato per dare un feedback all'utente del progresso dell'operazione]
                int counter = 0;
                for (;;)
                {
                    // Estraggo l'oggetto dipendenza diretta
                    var resultObject = GetObjectsToExport(objEnum.Key.ToString()
                                                    , new ImportExportFilter() { ObjectIdentifiers = new List<object>() { objEnum.Value } }
                                                    , serviceFactory
                                                    , token
                                                    , statusEvent);
                    if (resultObject.Failure)
                        return resultObject;

                    var dependencyObjectsExport = resultObject.Value.FirstOrDefault();

                    if (dependencyObjectsExport != null)
                    {
                        // L'oggetto correlato potrebbe avere a sua volta delle dipendenze da considerare (indirette)
                        slave = AddRange(slave, dependencyObjectsExport.GetObjectToExport().Dependencies);

                        // Aggiungo l'oggetto da serializzare nella lista
                        dependencyObjectsToSerialize.Add(dependencyObjectsExport);

                    }
                    if (counter < slave.Count())
                    {
                        objEnum = slave[counter];
                        counter++;
                    }
                    else
                        break;
                } 
            }
            // Vengono inserite prima tutte le dipendenze e poi l'oggetto principale
            dependencyObjectsToSerialize.Add(masterObjectToExport);
            return Result.Ok(dependencyObjectsToSerialize);
        }

        public List<KeyValuePair<ImportExportObjectEnum, long>> AddRange(List<KeyValuePair<ImportExportObjectEnum,long>> destination,
                                          List<KeyValuePair<ImportExportObjectEnum, long>> source)
        {
            var equalComparer = new DependencyAttributeListComparer();
            var list = destination as List<KeyValuePair<ImportExportObjectEnum, long>>;

            if (list == null)
            {
                list = source.ToList();
            }
            else
            {
                foreach (var item in source)
                {
                    var f = list.FirstOrDefault(l => equalComparer.Equals(item, l));
                    if (f.Equals(default(KeyValuePair<ImportExportObjectEnum, long>)))
                        list.Add(item);
                }
            }
            return list;
        }

        private List<ExportImportList> OptimizeExportStruct(List<IExportObject> dependencies)
        {
            var groupexported = dependencies.GroupBy(exportedObj => exportedObj.GetObjectType().ToString());
            List<ExportImportList> objectsToSerialize = new List<ExportImportList>();
            foreach (var group in groupexported)
            {
                Dictionary<string, object> objects = new Dictionary<string, object>();
                foreach (var exportedItem in group)
                {
                    //elimino i duplicati
                    if (!objects.ContainsKey(exportedItem.GetObjectIdentifier()))
                        objects.Add(exportedItem.GetObjectIdentifier(), exportedItem.GetObjectToExport());
                }
                objectsToSerialize.Add(new ExportImportList() { Type = group.Key, Objects = objects.Select(kvp => kvp.Value).ToList() });
            }
            return (objectsToSerialize.OrderBy(objecttoexp => objecttoexp, new ExportImportListComparer()).ToList());
        }

        public async Task<Result> AbortImportExportOperations(List<string> guids)
        {
            await Task.Factory.StartNew(() =>
            {
                CancellableBackgroundTaskQueue.CancellationRequest(guids);
            });
            return Result.Ok();
        }

        private bool ValidateFileContentWithObjectType(List<ExportImportList> objects, ImportExportObjectEnum type)
        {
            return objects.Any(o => 
            {
                if (Enum.TryParse<ImportExportObjectEnum>(o.Type, out var typeEnum))
                {
                    if (typeEnum == type)
                        return true;
                }
                return false; 
            });
        }
        /// <summary>
        /// Import From Json Format
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>Task&lt;Result&lt;System.Int64&gt;&gt;.</returns>
        /// Chiamata 
        public Result<TaskInfo> ImportFromJsonAsync(string json, string validation, bool validateFileContent)
        {
            var tasks = new TaskInfo();
            // Aggiungo il convertitore per gli identificatori dei Tool
            var options = new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.None,
            };
            // Aggiungo il convertitore per gli identificatori dei Tool
            options.Converters.Add(new DictionaryToPropertiesJsonConverter());
            try 
            {
                var objectsToImport = JsonConvert.DeserializeObject<IEnumerable<ExportImportList>>(json, options);
                tasks = new TaskInfo();

                if (validateFileContent)
                {
                    if (!EncryptionManagement.ValidateFileContent(validation, json))
                        return Result.Fail<TaskInfo> (ErrorCodesEnum.ERR_IMEX15.ToString());
                }
                var importTasks = new List<OperationInfo>();

                OperationInfo task = new() { Identifier = 0 };
                task.SetStatus(GenericEventStatusEnum.Waiting);
                task.Content = objectsToImport;
                       
                importTasks.Add(task);
             
                var taskInfo = CancellableBackgroundTaskQueue.QueueWorkItem((scope, identifTask) => ImportFromJsonMultiFiles(scope, identifTask), importTasks);

                return Result.Ok(taskInfo);

            }
            catch(Exception e)
            {

            }
            return Result.Ok(tasks);
        }

        //verifica se il pathToVerify è una sottocartella di pathParent
        private bool IsSubDirectoryPath(string pathToVerify, string pathParent)
        {
            var parentUri = new Uri(pathParent);
            var childUri = new DirectoryInfo(pathToVerify);
            while (childUri != null)
            {
                //la cartella deve essere una sotto directory di quella permessa
                if (string.Compare(new Uri(childUri.FullName).AbsolutePath, parentUri.AbsolutePath, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    return true;
                }
                childUri = childUri.Parent;
            }
            return false;
        }

        public Result ValidatePath(string path, IEnumerable<ImportExportPath> pathsEnabled)
        {
            try
            {
                // Verifico la validità del percorso
                // La cartella deve esistere
                if (!Directory.Exists(path))
                    return Result.Fail(ErrorCodesEnum.ERR_EXP003.ToString());

                if (pathsEnabled != null)
                {
                    // Verifico che il path dove esportare sia una sottodirectory dei quelli configurati
                    foreach (var p in pathsEnabled)
                    {
                        var subDirectory = IsSubDirectoryPath(path, p.Path);
                        if (subDirectory)
                            return Result.Ok();
                    }
                    return Result.Fail(ErrorCodesEnum.ERR_EXP001.ToString());
                }
                return Result.Ok();
            }
            catch (JsonSerializationException)
            {
                return Result.Fail(ErrorCodesEnum.ERR_GEN006.ToString());
            }
        }
    

        public Result<TaskInfo> ImportFromJsonAsync(ImportExportFilter filter, string extension, bool validateFileContent)
        {
            var importTasks = new List<OperationInfo>();
            var validationFilesContent = ValidateFilesContent(filter, extension,validateFileContent);
            var filesContents = new Dictionary<string,List<ExportImportList>>();
            // Verifico la validità di tutti i files e del loro contenuto (colleziono il risultato)
            foreach (var content in validationFilesContent)
            {
                OperationInfo task = new()
                {
                    Identifier = content.Key
                };
                task.SetStatus(GenericEventStatusEnum.Waiting);
                if (content.Value.Success)
                {
                    filesContents.Add(content.Key, content.Value.Value);
                    task.Content = content.Value.Value;
                }
                else
                {
                    task.SetStatus(GenericEventStatusEnum.Failed);
                    task.StatusLocalizationKey = content.Value.Error.ToString();
                    filesContents.Add(content.Key, null);
                }
                importTasks.Add(task);
            }

            var taskInfo = CancellableBackgroundTaskQueue.QueueWorkItem((scope, identifTask) => ImportFromJsonMultiFiles(scope, identifTask), importTasks);

            return Result.Ok(taskInfo);
        }

        private Dictionary<string, Result<List<ExportImportList>>> ValidateFilesContent(ImportExportFilter filter, string ext, bool validateFileContent)
        {
            var filesContents = new Dictionary<string, Result<List<ExportImportList>>>();
            // Verifico la validità di tutti i files e del loro contenuto (colleziono il risultato)
            var extension = $".{ext}";
            foreach (var name in filter.ObjectIdentifiers)
            {
                var fullFileName = Path.Combine(filter.Path, name + extension);
                if (System.IO.File.Exists(fullFileName))
                {
                    var content = System.IO.File.ReadAllText(fullFileName);

                    if (validateFileContent && !EncryptionManagement.ValidateFileContent(content))
                    {
                        filesContents.Add(name.ToString(),Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX15.ToString()));
                    }
                    else
                    {
                        var result = ValidateImportType(filter.ObjectType, content);
                        filesContents.Add(name.ToString(), result);
                    }
                }
                else
                {
                    filesContents.Add(name.ToString(), Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_GEN002.ToString()));
                }
            }
            return filesContents;
        }

        private Result<List<ExportImportList>> ValidateImportType(ImportExportObjectEnum requiredObjectType, string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    return Result.Fail<List<ExportImportList>> (ErrorCodesEnum.ERR_GEN001.ToString());
                }
                var options = new JsonSerializerSettings
                {
                    DateParseHandling = DateParseHandling.None,
                };
                //Aggiungo il convertitore per gli identificatori dei Tool
                options.Converters.Add(new DictionaryToPropertiesJsonConverter());
               
                var objectsToImport = JsonConvert.DeserializeObject<List<ExportImportList>>(json, options);
                    
                if (ValidateFileContentWithObjectType(objectsToImport,requiredObjectType) == false)
                {
                    switch (requiredObjectType)
                    {
                        case ImportExportObjectEnum.PieceObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX02.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.ProgramObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX03.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.ToolHolderObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX07.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.ToolObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX08.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.ToolTableObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX09.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.StockObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX10.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.MaintenanceObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX11.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.MaterialObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX12.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.ProfileObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX13.ToString());//non ho trovato l'oggetto richiesto
                        case ImportExportObjectEnum.AccountObject:
                            return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_IMEX14.ToString());//non ho trovato l'oggetto richiesto
                    }
                }

                return Result.Ok(objectsToImport);
            }
            catch (JsonSerializationException)
            {
                return Result.Fail<List<ExportImportList>>(ErrorCodesEnum.ERR_GEN006.ToString());
            }
        }

        /// <summary>
        /// Imports from json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns
        /// Chiamata per più masters contenuti in un unico item della coda. 
        private async Task ImportFromJsonMultiFiles(IServiceScope scope, IdentifiedTask identTask)
        {
            var serviceFactory = scope.ServiceProvider.GetRequiredService<IServiceFactory>();
            var progressEventHubClient = serviceFactory.GetService<IEventHubClient>();
            CumulativeCancellationToken token = identTask.CumulativeCancellationToken;
            List<OperationInfo> masterObjectsToImport = identTask.Operations;
            if (token.IsCancellationRequested()) return;

            var iterator = new ImportExportItemsEnumerator(masterObjectsToImport, progressEventHubClient);

            await Task.Factory.StartNew(() =>
            {
                //evento di progressione della fase di Export                
                try
                {
                    // Ciclo su tutti gli oggetti che devono essere importati
                    for (OperationInfo taskToImport = iterator.GetNextObjectToExport(); taskToImport != null; taskToImport = iterator.GetNextObjectToExport())
                    {
                        if (token.IsCancellationRequested()) //taskToImport.status= Aborting
                            iterator.CurrentObjectAborted();
                        if (taskToImport.Status == GenericEventStatusEnum.Running) //escludo il taskToImport.status= Failed
                        {
                            var statusEvent = new StatusEvent(status: taskToImport.Status, Id: taskToImport.Identifier);
                            progressEventHubClient.StatusEvent(statusEvent);

                            var unitOfWorkFactory = serviceFactory.GetService<IUnitOfWorkFactory>();
                            var uow = unitOfWorkFactory.Create(UserSession);
                            unitOfWorkFactory.BeginTransaction(uow);

                            var res = ImportFromJsonSingleFile(serviceFactory,
                                                               progressEventHubClient,
                                                               token,
                                                               ((IEnumerable)taskToImport.Content).Cast<ExportImportList>().ToList(),
                                                               uow,
                                                               statusEvent
                                                               ,unitOfWorkFactory);
                            if (res.Success)
                            {
                                unitOfWorkFactory.Commit(uow);
                                unitOfWorkFactory.CommitTransaction(uow);
                                // Import master corrente completata
                                iterator.CurrentObjectCompleted();
                            }
                            else 
                            {
                                unitOfWorkFactory.RollBackTransaction(uow);

                                if (res.Aborted)
                                    iterator.CurrentObjectAborted(); // Import file corrente (e successivi) abortita
                                else
                                    iterator.CurrentObjectFailed(res.Error);// Import file corrente fallita
                            }

                        }
                        else if (taskToImport.Status == GenericEventStatusEnum.Failed)
                        {
                            var statusEvent = new StatusEvent(status: taskToImport.Status, Id: taskToImport.Identifier, localizationKey: taskToImport.StatusLocalizationKey);
                            progressEventHubClient.StatusEvent(statusEvent);
                        }
                    }
                    return Result.Ok();
                }
                catch (JsonSerializationException)
                {
                    //evento di progressione della fase di Import
                    progressEventHubClient.StatusEvent(new StatusEvent(status: GenericEventStatusEnum.Failed, localizationKey: ErrorCodesEnum.ERR_GEN006.ToString()));
                    return Result.Fail(ErrorCodesEnum.ERR_GEN006.ToString());
                }
            }
            , cancellationToken: token.ShutdownToken
            , TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning
            , TaskScheduler.Default);
        }

        private Result ImportFromJsonSingleFile(IServiceFactory serviceFactory
                                        , IEventHubClient progressEventHubClient
                                        , CumulativeCancellationToken token
                                        , List<ExportImportList> objectsToImport
                                        , IUnitOfWork uow
                                        , StatusEvent statusEvent
                                        , IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (token.IsCancellationRequested()) return Result.Abort();

            //evento di progressione della fase di Export                
            try
            {
                // Recupero tutti i servizi necessari per importare gli oggetti
                var resService= GetServicesForDependency(serviceFactory, objectsToImport);
                if (resService.Failure)
                    return Result.Fail(ErrorCodesEnum.ERR_GEN002.ToString());

                var services = resService.Value;

                // Cancello logicamente tutti gli oggetti che verranno cambiati durante l'import
                GenericImportDependencies(progressEventHubClient, token, objectsToImport, uow, services,null, unitOfWorkFactory, InitializeImport);
                var statusEventGlobal = new StatusEvent(status: statusEvent.GetStatus()
                                                     , total: objectsToImport.Count + 1 //aggiungo un item che sarà la fase finale
                                                    , progress: 0
                                                    , Id: statusEvent.GetId());
                var importResult = GenericImportDependencies(progressEventHubClient, token, objectsToImport, uow, services, statusEventGlobal,unitOfWorkFactory, ImportObjectsSameType);
                if (importResult.Success)
                {
                    statusEventGlobal = new StatusEvent(status: statusEvent.GetStatus()
                                                     , total: objectsToImport.Count + 1 //aggiungo un item che sarà la fase finale
                                                    , progress: objectsToImport.Count + 1
                                                    , Id: statusEvent.GetId());
                    progressEventHubClient.StatusEvent(statusEventGlobal);
                    // Cancello fisicamente tutti gli oggetti, ma prima riassegno se hanno legami gli oggetti(programmi, productionList)
                    importResult = GenericImportDependencies(progressEventHubClient, token, objectsToImport, uow, services, null, unitOfWorkFactory, FinalizeImport);
                }
                return importResult;
            }
            catch (JsonSerializationException)
            {
                //evento di progressione della fase di Import
                return Result.Fail(ErrorCodesEnum.ERR_GEN006.ToString());
            }
        }
        private Result GenericImportDependencies(IEventHubClient progressEventHubClient
                                       , CumulativeCancellationToken token
                                       , List<ExportImportList> objectsToImport
                                       , IUnitOfWork uow
                                       , IEnumerable<HelpImportResolver> resolvers
                                       , StatusEvent statusEvent
                                       , IUnitOfWorkFactory unitOfWorkFactory
                                       , Func<CumulativeCancellationToken, ExportImportList, StatusEvent, List<HelpImportResolver>, IUnitOfWork, IUnitOfWorkFactory, Result> func)
        {
            try
            {
                int counter = 1;
                foreach (var objecttoimport in objectsToImport)
                {
                    if (statusEvent != null)
                    {
                        //progresso delle dipendenze
                        statusEvent = new StatusEvent(status: statusEvent.GetStatus()
                                                     , total: statusEvent.GetTotal()
                                                    , progress: counter
                                                    , Id:statusEvent.GetId());
                        progressEventHubClient.StatusEvent(statusEvent);
                    }
                    // Interrompo se è stata richiesta la cancellazione
                    if (token.IsCancellationRequested())
                    {
                        return Result.Abort();
                    }
                    //evento di progressione della fase di Export
                    if (objecttoimport.Objects != null)
                    {
                        var res = func(token, objecttoimport, statusEvent, resolvers.ToList(),  uow, unitOfWorkFactory);
                        if (res.Aborted)
                            return Result.Abort();
                        if (res.Failure)
                        {
                            // Se ha fallito vuol dire che un oggetto ha fallito l'import quindi esco dall'import di questo file
                            return Result.Fail(res.Error);
                        }
                    }
                    counter++;
                }
                //cancellare fisicamente tutti gli oggetti, ma prima riassegno se hanno legami gli oggetti(programmi, productionList)
                return Result.Ok();
            }
            catch (JsonSerializationException)
            {
                //evento di progressione della fase di Import
                return Result.Fail(ErrorCodesEnum.ERR_GEN006.ToString());
            }
        }

        private Result ImportObjectsSameType(CumulativeCancellationToken token
                                            , ExportImportList objectsToImportList
                                            , StatusEvent statusEvent
                                            , List<HelpImportResolver> resolver
                                            , IUnitOfWork uow
                                            , IUnitOfWorkFactory unitOfWork)
        {
            var serv = resolver.FirstOrDefault(s => s.Type.CompareTo(objectsToImportList.Type) == 0);
            if (serv!=null)
            {
                var objectTypeToImport = serv.ObjectTypeToImport;
                var ConvertType = serv.MethodInfo;
                foreach (var obj in objectsToImportList.Objects)
                {
                    ConvertType.Invoke(objectTypeToImport, new object[] { obj });
                    var service =serv.DataProvider;
                    var result = service.ImportFromJsonService(objectTypeToImport, token, statusEvent, uow, unitOfWork);
                    if (result.Aborted)
                        return Result.Abort(); //Operazione interrotta
                    if (result.Failure)
                    {
                        //se ho avuto un problema durante l'import allora esco dall'import del file corrente
                        return Result.Fail(result.Error);
                    }
                }
                return Result.Ok();
            }
            return Result.Fail(ErrorCodesEnum.ERR_GEN001.ToString());
        }

        private Result InitializeImport(CumulativeCancellationToken token
                                            , ExportImportList objectsToImportList
                                            , StatusEvent statusEvent
                                            , List<HelpImportResolver> resolver
                                            , IUnitOfWork uow
                                            , IUnitOfWorkFactory unitOfWorkFactory)
        {
            var serv = resolver.FirstOrDefault(s => s.Type.CompareTo(objectsToImportList.Type) == 0);
            if (serv != null)
            {
                var objectTypeToImport = serv.ObjectTypeToImport;
                var ConvertType = serv.MethodInfo;
                foreach (var obj in objectsToImportList.Objects)
                {
                    ConvertType.Invoke(objectTypeToImport, new object[] { obj });
                    var service = serv.DataProvider;
                    var result = service.InizializeImport(objectTypeToImport, token, uow, unitOfWorkFactory);
                    if (result.Aborted)
                        return Result.Abort(); //Operazione interrotta
                    if (result.Failure)
                    {
                        //se ho avuto un problema durante l'import allora esco dall'import del file corrente
                        return Result.Fail(result.Error);
                    }
                }
                return Result.Ok();
            }
            return Result.Fail(ErrorCodesEnum.ERR_GEN001.ToString());
        }

        private Result FinalizeImport(CumulativeCancellationToken token
                                            , ExportImportList objectsToImportList
                                            , StatusEvent statusEvent
                                            , List<HelpImportResolver> resolver
                                            , IUnitOfWork uow
                                            , IUnitOfWorkFactory unitOfWorkFactory)
        {
            var serv = resolver.FirstOrDefault(s => s.Type.CompareTo(objectsToImportList.Type) == 0);
            if (serv != null)
            {
                var objectTypeToImport = serv.ObjectTypeToImport;
                var ConvertType = serv.MethodInfo;
                foreach (var obj in objectsToImportList.Objects)
                {
                    ConvertType.Invoke(objectTypeToImport, new object[] { obj });
                    var service = serv.DataProvider;
                    var result = service.FinalizeImport(objectTypeToImport, token, uow,unitOfWorkFactory);
                    
                    if (result.Result.Aborted)
                        return Result.Abort(); //Operazione interrotta
                    if (result.Result.Failure)
                    {
                        //se ho avuto un problema durante l'import allora esco dall'import del file corrente
                        return Result.Fail(result.Result.Error);
                    }
                }
                return Result.Ok();
            }
            return Result.Fail(ErrorCodesEnum.ERR_GEN001.ToString());
        }

        private Result<HelpImportResolver> GetService(IServiceFactory serviceFactory,string typeName)
        {
            var type = RetrieveTypeWithImportAttributeByName(typeName);
            if (!type.Equals(default(KeyValuePair<Type, ImportExportAttribute>)))
            {
                var objectTypeToImport = Activator.CreateInstance(type.Key);
                var ConvertType = type.Key.GetMethod("Convert");

                var service = ResolveImportExportDataProvider(serviceFactory, type.Value.ServiceToBeUsed);
                return Result.Ok(new HelpImportResolver() { DataProvider = service,MethodInfo=ConvertType,Type=typeName,ObjectTypeToImport=objectTypeToImport});

            }
            return Result.Fail<HelpImportResolver>(ErrorCodesEnum.ERR_GEN001.ToString());
        }

        private Result<List<HelpImportResolver>> GetServicesForDependency(IServiceFactory serviceFactory, List<ExportImportList> list)
        {
            var services = new List<HelpImportResolver>();
            foreach (var objectType in list)
            {
                var res = GetService(serviceFactory, objectType.Type);
                if (res.Failure)
                    return Result.Fail<List<HelpImportResolver>>(ErrorCodesEnum.ERR_GEN001.ToString());
                if (res.Success)
                    services.Add(res.Value);
            }
            return Result.Ok(services);
        }
    }
    public class HelpImportResolver
    {
        public IImportExportDataProvider DataProvider { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public string Type { get; set; }

        public object ObjectTypeToImport { get; set; }
    }
}
