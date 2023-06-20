namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Core.CustomExceptions;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class MachineParameterService : MachineManagementBaseService, IMachineParameterService
    {
        private readonly Lazy<IParameterHandler> _parameterHandler;

        public MachineParameterService(IServiceFactory serviceFactory)
            : base(serviceFactory)
        {
            _parameterHandler = new Lazy<IParameterHandler>(() => ServiceFactory.Resolve<IParameterHandler>());
        }

        private IMachineConfigurationService MachineConfigurationService => ServiceFactory.GetService<IMachineConfigurationService>();
        private IMachineParameterRepository MachineParameterRepository => ServiceFactory.GetService<IMachineParameterRepository>();
        private IParameterHandler ParameterHandler => _parameterHandler.Value;

        /// <summary>
        /// Machine Parameter Service Boot
        /// </summary>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public Result Boot(IUserSession userSession)
        {
            try
            {
                SetSession(userSession);
                return ImportParametersAndGroups() //Ritorna i parametri ed i gruppi importati
                    .OnFailure(message => EventHubClient.ProgressEvent(new ProgressEvent($"Parameters Boot KO..{message}", EventTypeEnum.Boot)));
            }
            catch (Exception ex)
            {
                var errorMessage = $"Parameters Boot KO..{ex.InnerException?.Message ?? ex.Message}";

                EventHubClient.ProgressEvent(new ProgressEvent(errorMessage, EventTypeEnum.Error));
                EventLogHubClient.WriteLogEvent(new WriteLogEvent() { Message = errorMessage, EventType = EventTypeEnum.Error });

                return Result.Fail(errorMessage);
            }
        }

        /// <summary>
        /// Operazione Cleanup prima del Boot
        /// </summary>
        /// <param name="userSession"></param>
        public Result CleanUpBeforeBoot(IUserSession userSession) => Result.Ok();

        /// <summary>
        /// Export Parameters To Json format
        /// </summary>
        /// <returns></returns>
        public object ExportToJsonService()
        {
            var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            MachineParameterRepository.Attach(uow);
            return Mapper.Map<IEnumerable<MachineParameterToExport>>(MachineParameterRepository.GetAll());
        }

        /// <summary>
        /// Get Parameter Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MachineParameterItem Get(long id)
        {
            var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            MachineParameterRepository.Attach(uow);
            var dbEntity = MachineParameterRepository.Get(id);

            if (dbEntity == null)
                return null;

            var machineParameter = Mapper.Map<MachineParameterItem>(dbEntity);

           // Gitea #667
            machineParameter.UMLocalizationKey =
                               $"{DomainExtensions.GENERIC_LABEL}_{((AttributeDataFormatEnum)dbEntity.DataFormatId).ToString().ToUpper()}_METRICSYSTEM";

            machineParameter.ProtectionLevel = TypeDescriptor.GetConverter(typeof(ProtectionLevelEnum))
                            .ConvertTo
                            (null, CultureInfo.InvariantCulture, dbEntity.ProtectionLevel, typeof(string)).ToString();

            return machineParameter;
        }

        /// <summary>
        /// Get Parameter by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Return Parameter if exists.</returns>
        public MachineParameterItem Get(string code, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            MachineParameterRepository.Attach(uow);
            return Mapper.Map<MachineParameterItem>(MachineParameterRepository.Get(code));
        }

        /// <summary>
        /// Get Parameter from Category and Index
        /// </summary>
        /// <param name="categoryEnum"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public MachineParameterItem Get(ParameterCategoryEnum categoryEnum, short id)
        {
            var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            MachineParameterRepository.Attach(uow);
            return Mapper.Map<MachineParameterItem>(MachineParameterRepository.FindBy(x => x.Category == categoryEnum &&
                                                    x.Id == id).SingleOrDefault());
        }

        /// <summary>
        /// Recupera la lista di tutti i parametri presenti filtrati in base ai permessi dell'utente
        /// della sessione corrente.
        /// </summary>
        /// <param name="session">Session corrente</param>
        public Task<IEnumerable<MachineParameterItem>> GetAllItemsAsync(MeasurementSystemEnum conversionSystem)
        {
            IEnumerable<MachineParameterItem> get()
            {
                // Recupera i protection levels in base ai permessi
                var protectionLevels = UserSession.GetProtectionLevels();
                var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
                MachineParameterRepository.Attach(uow);
                // Filtra i parameteri in base ai protection levels ottenuti
                return ConvertToHelper<MachineParameterItem>.Convert(
                        conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                        , conversionSystemTo: conversionSystem
                        , entities: Mapper.Map<IEnumerable<MachineParameterItem>>(MachineParameterRepository
                            .FindBy(groupDetail => protectionLevels.Contains(groupDetail.ProtectionLevel))));
            }

            return Task.Factory.StartNew(get);
        }

        /// <summary>
        /// Async Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<MachineParameterItem> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        /// <summary>
        /// Async Get Parameter by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task<MachineParameterItem> GetAsync(string code, MeasurementSystemEnum conversionSystem)
        {
            return Task.Factory.StartNew(() => Get(code, conversionSystem));
        }


        public Task<IEnumerable<MachineParameterNodeConfiguration>> GetNodesAsync()
        {
            return Task.Factory.StartNew(GetNodes);
        }

        private IEnumerable<MachineParameterNodeConfiguration> GetNodes()
        {
            return Directory.EnumerateFiles(MachineConfigurationService.GetParametersDirectory(),
                                                           "*.node.json",
                                                           SearchOption.AllDirectories)
                .OrderBy(filename => filename)
                .Select(filename => JsonConvert.DeserializeObject<MachineParameterNodeConfiguration>(File.ReadAllText(filename)))
                .GroupBy(group => group.NodeName)
                .Select(group => group.Aggregate((merged, current) => Mapper.Map(source: current, destination: merged)))
                .OrderBy(node => node.Priority)
                .ToArray();
        }

        /// <summary>
        /// Get Value from Category and Index
        /// </summary>
        /// <param name="parameterCategory"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public decimal GetValue(ParameterCategoryEnum parameterCategory, int id
                    , MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            MachineParameterRepository.Attach(uow);
            var parameter = MachineParameterRepository.FindBy(x => x.Category == parameterCategory && x.Id == id).SingleOrDefault();
            decimal value = parameter?.Value ?? 0;

            if (parameter != null)
            {
                value = ConvertToHelper.Convert(
                      conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                    , conversionSystemTo: conversionSystem
                    , dataFormat: (AttributeDataFormatEnum)parameter.DataFormatId
                    , value: value).Value;
            }

            return value;
        }

        /// <summary>
        /// Importazione file json parameter values
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public IEnumerable<ImportResult<string>> ImportParameterValues(string json)
        {
            var parameterValueList = JsonConvert.DeserializeObject<List<MachineParameterToExport>>(json);
            var results = new List<ImportResult<string>>();

            using (var uow = UnitOfWorkFactory.GetOrCreate(UserSession))
            {
                MachineParameterRepository.Attach(uow);

                foreach (var paramValue in parameterValueList)
                {
                    var importResult = new ImportResult<string>();
                    var founded = MachineParameterRepository.Get(paramValue.Code, paramValue.Category);
                    importResult.Result = $"{paramValue.Code}_{paramValue.Category}";
                    if (founded == null)
                    {
                        importResult.ProcessingResult = ImportProcessingResultEnum.Invalid;
                    }
                    else
                    {
                        var serviceResult = InnerUpdateParameter(founded, paramValue.Value);
                        uow.Commit();
                        importResult.ProcessingResult = serviceResult.Success? ImportProcessingResultEnum.Updated 
                                                                                : ImportProcessingResultEnum.Failed;
                        if (importResult.ProcessingResult == ImportProcessingResultEnum.Failed)
                            importResult.ErrorDetails.Add(new ErrorDetail(ErrorCodesEnum.ERR_PAR007.ToString()));
                    }

                    results.Add(importResult);
                }

                return results;
            }
        }

        /// <summary>
        /// Set CNC Variables from parameters
        /// </summary>
        /// <returns></returns>
        public Result SetCNCVariables(CncTypeEnum cncTypeFilter = CncTypeEnum.All)
        {
            var result = Result.Ok();
            //Get All parameters with associations
            var parameters = MachineParameterRepository.GetMachineParameterWithLinks();
            foreach (var p in parameters)
            {
                result = Result.Combine(result, p.Links.Where(l => (l.CncTypeId & cncTypeFilter) != 0).SendToCNC(decimal.ToSingle(p.Value), ParameterHandler));

                if (result.Failure) break;
            }
            return result;
        }

        /// <summary>
        /// Change parameter value
        /// </summary>
        /// <param name="parameterToUpdate"></param>
        /// <returns></returns>
        public Result<MachineParameterItem> Update(MachineParameterToUpdate parameterToUpdate)
        {
            var validator = new MachineParameterToUpdateValidator();
            var result = validator.Validate(parameterToUpdate);

            if (!result.IsValid)
            {
                throw new BusinessValidationException(result.ToErrors(), eventContext: EventContextEnum.Parameter);
            }

            var dbEntity = MachineParameterRepository.Get(parameterToUpdate.Id);
            if (dbEntity == null)
                return Result.Fail<MachineParameterItem>(ErrorCodesEnum.ERR_GEN002.ToString());

            if (dbEntity.MinimumValue != 0 && parameterToUpdate.Value < dbEntity.MinimumValue)
                return Result.Fail<MachineParameterItem>(ErrorCodesEnum.ERR_GEN001.ToString());

            if (dbEntity.MaximumValue != 0 && parameterToUpdate.Value > dbEntity.MaximumValue)
                return Result.Fail<MachineParameterItem>(ErrorCodesEnum.ERR_GEN001.ToString());

            parameterToUpdate.Value = ConvertToHelper.Convert(
                                conversionSystemFrom: parameterToUpdate.CurrentConversionSystem
                                , conversionSystemTo: MeasurementSystemEnum.MetricSystem
                                , dataFormat: (AttributeDataFormatEnum)dbEntity.DataFormatId
                                , value: parameterToUpdate.Value).Value;

            using (var uow = UnitOfWorkFactory.GetOrCreate(UserSession))
            {
                MachineParameterRepository.Attach(uow);
                var serviceResult = InnerUpdateParameter(dbEntity, parameterToUpdate.Value);
                uow.Commit();
                return serviceResult.OnSuccess(updatedParameter =>
                {
                    var machineParameter = Mapper.Map<MachineParameterItem>(serviceResult.Value);
                    machineParameter.ProtectionLevel = TypeDescriptor.GetConverter(typeof(ProtectionLevelEnum))
                                .ConvertTo(null, CultureInfo.InvariantCulture, dbEntity.ProtectionLevel, typeof(string))
                                .ToString();
                    return machineParameter;
                })
                .OnFailure(error => Result.Fail<MachineParameterItem>(error.Errors));
            }
        }

        /// <summary>
        /// Creates the set of parameters for import
        /// </summary>
        private Result<IEnumerable<MachineParameterToImport>> CreateParametersToImport()
        {
            var progressMessage = "Reading Parameters";

            EventHubClient.ProgressEvent(new ProgressEvent(progressMessage));

            var parametersDirectory = MachineConfigurationService.GetParametersDirectory();
            try
            {
                var parameters = Directory.EnumerateFiles(parametersDirectory, "*.parameters.json", SearchOption.AllDirectories)
                    .OrderBy(filename => filename)
                    .SelectMany(filePath =>
                    {
                        try
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<MachineParameterToImport>>(File.ReadAllText(filePath));
                        }
                        catch (JsonReaderException ex)
                        {
                            throw new JsonReaderException($"{ex.Message} file '{Path.GetFileName(filePath)}'");
                        }
                    });

                //Check if there is any
                if (parameters.Any() is false)
                {
                    return Result.Fail<IEnumerable<MachineParameterToImport>>("No parameter defined. Can't start application.");
                }

                // Content validation
                var validator = new MachineParameterToImportValidator();
                foreach (var parameter in parameters)
                {
                    var result = validator.Validate(parameter);
                    if (!result.IsValid)
                    {
                        return Result.Fail<IEnumerable<MachineParameterToImport>>(result.ToErrorString());
                    }
                }

                //Check if there are duplicated Keys
                var duplicatedKeys = parameters.Duplicates(a => a.Code);
                if (duplicatedKeys.Any())
                {
                    return Result.Fail<IEnumerable<MachineParameterToImport>>($"Parameters duplicated: {string.Join(",", duplicatedKeys)}");
                }

                return Result.Ok(parameters);
            }
            catch (JsonSerializationException)
            {
                // Encountered an invalid format error
                return Result.Fail<IEnumerable<MachineParameterToImport>>(ErrorCodesEnum.ERR_GEN006.ToString());
            }
        }

        /// <summary>
        /// Importazione Parametri e Gruppi
        /// </summary>
        /// <param name="userSession"></param>
        /// <param name="virtualHostType"></param>
        /// <returns>Ritorna la lista dei codici parametri e nome gruppi da importare</returns>
        private Result ImportParametersAndGroups()
        {
            var parameterNodes = Directory.EnumerateFiles(MachineConfigurationService.GetParametersDirectory(), "*.node.json", SearchOption.AllDirectories)
                .OrderBy(filename => filename)
                .Select(filePath =>
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<MachineParameterNodeConfiguration>(File.ReadAllText(filePath));
                    }
                    catch (JsonReaderException ex)
                    {
                        throw new JsonReaderException($"{ex.Message} file '{Path.GetFileName(filePath)}'");
                    }
                })
                .GroupBy(group => group.NodeName)
                .Select(group => group.Aggregate((merged, current) => Mapper.Map(source: current, destination: merged)))
                .ToArray();

            // Recupero lista parametri da importare
            var parametersFromFileResult = CreateParametersToImport();
            if (parametersFromFileResult.Failure) return parametersFromFileResult;

            var parameterNames = parameterNodes.SelectMany(node => node.Groups.SelectMany(group => group.ParameterCodes)).ToHashSet();

            var parametersFromFile = parametersFromFileResult.Value
                .Where(p => parameterNames.Contains(p.Code))
                .Select(p => Mapper.Map<MachineParameter>(p));

            var parametersInDb = MachineParameterRepository
                .FindBy(parameter => parameterNames.Contains(parameter.Code))
                .ToDictionary(p => p.Code);

            var existingParameters = new List<MachineParameter>();
            if (parametersInDb.Any())
            {
                foreach (var parameter in parametersFromFile)
                {
                    if (parametersInDb.TryGetValue(parameter.Code, out var founded))
                    {
                        founded.MaximumValue = parameter.MaximumValue;
                        founded.MinimumValue = parameter.MinimumValue;
                        founded.Category = parameter.Category;
                        founded.DataFormatId = parameter.DataFormatId;
                        founded.ProtectionLevel = parameter.ProtectionLevel;
                        founded.ImageCode = parameter.ImageCode;
                        founded.IconCode = parameter.IconCode;
                        founded.DescriptionLocalizationKey = parameter.DescriptionLocalizationKey;
                        founded.HelpLocalizationKey = parameter.HelpLocalizationKey;
                        founded.DefaultValue = parameter.DefaultValue;
                        // TODO
                        //founded.PreserveUpdatedOn = true;
                        existingParameters.Add(founded);
                    }
                }
            }

            var existingParametersDictionary = existingParameters.ToDictionary(p => p.Code);

            var parameterLinksToImport = parametersFromFile.Where(p => existingParametersDictionary.ContainsKey(p.Code))
                .Select(p => new { existingParametersDictionary[p.Code].Id, Links = p.Links.ToList() });

            var parametersToAdd = parametersFromFile.Where(p => !existingParametersDictionary.ContainsKey(p.Code));

            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            try
            {
                MachineParameterRepository.Attach(uow);

                uow.BeginTransaction();

                // Associazioni con variabili CNC
                var parameterLinksToKeep = new List<long>();
                foreach (var parameterLinkInfo in parameterLinksToImport)
                {
                    var linkCount = parameterLinkInfo.Links.Count;
                    for (var linkId = 0; linkId < linkCount; ++linkId)
                    {
                        var link = parameterLinkInfo.Links[linkId];
                        link.ParameterId = parameterLinkInfo.Id;
                        link.LinkId = linkId;
                        parameterLinksToKeep.Add(MachineParameterRepository.AddOrUpdateLink(link).Id);
                    }
                }

                MachineParameterRepository.CleanUp(existingParameters.Select(p => p.Id).ToHashSet(), parameterLinksToKeep.ToHashSet());

                // Aggiorno i parametri esistenti
                //foreach (var parameter in existingParameters)
                //{
                //    parameter.PreserveUpdatedOn = true;
                //    MachineParameterRepository.Update(parameter);
                //}

                MachineParameterRepository.BulkUpdate(existingParameters);

                // Aggiungo i nuovi parametri
                //foreach (var parameter in parametersToAdd)
                //{
                //    MachineParameterRepository.Add(parameter);
                //}
                MachineParameterRepository.BulkInsert(parametersToAdd);

                uow.Commit();
                uow.CommitTransaction();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                uow.RollBackTransaction();
                return Result.Fail<ParametersGroupsImportResult>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private Result<MachineParameter> InnerUpdateParameter(MachineParameter dbEntity, decimal value)
        {
            dbEntity.Value = value;
            dbEntity.NumberOfUpdates++;
            MachineParameterRepository.Update(dbEntity);
            if (dbEntity.Links.Any())
            {
                //Update CNC Variables
                var cncResult = dbEntity.Links.SendToCNC(decimal.ToSingle(dbEntity.Value), ParameterHandler);
                if (cncResult.Failure)
                    return Result.Fail<MachineParameter>(cncResult);

            }
            return Result.Ok(dbEntity);
        }
        public Dictionary<string, decimal> GetLinearNestingParameters()
        {
            return typeof(ParameterCodes).GetFields().Select(member =>
                    {
                        // Recupero i fields che hanno un'attributo che indica che servono per il linear nesting
                        if (member.GetCustomAttributes(typeof(LinearNestingParameterAttribute), false).Any())
                        {
                            // Recupero il valore del fields
                            return (string)member.GetValue(null);
                        }
                        else
                            return null;
                    })
                   .Where(member => member != null)
                   .Select(m =>
                   {
                       // Recupero il valore del parametro.
                       // @SimonaTODO: in futuro pensare a utilizzare un'unica chiamata per ottenere tutti i parametri (al momento sono solo due quelli necessari) 
                       var par = MachineParameterRepository.Get(m);
                       return (name: m, paramValue: par!=null?par.Value:0);
                   })
                 .ToDictionary(x=> x.name, x => x.paramValue);
        }
    }
}