namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Configuration.Models;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Macro;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.MachineManagement.Application.Enums;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Configuration;
    using Mitrol.Framework.MachineManagement.Application.Models.Production.Pieces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Class that provides access to the machine configuration.
    /// </summary>
    public sealed class MachineConfigurationService : MachineManagementBaseService
                        , IMachineConfigurationService, IBootableService
    {
        private IApplicationSettingRepository ApplicationSettingRepository => ServiceFactory.GetService<IApplicationSettingRepository>();

        public static RootConfiguration _configurationRoot;
        
        /// <summary>
        /// Default DI constructor.
        /// </summary>
        /// <param name="serviceFactory">A DI service factory reference.</param>
        public MachineConfigurationService(IServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }

        private List<ImportExportExtension> GetImportTypesLocalized(string[] fileTypesImport)
        {
            var typeList = new List<ImportExportExtension>();
            foreach (var type in fileTypesImport)
            {
                if (Enum.TryParse<ImportFileTypeEnum>(type, out var typeEnum))
                {
                    var fileExtensionAttribute = DomainExtensions.GetEnumAttribute<ImportFileTypeEnum, ExtensionAttribute>(typeEnum);
                    if (fileExtensionAttribute != null)
                    {
                        typeList.Add(new ImportExportExtension(
                            fileExtensionAttribute.FileExtension,
                            $"{DomainExtensions.GENERIC_LABEL}_FILETYPE_{type}"
                        ));
                    }
                }
            }

            return typeList;
        }

        private Result GetMacroConfigurationFileContent()
        {
            foreach (var type in Enum.GetValues(typeof(MacroTypeEnum)).Cast<MacroTypeEnum>()
                .Where(macroType => macroType != MacroTypeEnum.NotDefined && macroType != MacroTypeEnum.MacroAll))
            {
                // Percorso relativo a dove trovare le infomazioni sulle macro
                var relativePath = string.Empty;
                // Nome del file di configurazione delle macro
                var configName = string.Empty;
                // Percorso relativo a dove trovare le infomazioni delle configurazioni degli attributi sulle macro
                var attributesConfigRelativePath = string.Empty;
                // Nome del file di configurazione degli attributi delle macro
                var attributesConfigFileName = string.Empty;

                var macroConfigSection = type switch
                {
                    MacroTypeEnum.MacroCut => ConfigurationRoot.Programming.MacroCut,
                    MacroTypeEnum.MacroMill => ConfigurationRoot.Programming.MacroMill,
                    MacroTypeEnum.MacroRobot => ConfigurationRoot.Programming.MacroCope,
                    _ => throw new NotSupportedException(),
                };

                if (macroConfigSection == null) continue;

                //Recupero il percorso relativo del file di configurazione delle macro
                relativePath = macroConfigSection.BaseFolder;
                //Recupero il nome del file di configurazione delle macro
                configName = macroConfigSection.ConfigurationFilename;
                //Recupero il nome del file di configurazione degli attributi delle macro
                attributesConfigFileName = macroConfigSection.AttributesConfigurationFilename;
                //Recupero il percorso relativo a dove trovare le infomazioni sulle macro sugli attributi delle macro
                attributesConfigRelativePath = macroConfigSection.AttributesConfigurationFolder;

                //Root applicativo 
                var startupFolder = DomainExtensions.GetStartUpDirectoryInfo().FullName;

                var filename = $"{startupFolder}\\{relativePath}\\{ configName}";
                //Se il file Esiste
                if (File.Exists(filename))
                {
                    //Leggo la configurazione dal file
                    var lines = File.ReadAllText(filename);
                    try
                    {
                        //serializzo oggetto 
                        var clusters = JsonConvert.DeserializeObject<List<MacroCluster>>(lines);

                        MacroConfigurationDictionary.Instance.AddOrUpdate(type, clusters);
                    }
                    catch (Exception exc)
                    {
                        return Result.Fail(exc.Message.ToString());
                    }
                }

                filename = $"{startupFolder}\\{attributesConfigRelativePath}\\{attributesConfigFileName}";
                //Se il file Esiste
                if (File.Exists(filename))
                {

                    //Leggo la configurazione dal file
                    var lines = File.ReadAllText(filename);
                    try
                    {
                        //serializzo oggetto 
                        var attributes = JsonConvert.DeserializeObject<List<MacroAttributeStructure>>(lines);
                        MacroConfigurationDictionary.Instance.AddOrUpdate(type, attributes);
                    }
                    catch (Exception exc)
                    {
                        return Result.Fail(exc.Message.ToString());
                    }
                }
            }

            return Result.Ok();
        }

        private string GetPathLocalizationKey(string suffix)
            => $"{DomainExtensions.GENERIC_LABEL}_PATHTYPE_{suffix}";
        /**
         * Returns the local absolute path containing translations
         */
        private string GetTranslationsPath()
            => Path.Combine(DomainExtensions.GetStartUpDirectoryInfo().FullName, "translations");

        /// <summary>
        /// Boot Macchina 
        /// </summary>
        /// <param name="userSession"></param>
        /// <returns>True se il boot è andato a buon fine. False altrimenti</returns>
        public Result Boot(IUserSession userSession)
        {
            try
            {
                EventHubClient.ProgressEvent(new ProgressEvent("Reading Configuration"));
                var configurationDirectory = GetMachineConfigurationDirectory();
                var configurationElements = Directory.EnumerateFiles(configurationDirectory,
                                                                     "*.configuration.json",
                                                                     SearchOption.AllDirectories)
                                    .Concat(Directory.EnumerateFiles(configurationDirectory,
                                                                     "*.paths.json",
                                                                     SearchOption.AllDirectories))
                    .OrderBy(filePath => filePath)
                    .Select(filePath =>
                    {
                        try
                        {
                            return JsonConvert.DeserializeObject<RootConfiguration>(File.ReadAllText(filePath));
                        }
                        catch (JsonReaderException ex)
                        {
                            throw new JsonReaderException($"{ex.Message} file '{Path.GetFileName(filePath)}'");
                        }
                    });

                if (configurationElements.Any() is false)
                {
                    return Result.Fail($"Machine configuration not found in '{configurationDirectory}'");
                }

                _configurationRoot = configurationElements.ToArray()
                    .Aggregate((merged, current) => Mapper.Map(source: current, destination: merged));

                if (_configurationRoot is null)
                {
                    return Result.Fail($"Machine configuration not found in '{configurationDirectory}'");
                }

                var validationResult = ServiceFactory.GetService<RootConfigurationValidator>()
                    .Validate(_configurationRoot);

                if (validationResult.IsValid is false)
                {
                    return Result.Fail(validationResult.Errors.GetErrorDetails());
                }

                //Recupero la regular expression per il nome file delle informazioni addizionali legate alle notifiche
                var notificationOverridesDir = GetNotificationOverridesDirectory();

                //Resetta il dizionario
                NotificationOverrides.Instance.Reset();

                //Se esistono files che rispettano la regular expression 
                //allora importo le informazioni nel dizionario interno
                foreach (var filePath in Directory.EnumerateFiles(notificationOverridesDir, "*.override.json"))
                {
                    //Leggo il contenuto del file
                    var jsonContent = File.ReadAllText(filePath);
                    if (!string.IsNullOrEmpty(jsonContent))
                    {
                        try
                        {
                            //Recupero il dizionario presente nel file
                            var dict = JsonConvert.DeserializeObject<Dictionary<string, MachineNotificationConfiguration>>(jsonContent);
                            foreach (var key in dict.Keys)
                            {
                                //Lo importo nelle additionalInfos
                                if (dict.TryGetValue(key, out var values))
                                {
                                    NotificationOverrides.Instance.Add(key, values);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }

                //carico le configurazioni delle macro.
                var resultMacro = GetMacroConfigurationFileContent();
                if (resultMacro.Failure)
                    return resultMacro;
            }
            catch (Exception ex)
            {
                return Result.Fail($"{"Reading Configuration"} KO ({ex.InnerException?.Message ?? ex.Message})");
            }

            return Result.Ok();
        }

        /// <summary>
        /// Operazione di cleanup
        /// </summary>
        /// <param name="userSession"></param>
        public Result CleanUpBeforeBoot(IUserSession userSession)
        {
            return Result.Ok();
        }

        /// <summary>
        /// Lista di tutti i settings gestiti dall'applicazione
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationSettingItem> GetApplicationSettings()
        {
            return Mapper.Map<IEnumerable<ApplicationSettingItem>>(ApplicationSettingRepository.GetAll());
        }

        /// <summary>
        /// Recupera il valore dell'application setting in input
        /// </summary>
        /// <param name="applicationSettingKey"></param>
        /// <returns></returns>
        public string GetApplicationSettingValue(ApplicationSettingKeyEnum applicationSettingKey)
        {
            return ApplicationSettingRepository.Get(applicationSettingKey)?.DefaultValue;
        }

        /// <summary>
        /// Get BE and DB Version info
        /// </summary>
        /// <returns></returns>
        public BEVersionInfo GetBEVersions()
        {
            var hostInfo = Dns.GetHostEntry(Dns.GetHostName());

            var networkInfos = from ni in NetworkInterface.GetAllNetworkInterfaces()
                               where ni.OperationalStatus is OperationalStatus.Up
                               && ni.NetworkInterfaceType is NetworkInterfaceType.Wireless80211 or NetworkInterfaceType.Ethernet or (NetworkInterfaceType)53
                               select new NetworkInfo
                               {
                                   InterfaceName = ni.Name,
                                   PhysicalAddress = ni.GetPhysicalAddress().ToString(),
                                   IpAddresses = from unicastAddress in ni.GetIPProperties().UnicastAddresses
                                                 where unicastAddress.Address.AddressFamily is AddressFamily.InterNetwork or AddressFamily.InterNetworkV6
                                                 select new IPNetworkAddress(unicastAddress)
                               };

            BEVersionInfo versionInfo = new BEVersionInfo();
            string versionDir = Path.Combine(DomainExtensions.GetStartUpDirectoryInfo().FullName, "bin", "services"
                                    , "version.json");
            if (File.Exists(versionDir))
            {
                versionInfo = JsonConvert.DeserializeObject<BEVersionInfo>(File.ReadAllText(versionDir));
            }

            versionInfo.BEVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            versionInfo.DBVersion = GetApplicationSettingValue(ApplicationSettingKeyEnum.DbVersion);
            versionInfo.HostName = hostInfo.HostName;
            versionInfo.NetworkInfos = networkInfos;

            return versionInfo;
        }

        /// <summary>
        /// Recupera la directory di configurazione
        /// </summary>
        public string GetConfigurationDirectory() => Path.Combine(DomainExtensions.GetStartUpDirectoryInfo().ToString(), "config");


        /// <summary>
        /// Get Configuration file content from settingkey
        /// </summary>
        /// <returns></returns>
        public Result<string> GetConfigurationFileContent(ApplicationSettingKeyEnum fileDefinitionName)
        {
            var filename = ApplicationSettingRepository.Get(fileDefinitionName);

            var machineConfigDir = GetConfigurationDirectory();

            if (filename == null || string.IsNullOrEmpty(machineConfigDir))
                return Result.Fail<string>(ErrorCodesEnum.ERR_STG001.ToString());

            var filePath = Path.Combine(machineConfigDir, filename.DefaultValue);

            if (!File.Exists(filePath))
                return Result.Fail<string>(ErrorCodesEnum.ERR_STG002.ToString());

            using var reader = new StreamReader(filePath);

            var content = reader.ReadToEnd();
            return string.IsNullOrEmpty(content)
                    ? Result.Fail<string>(ErrorCodesEnum.ERR_STG003.ToString())
                    : Result.Ok(content);
        }

        /**
         * Returns the configured cultures.
         */
        public IEnumerable<CultureItem> GetCultures()
        {
            var cfgFile = Path.Combine(GetTranslationsPath(), "_cultures.json");
            if (File.Exists(cfgFile))
            {
                return JsonConvert.DeserializeObject<IEnumerable<CultureItem>>(File.ReadAllText(cfgFile));
            }
            else
            {
                throw new ArgumentException("Cultures  configuration file (_cultures.json) is missing!");
            }
        }

        public Dictionary<MachineFeaturesEnum, object> GetFeatures()
        {
            return ConfigurationRoot.ToFeatures()
                .ToDictionary(item => item.Key, item => item.Value);
        }

        public FontConfiguration GetFontConfiguration(FontConfigurationFilter filter)
        {
            // Recupera la lista dei font(indice-nome) per l'unità di marcatura indicata
            var fontLists = GetFontsList(filter.MarkingUnit)
                    .ToDictionary(f => f.Id, f => f.Value);



            // Recupero il nome del font corrispondente all'indice 
            if (fontLists.TryGetValue(filter.FontIndex, out var fontName))
            {
                // Quando la configurazione esiste recupero anche i fonts, e il size dell'unità di marcatura scelta
                // Preventivamente verifico se la configurazione richiesta sia già caricata in memoria 
                if (FontsConfigurationMarkingUnitDictionary.Instance.Get(filter.MarkingUnit, filter.ScribingType, fontName) == null)
                {
                    var UnitMarkingConfiguration = filter.MarkingUnit switch
                    {
                        MarkingUnitTypeEnum.Plasma => ConfigurationRoot.Programming.Plasma,
                        MarkingUnitTypeEnum.Reajet => ConfigurationRoot.Programming.ReaJet,
                        MarkingUnitTypeEnum.Reajet_F => ConfigurationRoot.Programming.ReaJetF,
                        MarkingUnitTypeEnum.Drill => ConfigurationRoot.Programming.Scribing,
                        _ => throw new NotSupportedException(),
                    };

                    // Root applicativo 
                    var startupFolder = DomainExtensions.GetStartUpDirectoryInfo().FullName;

                    // Se il file della configurazione dei fonts esiste 
                    if (File.Exists($"{startupFolder}\\{UnitMarkingConfiguration.FontsDefinitionFileName}"))
                    {
                        // Leggo la configurazione dal file dei fonts
                        var lines = File.ReadAllText($"{startupFolder}\\{UnitMarkingConfiguration.FontsDefinitionFileName}");
                        try
                        {
                            // Serializzo oggetto relativo alla configurazione dei fonts.
                            var fontsConfiguration = (FontsConfiguration)JsonConvert.DeserializeObject<FontsConfiguration>(lines);

                            // Recupero le configurazioni dei font che sono abbinate all tipo di unità scelta.
                            var fontsConfigForMarkingTypeUnit = fontsConfiguration.GetFonts().Where(f => UnitMarkingConfiguration.Fonts.Contains(f.Key));

                            // Recupero il path del file delle macro: tengo conto che per alcuni tipi di marcatura(MarkingUnitTypeEnum) non esiste la relazione ScribingType-Macro Path
                            var scribingPath = string.Empty;

                            if (UnitMarkingConfiguration.Paths != null)
                                scribingPath = filter.ScribingType switch
                                {
                                    ScribingMarkingTypeEnum.Standard => UnitMarkingConfiguration.Paths.Standard,
                                    ScribingMarkingTypeEnum.LinkedCharacters => UnitMarkingConfiguration.Paths.LinkedCharacters,
                                    _ => string.Empty,
                                };

                            // Inserisco la configurazione dei font trovati nel dizionario in memoria per poterlo usare anche per altre richieste  
                            fontsConfigForMarkingTypeUnit.ForEach(config =>
                            {
                                // Mi memorizzo il percorso dei font, dipendente dal tipo di scribing, perchè se necessario potrò caricare la macro(e le relative op) in un secondo tempo
                                config.Value.ScribingPath = !string.IsNullOrEmpty(scribingPath) ? $"{startupFolder}\\{scribingPath}" : string.Empty;
                                FontsConfigurationMarkingUnitDictionary.Instance.AddOrUpdate(filter.MarkingUnit, filter.ScribingType, config.Key, config.Value);
                            });
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                }
                return FontsConfigurationMarkingUnitDictionary.Instance.Get(filter.MarkingUnit, filter.ScribingType, fontName);
            }
            return null; // Non è stato trovato un elemento valido oppure sono più elementi e non è possibile sapere tra quali scegliere.  
        }

        public Task<IEnumerable<ListBoxItem>> GetFontsConfigurationForListBoxAsync(ToolTypeEnum toolType)
        {
            return Task.Factory.StartNew(() =>
            {
                var fonts = GetFontsList(toolType.GetMarkingUnitType());
                return Mapper.Map<IEnumerable<ListBoxItem>>(fonts);
            });
        }

        public List<BaseInfoItem<int, string>> GetFontsList(MarkingUnitTypeEnum markingUnit)
        {
            var fontsList = new List<BaseInfoItem<int, string>>();
            if (markingUnit == MarkingUnitTypeEnum.NotSpecified) return fontsList;
            var UnitMarkingConfiguration = markingUnit switch
            {
                MarkingUnitTypeEnum.Plasma => ConfigurationRoot.Programming.Plasma,
                MarkingUnitTypeEnum.Reajet => ConfigurationRoot.Programming.ReaJet,
                MarkingUnitTypeEnum.Reajet_F => ConfigurationRoot.Programming.ReaJetF,
                MarkingUnitTypeEnum.Drill => ConfigurationRoot.Programming.Scribing,
                _ => throw new ArgumentException("Invalid value", nameof(markingUnit)),
            };

            //Root applicativo 
            var startupFolder = DomainExtensions.GetStartUpDirectoryInfo().FullName;
            //Se il file dei fonts esiste 
            if (File.Exists($"{startupFolder}\\{UnitMarkingConfiguration.FontsDefinitionFileName}"))
            {
                //Leggo la configurazione dal file dei fonts
                var lines = File.ReadAllText($"{startupFolder}\\{UnitMarkingConfiguration.FontsDefinitionFileName}");
                try
                {
                    //serializzo oggetto 
                    var fontsConfiguration = (FontsConfiguration)JsonConvert.DeserializeObject<FontsConfiguration>(lines);
                    //recupero le configurazioni dei font che sono abbinate all tipo di unità scelta.
                    return fontsConfiguration.GetFonts().Where(f => UnitMarkingConfiguration.Fonts.Contains(f.Key))
                        .Select(font => new BaseInfoItem<int, string>() { Id = font.Value.Index, Value = font.Key })
                        .ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return fontsList;
        }


        /**
         * Torna il contenuto di un certo path: sottocartelle + files.
         */
        public IEnumerable<FileSystemItem> GetImportExportPathContent(PathContentParam param)
        {
            var extension = DomainExtensions.GetImportExportExtension(param.type);
            if (string.Compare(param.basePath, "") == 0)
                return new List<FileSystemItem>();

            var res = new List<FileSystemInfo>();
            var searchPattern = $"*.{extension}";
            var di = new DirectoryInfo(param.basePath);
            if (di.Exists)
            {
                if (param.includeFolders)
                {
                    res.AddRange(
                        di.GetDirectories()
                        .Where(i => !i.Attributes.HasFlag(FileAttributes.Hidden))
                        .OrderNatural(d => d.Name)
                    );
                }
                if (param.includeFiles)
                {
                    res.AddRange(
                       di.GetFiles(searchPattern)
                        .Where(i => !i.Attributes.HasFlag(FileAttributes.Hidden))
                        .OrderNatural(d => d.Name));
                }
            }
            return res.Select(fsi => new FileSystemItem(fsi, extension));
        }

        /**
         * Torna una lista di percorsi accessibili per import/export così composta:
         * - percorso locale configurato in General.LOCAL_IMPORTEXPORTFOLDER
         * - percorso di rete configurato in General.REMOTE_IMPORTEXPORTFOLDER
         * - tutti dischi locali (dischi fissi e USB)
         */
        public IEnumerable<ImportExportPath> GetImportExportPaths()
        {
            var paths = new List<ImportExportPath>();

            // Cartelle di import/export configurate
            var localImportFolders = ConfigurationRoot.ImportExport.Folders;

            foreach (var path in localImportFolders)
            {
                if (path != string.Empty)
                {
                    // compongo il percorso assoluto
                    var fullPath = Path.IsPathFullyQualified(path)
                        ? path
                        : Path.GetFullPath(Path.Combine(DomainExtensions.GetStartUpDirectoryInfo().FullName, path));
                    var folderType = fullPath.StartsWith(@"\\") ? DriveType.Network : DriveType.Fixed;
                    // Aggiungi percorso locale
                    paths.Add(new ImportExportPath(
                        id: paths.Count + 1,
                        path: fullPath,
                        type: folderType,
                        displayName: GetPathLocalizationKey(folderType.ToString().ToUpper()),
                        available: Directory.Exists(fullPath)
                    ));
                }
            }

            // ottengo la lista di tutti i dispositivi locali rimovibili
            var removableDriveList = DriveInfo
                .GetDrives()
                .Where(d => d.IsReady && (d.DriveType == DriveType.Removable));

            foreach (var driveUri in removableDriveList)
            {
                var uri = new Uri(driveUri.Name);
                paths.Add(new ImportExportPath(
                    id: paths.Count + 1,
                    path: uri.AbsolutePath,
                    type: driveUri.DriveType,
                    displayName: GetPathLocalizationKey(driveUri.DriveType.ToString().ToUpper())
                ));
            }

            return paths;
        }

        public Task<IEnumerable<ImportExportPath>> GetImportExportPathsAsync()
        {
            return Task.Factory.StartNew(() => GetImportExportPaths());
        }

        /// <summary>
        /// Recupera la directory di configurazione macchina
        /// </summary>
        public string GetMachineConfigurationDirectory()
        {
            var machineDir = Path.Combine(GetConfigurationDirectory(), "machine");

            // se non esiste la cartella la creo
            if (Directory.Exists(machineDir) is false)
                Directory.CreateDirectory(machineDir);

            return machineDir;
        }

        public Result<HashSet<AttributeConfigurationItem>> GetMacroAttributes(MacroConfigurationFilter filter)
        {
            // Configurazione delle macro del tipo scelto
            var MacrosConfiguration = MacroConfigurationDictionary.Instance.Get(filter.MacroType);

            // Se non è stato configurato il tipo di macro richiesto interrompo con errore
            if (MacrosConfiguration == null) throw new NotSupportedException($"Unable to find configuration for macro type '{filter.MacroType}'");

            // Prendo solo la configurazione della macro con il profilo corrente e il nome indicato
            var Macro = MacrosConfiguration.Where(cluster => cluster.ProfileType == filter.ProfileType)
                .SelectMany(cluster => cluster.Macros)
                .Where(macro => (macro.Enable == true) && string.Compare(macro.Name, filter.MacroName) == 0).SingleOrDefault();

            // Se non è stato configurato il tipo di macro richiesto interrompo con errore
            if (Macro == null) throw new NotSupportedException($"Unable to find of type '{filter.MacroType}' with name '{filter.MacroName}'");

            // Trovo tutti gli attributi che sono stati definiti nel file di configurazione della macro specifica
            var attributesDefined = Macro.Attributes.Select(attr => new AttributeConfigurationItem(attr)).ToHashSet();

            // Ottengo la struttura del tipo di macro scelto
            var structuresAll = MacroConfigurationDictionary.Instance.GetStructure(filter.MacroType);

            // Recupero gli attributi che sono sempre presenti(da definizione della struttura)
            // oppure quelli che sono configurabili lato "Attributo"
            // che sono presenti nella lista degli attributi di configurazione della macro 
            var attributeConfigElements = structuresAll.Where(element => (element.ConfigurationType == ConfigurationTypeEnum.AlwaysOn)
                                  || ((element.ConfigurationType == ConfigurationTypeEnum.AttributeConfiguration)
                                      && (attributesDefined.Where(attr => attr.DisplayName == element.AttributeDefinitionName).Any())));
            // Trasformo e aggiungo la localizzazione se esiste
            var attributeElements = attributeConfigElements.Select(attr =>
            {
                var attribute = new AttributeConfigurationItem(attr);

                var attributeDefined = attributesDefined.Where(a => a.DisplayName == attr.AttributeDefinitionName).FirstOrDefault();
                if (attributeDefined != null)
                {
                    attribute.LocalizationKey = attributeDefined.LocalizationKey;
                    attribute.DataFormat = attributeDefined.DataFormat;
                }

                //assegno il nome della Macro
                if (attribute.DisplayName == AttributeDefinitionEnum.MacroName)
                    attribute.Value = Macro.Name;
                //assegno il nome dell'immagine
                if (attribute.DisplayName == AttributeDefinitionEnum.Image)
                    attribute.Value = Macro.ImageCode;
                return attribute;
            }
            );
            // Recupero gli attributi che sono configurabili lato "Gruppo"
            // che sono presenti nella lista dei gruppi di configurazione della macro specifica
            var attributesOfGroupToHaveConfig = structuresAll.Where(element => element.ConfigurationType == ConfigurationTypeEnum.GroupConfiguration)
                                                             .GroupBy(st => st.GroupName)
                                                             .Where(g => Macro.Technologies.Where(m => string.Compare(m.GroupName, g.Key) == 0).Any())
                                                             .Select(g => g.ToList());

            // trasformo e aggiungo la localizzazione se esiste
            var attributesOfGroupToHave = attributesOfGroupToHaveConfig.SelectMany(g => g).Select(attr =>
             {
                 var attribute = new AttributeConfigurationItem(attr)
                 {
                     LocalizationKey = attributesDefined.Where(a => a.DisplayName == attr.AttributeDefinitionName).FirstOrDefault() != null ?
                     attributesDefined.Where(a => a.DisplayName == attr.AttributeDefinitionName).FirstOrDefault().LocalizationKey : ""
                 };
                 var group = Macro.Technologies.FirstOrDefault(tech => tech.GroupName == attr.GroupName);
                 attribute.SourceValues = ((group.Values != null)
                                    && (!group.Values.FirstOrDefault(value => value.Key == attribute.DisplayName.ToString()).Equals(default(KeyValuePair<string, List<string>>)))) ?
                                         group.Values.FirstOrDefault(value => value.Key == attribute.DisplayName.ToString()).Value
                                        : null;
                 return attribute;
             });

            // Recupero gli attributi che sono configurati lato "Gruppo"
            // che non sono presenti nella lista dei gruppi di configurazione della macro specifica
            var attributesOfGroupNotPresent = structuresAll.Where(element => element.ConfigurationType == ConfigurationTypeEnum.GroupConfiguration)
                                                           .GroupBy(st => st.GroupName)
                                                           .Where(g => Macro.Technologies.Where(m => m.GroupName != g.Key).Any())
                                                           .Select(g => g.ToList())
                                                           .SelectMany(g => g)
                                                           .ToList();

            //recupero gli attributi eccezione che sono configurabili lato "Gruppo" ma
            //che sono presenti nella lista degli attributi di configurazione della macro specifica
            var attributesExceptionConfig = attributesOfGroupNotPresent.Where(attrNotPres =>
                                  attributesDefined.Where(attr => attr.DisplayName == attrNotPres.AttributeDefinitionName).Any());

            //trasformo e aggiungo la localizzazione se esiste
            var attributesException = attributesExceptionConfig.Select(attr => new AttributeConfigurationItem(attr)
            {
                LocalizationKey = attributesDefined.Where(a => a.DisplayName == attr.AttributeDefinitionName).FirstOrDefault() != null ?
                                attributesDefined.Where(a => a.DisplayName == attr.AttributeDefinitionName).FirstOrDefault().LocalizationKey : ""
            });
            //Ottengo la lista passare al chiamante come concatenazione delle tre liste (attributi + gruppi + eccezione)
            var attributeDefinedComplete = (attributeElements.Concat(attributesOfGroupToHave)).Concat(attributesException).ToHashSet<AttributeConfigurationItem>();

            return Result.Ok(attributeDefinedComplete);
        }

        public bool GetMacroEnabled(MacroConfigurationFilter macroConfigurationFilter)
        {
            var MacroConfiguration = MacroConfigurationDictionary.Instance.Get(macroConfigurationFilter.MacroType);

            //Prendo solo le macro con il profilo corrente, il nome indicato che siano abilitate
            var Macro = MacroConfiguration
                .Where(cluster => cluster.ProfileType == macroConfigurationFilter.ProfileType)
                .SelectMany(cluster => cluster.Macros)
                .Where(macro => (macro.Enable == true) && string.Compare(macro.Name, macroConfigurationFilter.MacroName) == 0).SingleOrDefault();

            return Macro != null;
        }

        /// <summary>
        /// Restituisce la configurazione delle macro (ex .INI) 
        /// </summary>
        /// <returns> dizionario che per ogni tipo di macro disponibile restituisce la sua configurazione</returns>
        public Dictionary<MacroTypeEnum, List<MacroCluster>> GetMacrosConfiguration()
        {

            return Enum.GetValues(typeof(MacroTypeEnum))
                       .Cast<MacroTypeEnum>()
                       .Select(type =>
                          {
                              var config = MacroConfigurationDictionary.Instance.Get(type);
                              if (config != null)
                                  return (type, config);
                              return default;
                          })
                       .Where(val => val != default)
                       .ToDictionary(val => val.type, val => val.config);

        }

        /// <summary>
        /// Get Macros defined for CNC
        /// </summary>
        /// <returns></returns>
        public Task<Result<HashSet<MacroInfoItem>>> GetManagedMacrosAsync(MacroConfigurationFilter filter)
        {
            return Task.Factory.StartNew(() =>
            {
                var definedMacros = new HashSet<MacroInfoItem>();

                var MacroConfiguration = MacroConfigurationDictionary.Instance.Get(filter.MacroType);

                //Prendo solo le macro con il profilo corrente
                var Macros = MacroConfiguration.Where(cluster => cluster.ProfileType == filter.ProfileType);

                var Operationtype = DomainExtensions.GetOperationTypeFromMacroType(filter.MacroType);

                //Recupero le macro abilitate
                definedMacros.UnionWith(Macros.SelectMany(n => n.Macros)
                    .Where(m => m.Enable == true)
                    .Select(m => new MacroInfoItem
                    {
                        MacroName = m.Name,
                        Image = m.ImageCode,
                        Type = Operationtype
                    }));
                return Result.Ok(definedMacros);
            });
        }

        /// <summary>
        /// Get Program Types managed by machine
        /// </summary>
        /// <returns>List of program types that could be created on the machine</returns>
        public IEnumerable<InfoItem<string>> GetManagedProgramTypes()
        {

            var managedProgramTypes = new List<InfoItem<string>>();

            if (ConfigurationRoot.Machine.AnyProgramType)
            {
                var managedTypes = ConfigurationRoot.Machine.ProgramTypes
                        .Where(programTypeConfig => programTypeConfig.Value)
                        .Select(programTypeConfig => programTypeConfig.Key.ToString());

                managedProgramTypes = managedTypes
                    .Select(manageType =>
                    {
                        string localizationKey = manageType.ToUpper();

                        //Recupero il serializzation name per fornire la chiave di localizzazione
                        var programType = Enum.Parse<ProgramTypeEnum>(manageType);

                        var serializationName = programType.GetEnumAttribute<EnumSerializationNameAttribute>();

                        //Assegno la chiave di localizzazione
                        if (serializationName != null)
                        {
                            localizationKey = $"LBL_PROGRAMTYPE_{serializationName.Description.ToUpper()}";
                        }

                        return new InfoItem<string>
                        {
                            Id = Convert.ToInt64(programType),
                            Value = localizationKey
                        };
                    }).ToList();
            }

            return managedProgramTypes;
        }

        /// <summary>
        /// Lista informazioni addizionali
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MachineNotificationConfiguration GetNotificationOverridesConfiguration(string code)
        {
            return NotificationOverrides.Instance.Get(code);
        }
        /// <summary>
        /// Recupera la directory di configurazione degli override per le notifiche macchina
        /// </summary>
        public string GetNotificationOverridesDirectory()
        {
            var notificationsDir = Path.Combine(GetConfigurationDirectory(), "notifications");

            // se non esiste la cartella la creo
            if (Directory.Exists(notificationsDir) is false)
                Directory.CreateDirectory(notificationsDir);

            return notificationsDir;
        }

        /// <summary>
        /// Recupera la directory di configurazione dei parametri
        /// </summary>
        public string GetParametersDirectory()
        {
            var parametersDir = Path.Combine(GetConfigurationDirectory(), "parameters");

            // se non esiste la cartella la creo
            if (Directory.Exists(parametersDir) is false)
                Directory.CreateDirectory(parametersDir);

            return parametersDir;
        }

        public IEnumerable<ListBoxItem> GetSources(AttributeDefinitionEnum attribute, Dictionary<AttributeDefinitionEnum, object> additionalInfo)
        {
            if ((attribute == AttributeDefinitionEnum.FontType) && (additionalInfo != null))
            {
                var toolType = additionalInfo.FirstOrDefault(additionalItem => additionalItem.Key == AttributeDefinitionEnum.ToolType);
                if (!toolType.Equals(default(KeyValuePair<AttributeDefinitionEnum, object>)))
                {
                    if (Enum.TryParse<ToolTypeEnum>(toolType.Value.ToString(), out var TS))
                    {
                        var fonts = GetFontsList(TS.GetMarkingUnitType());
                        return Mapper.Map<IEnumerable<ListBoxItem>>(fonts);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get Configured table list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseInfoItem<int, string>> GetTableTypeList()
        {
            //Esclude gli elementi soggetti a configurazione (ToolTables and Profiles)
            var configuratedTables = Enum.GetValues(typeof(TableSelectionEnum)).Cast<TableSelectionEnum>()
                .Where(@enum => @enum != TableSelectionEnum.ToolTables &&
                        @enum != TableSelectionEnum.Profiles)
                .Select(enumItem =>
                {
                    if (enumItem == TableSelectionEnum.MaterialCodes ||
                            (enumItem == TableSelectionEnum.ToolHolders))
                    {
                        return new BaseInfoItem<int, string>
                        {
                            Id = (int)enumItem,
                            Value = $"{DomainExtensions.GENERIC_LABEL}_{enumItem.ToString().ToUpper()}"
                        };
                    }
                    else
                    {
                        return null;
                    }
                })
                .Where(item => item != null)
                .ToList();

            // Se è stata configurata almeno una unità allora esistono le tabelle collegate
            if ((ConfigurationRoot.Setup.Drill?.AnyUnit ?? false)
                || (ConfigurationRoot.Setup.Pla?.AnyUnit ?? false)
                || (ConfigurationRoot.Setup.Oxy?.AnyUnit ?? false))
            {
                configuratedTables.Add(new BaseInfoItem<int, string>
                {
                    Id = (int)TableSelectionEnum.ToolTables,
                    Value = $"{DomainExtensions.GENERIC_LABEL}_{TableSelectionEnum.ToolTables.ToString().ToUpper()}"
                });
            }

            if (ConfigurationRoot.Machine.Profiles
                    .Any(profile => profile.Key != ProfileTypeEnum.P && profile.Value))
            {
                configuratedTables.Add(new BaseInfoItem<int, string>
                {
                    Id = (int)TableSelectionEnum.Profiles,
                    Value = $"{DomainExtensions.GENERIC_LABEL}_{TableSelectionEnum.Profiles.ToString().ToUpper()}"
                });
            }

            return configuratedTables.OrderBy(item => item.Id);
        }

        public Task<IEnumerable<BaseInfoItem<int, string>>> GetTableTypeListAsync()
        {
            return Task.Factory.StartNew(() => GetTableTypeList());
        }

        /**
         * Return all of the language files for the given culture joined in a collection
         */
        public IDictionary<string, string> GetTranslations(string culture)
        {
            var res = new Dictionary<string, string>();

            var translationsFolder = Path.Combine(GetTranslationsPath(), culture);

            // Ensure that the translations folder exist.
            if (Directory.Exists(translationsFolder))
            {
                foreach (var file in Directory.GetFiles(translationsFolder, "*.json")
                    // Sort the files to allow the latest ones to override any keys in the previous ones.
                    .OrderBy(filename => filename))
                {
                    var content = JsonConvert.DeserializeObject<IDictionary<string, string>>(File.ReadAllText(file));
                    if (content != null)
                    {
                        // merge values (replacing duplicate keys)
                        content.ForEach(i => res[i.Key] = i.Value);
                    }
                }
            }

            return res;
        }

        public IEnumerable<ImportExportExtension> GetTypesFileforImport(ImportExportObjectEnum type)
        {
            var enumValue = (ImportFileTypeEnum)DomainExtensions.GetDefaultEnumValueFromTypename(typeof(ImportFileTypeEnum).AssemblyQualifiedName.ToString());
            var fileTypesImport = new string[] { enumValue.ToString() };
            var extensionList = GetImportTypesLocalized(fileTypesImport);
            foreach (var ext in extensionList)
            {
                ext.Extension = DomainExtensions.GetImportExportExtension(type, ext.Extension);
            }

            return extensionList;
        }

        public bool IsFileValidationEnabled()
        {
            // Il controllo è abilitato solo se la platform del CNC è "RealDevice"
            return ConfigurationRoot.Cnc.Platform.HasValue && ConfigurationRoot.Cnc.Platform.Value == PlatformEnum.RealDevice;
        }

        /// <summary>
        /// Aggiorna il valore nell'applicationSetting
        /// </summary>
        /// <param name="applicationSettingKey"></param>
        /// <param name="defaultValue"></param>
        public void UpdateApplicationSettingValue(ApplicationSettingKeyEnum applicationSettingKey, string defaultValue)
        {
            using (var uow = UnitOfWorkFactory.GetOrCreate(UserSession))
            {
                ApplicationSettingRepository.Attach(uow);
                var res = ApplicationSettingRepository.Update(applicationSettingKey, defaultValue);
                if (res)
                    uow.Commit();
            }
        }

        public IRootConfiguration ConfigurationRoot => _configurationRoot;
    }
}
