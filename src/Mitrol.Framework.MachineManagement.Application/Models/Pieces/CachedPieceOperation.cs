namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using FluentValidation;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Macro;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models.Production.Pieces;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Models.Views;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;


    public class CachePieceOperationComparer : IEqualityComparer<CachedPieceOperation>
    {
        public bool Equals(CachedPieceOperation first, CachedPieceOperation second)
        {
            //First check if both object reference are equal then return true
            if (object.ReferenceEquals(first, second))
            {
                return true;
            }
            //If either one of the object reference is null, return false
            if (object.ReferenceEquals(first, null) || object.ReferenceEquals(second, null))
            {
                return false;
            }


            return first.LineNumber == second.LineNumber
                    && first.Level == second.Level;
        }

        public int GetHashCode(CachedPieceOperation operation)
        {
            if (operation == null)
                return 0;

            return operation.LineNumber.GetHashCode() ^ operation.Level.GetHashCode();
        }
    }

    /// <summary>
    /// Tipologia di inserimento nodo
    /// InsertAfter, InsertBefore => Stesso livello
    /// InsertInto => livello inferiore
    /// </summary>
    public enum InsertModeEnum
    {
        InsertAfter = 1,
        InsertInto = 4
    }

    /// <summary>
    /// Graphics Code Generators for every operation
    /// </summary>
    public class GraphicsCodeGenerators
    {
        public Dictionary<DatabaseDisplayNameEnum, object> CodeGenerators { get; set; }
        public Dictionary<long, Dictionary<DatabaseDisplayNameEnum, object>> SlaveCodeGenerators { get; set; }

        public GraphicsCodeGenerators()
        {
            CodeGenerators = new Dictionary<DatabaseDisplayNameEnum, object>();
            SlaveCodeGenerators = new Dictionary<long, Dictionary<DatabaseDisplayNameEnum, object>>();
        }
    }

    /// <summary>
    /// Cached Operation for Piece
    /// </summary>
    public class CachedPieceOperation
    {
        /// <summary>
        /// Dizionario per la gestione della funzionalità di ReactToChange
        /// in base al tipo di operazione
        /// </summary>
        public static Dictionary<OperationTypeEnum, List<AttributeDefinitionEnum>> AttributesToNotifyChanging = new Dictionary<OperationTypeEnum, List<AttributeDefinitionEnum>>()
        {
            { OperationTypeEnum.PathC, new List<AttributeDefinitionEnum>() {  AttributeDefinitionEnum.ToolType, AttributeDefinitionEnum.UnloadingType } },
            {OperationTypeEnum.Mark, new List<AttributeDefinitionEnum>(){ AttributeDefinitionEnum.ToolType, AttributeDefinitionEnum.ToolForTechnology } },
            {OperationTypeEnum.MCut, new List<AttributeDefinitionEnum>(){ AttributeDefinitionEnum.ToolType, AttributeDefinitionEnum.ToolForTechnology } }
        };

        private Dictionary<int, CachedAdditionalItem> _innerAdditionalItems = new Dictionary<int, CachedAdditionalItem>();
        private OperationTypeEnum? _additionaItemType = null;
        /// <summary>
        /// Get Ordered Cached AdditionalItems
        /// </summary>
        /// <returns></returns>
        public CachedAdditionalItem[] GetCachedAdditionalItems()
        {
            if (_additionaItemType.HasValue && _additionaItemType.Value != OperationTypeEnum.Undefined)
            {
                //Recupero tutti gli item che non sono stati rimossi
                return _innerAdditionalItems?
                    .OrderBy(ai => ai.Key)
                    .Select(ai => ai.Value)
                    .ToArray() ?? Array.Empty<CachedAdditionalItem>();
            }

            return Array.Empty<CachedAdditionalItem>();
        }

        [JsonIgnore()]
        public long Id { get; set; }

        [JsonProperty("LineNumber")]
        public int LineNumber { get; set; }

        /// <summary>
        /// Tipo di operazione
        /// Type
        /// </summary>
        private OperationTypeEnum _type;
        [JsonProperty("Type")]
        public OperationTypeEnum Type {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                if (_additionaItemType == null)
                    _additionaItemType = DomainExtensions.GetEnumAttribute<OperationTypeEnum
                            , RelatedAdditionalItemTypeAttribute>(_type)?.AdditionalItemType
                            ?? OperationTypeEnum.Undefined;
            }
        }
        /// <summary>
        /// Skip dell'operazione
        /// ToBeSkipped
        /// </summary>
        [JsonProperty("ToBeSkipped")]
        public bool ToBeSkipped { get; set; }

        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        [JsonProperty("Level")]
        public int Level { get; set; }

        [JsonProperty("ReactToChangeUrl")]
        public string ReactToChangeUrl { get; set; }

        [JsonProperty("CodeGenerators")] //Dovrebbero essere fatti sulla lista degli attributi non filtrata x non perdere le informazioni
        public Dictionary<DatabaseDisplayNameEnum, object> CodeGenerators { get; internal set; }

        [JsonProperty("GraphicsCodeGenerators")]
        public Dictionary<DatabaseDisplayNameEnum, object> GraphicsCodeGenerators { get; internal set; }

        [JsonProperty("AdditionalItemInfosForGraphics")]
        public IEnumerable<AdditionalItemForGraphics> AdditionalItemInfosForGraphics { get; internal set; } 

        [JsonProperty("Attributes")]
        public IEnumerable<BaseGroupItem<ClusteredAttributeDetailItem>> Attributes { get; set; }

        [JsonProperty("Icons")]
        public IEnumerable<string> Icons { get; set; }

        [JsonProperty("SuggestedFormat")]
        public string SuggestedFormat { get; set; }

        [JsonProperty("IsCollapsed")]
        public bool IsCollapsed { get; set; }

        [JsonProperty("ParentId")]
        public long ParentId { get; set; }

        [JsonProperty("ParentLineNumber")]
        public int ParentLineNumber { get; set; }

        [JsonProperty("AdditionalItems")]
        public IEnumerable<CachedAdditionalItem> AdditionalItems {
            get
            {
                return GetCachedAdditionalItems();
            }
            set {
                _innerAdditionalItems = value != null
                    ? value.ToDictionary(ai => ai.Index)
                    : new Dictionary<int, CachedAdditionalItem>();
            } 
        } 

        [JsonProperty("Status")]
        public OperationRowStatusEnum Status {get;set;}

        [JsonProperty("RemovedAdditionalItems")]
        public List<long> RemoveAdditionalItems { get; internal set; }

        public CachedPieceOperation()
        {
            CodeGenerators = new Dictionary<DatabaseDisplayNameEnum, object>();
            GraphicsCodeGenerators = new Dictionary<DatabaseDisplayNameEnum, object>();
            AdditionalItemInfosForGraphics = Array.Empty<AdditionalItemForGraphics>();
            RemoveAdditionalItems = new List<long>();
            LineNumber = -1;
        }

        public CachedPieceOperation(OperationTypeEnum type):this()
        {
            Type = type;
            SuggestedFormat =
               DomainExtensions.GetEnumAttribute<OperationTypeEnum
                       , SuggestedFormatAttribute>(type)?.SuggestedFormat
               ?? string.Empty;

            DisplayName = type.ToString().ToUpper();
            LocalizationKey = $"{MachineManagementExtensions.LABEL_OPERATIONTYPE}_{type.ToString().ToUpper()}";
            ImageCode = type.ToString().ToUpper();
         
        }

        internal void AddAdditionalItem(CachedAdditionalItem additionalItem)
        {
            additionalItem.Status = OperationRowStatusEnum.Added;
            _innerAdditionalItems.Add(additionalItem.Index, additionalItem);
        }

        internal void RemoveAdditionalItem(CachedAdditionalItem additionalItem)
        {
            //Se è un elemento presente nel db
            //Allora lo aggiungo alla collezione degli additionalItems da rimuovere
            if (additionalItem.Status != OperationRowStatusEnum.Added && 
              !RemoveAdditionalItems.Contains(additionalItem.Id))
            { 
                RemoveAdditionalItems.Add(additionalItem.Id);
            }

            _innerAdditionalItems.Remove(additionalItem.Index);
        }

        internal void ReNumberingAdditionalItems(int startingIndex, int offset = 1)
        {
            if (AdditionalItems?.Any() ?? false)
            {
                //1. Trasformo additionalItems in una lista
                //2. Recupero gli additionalItems che non devono essere rinumerati
                var additionalItems = GetCachedAdditionalItems();
                //1. Trasformo le operazioni in una lista
                //2. Recupero le operazioni che non devono essere rinumerate
                var additionalItemsNotRenumbered = additionalItems
                            .TakeWhile(op => op.Index < startingIndex);


                var additionalItemsToRenum = additionalItems
                    .Where(op => op.Index >= startingIndex)
                                .Select(ai =>
                                {
                         
                                    ai.Index += offset;
                                    if (ai.Status == OperationRowStatusEnum.AttributeUpdated)
                                    {
                                        ai.Status = OperationRowStatusEnum.FullUpdated;
                                    }
                                    else if (ai.Status == OperationRowStatusEnum.UnChanged)
                                    {
                                        ai.Status = OperationRowStatusEnum.Updated;
                                    }
                                    return ai;
                                });

                _innerAdditionalItems = additionalItemsNotRenumbered.Concat(additionalItemsToRenum)
                                .ToDictionary(ai => ai.Index, ai => ai);
            }
        }

        internal bool HandleAdditionalItems()
        {
            return _additionaItemType != OperationTypeEnum.Undefined;
        }

        internal bool HasAdditionalItems()
        {
            var cachedAdditionalItems = GetCachedAdditionalItems();
            //Se esistono degli elementi oppure se tutti gli elementi sono stati rimossi
            return cachedAdditionalItems.Any()
                || (!cachedAdditionalItems.Any() && RemoveAdditionalItems != null && RemoveAdditionalItems.Any());
        }
    }


    public static class CachedOperationExtensions
    {
        public const string IMAGE_PROBE = "Probe";
        public const string IMAGE_ORIGINTYPE = "OriginType";
        public const string IMAGE_SPECIALCYCLE = "SpecialCycle";
        public const string IMAGE_PATTERN = "Pattern";

        /// <summary>
        /// Recursive search to find the parent's line-number based on current operation's line-number and level.
        /// </summary>
        /// <returns>Parent's line number or -1 if no parent was found.</returns>
        public static int GetParentLineNumber(this CachedPieceOperation operation)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (operation.Level == 0)
            {
                return 0;
            }
            else
            {
                var parentOp = CachedPieceFactory.CachedPiece.Operations.Values
                    .LastOrDefault(o => o.LineNumber < operation.LineNumber && o.Level == (operation.Level - 1));

                return parentOp?.LineNumber ?? -1;
            }
        }

        /// <summary>
        /// Recursive search to find the collapsed state of parent nodes.
        /// </summary>
        /// <returns>true if a collapsed parent is found; otherwise false.</returns>
        public static bool HasCollapsedParent(this CachedPieceOperation operation)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (operation.ParentLineNumber == 0)
            {
                return false;
            }
            else
            {
                if (CachedPieceFactory.CachedPiece.Operations.TryGetValue(operation.ParentLineNumber, out var parentOperation)
                    && parentOperation.IsCollapsed)
                { 
                    return true;
                }
                else
                {
                    return parentOperation.HasCollapsedParent();
                }
            }
        }

        public static Dictionary<AttributeDefinitionEnum, object> GetAdditionalAttributesForRulesHandler(this CachedPieceOperation operation
                                                                                , IMachineConfigurationService MachineConfigurationService)
        {
            var operationInfo = CoreExtensions
                                    .GetOperationTypeEnumAttribute<OperationInfoAttribute>(operation.Type);

            var additionalAttributes = new Dictionary<AttributeDefinitionEnum, object>()
                        {
                            { AttributeDefinitionEnum.OperationType, operation.Type },
                            { AttributeDefinitionEnum.OperationRelatedToolTypes, operationInfo.RelatedToolTypes  },
                            { AttributeDefinitionEnum.ProfileType, CachedPieceFactory.CachedPiece.ProfileType }
                        };

            //Aggiungo gli attributi per poter filtrare i dati secondo la Macro scelta.
            if (operation.Type.IsMacro())
            {
                var macroAttributes = operation.GetMacroAttributesConfiguration(MachineConfigurationService);
                if (macroAttributes != null)
                {
                    additionalAttributes.Add(AttributeDefinitionEnum.MacroConfigurationItems, macroAttributes);
                }
            }
            return additionalAttributes;

        }

        public static void SetClusteredAttributes(this CachedPieceOperation operation
                                            , IEnumerable<ClusteredAttributeDetailItem> attributes)
        {

            var groups = new HashSet<BaseGroupItem<ClusteredAttributeDetailItem>>();

            groups.Add(new BaseGroupItem<ClusteredAttributeDetailItem>
            {
                LocalizationKey = $"{MachineManagementExtensions.LABEL_GROUP}_MAIN",
                Details = attributes.Where(a => a.AttributeScopeId == AttributeScopeEnum.Fundamental)
                            .OrderBy(a => a.ClusterId)
                            .ThenBy(a => a.Order)
                            .ToHashSet()
            });

            //Recupero attributi "secondari"
            var slaveAttributes = attributes.Where(a => a.AttributeScopeId == AttributeScopeEnum.Optional);

            if (slaveAttributes.Any())
                groups.Add(new BaseGroupItem<ClusteredAttributeDetailItem>
                {
                    LocalizationKey = $"{MachineManagementExtensions.LABEL_GROUP}_SLAVE",
                    Details = slaveAttributes
                                .OrderBy(a => a.ClusterId)
                                .ThenBy(a => a.Order)
                                .ToHashSet()
                });

            operation.Attributes = groups;
        }

        public static void SetAttributesDefault(this CachedPieceOperation operation
                                                 , IServiceFactory serviceFactory
                                                 , IEnumerable<ProtectionLevelEnum> protectionLevels
                                                , MeasurementSystemEnum conversionSystemTo)
        {
            var MachineConfigurationService = serviceFactory.GetService<IMachineConfigurationService>();
            var macroAttributes = operation.GetMacroAttributesConfiguration(MachineConfigurationService);
            foreach (var attribute in operation.Attributes.SelectMany(group => group.Details))
            {
                SetAttributeLocalization(attribute, macroAttributes);
                SetAttributeDefaultException(attribute
                                         , serviceFactory
                                         , protectionLevels
                                         , macroAttributes
                                         , conversionSystemTo);
            }
        }

        private static void SetAttributeLocalization(ClusteredAttributeDetailItem attribute
                                                 ,IEnumerable<AttributeConfigurationItem> macroAttributes)
        {
            if (macroAttributes != null)
            {
                var macroAttribute = macroAttributes.FirstOrDefault(a => a.DisplayName == attribute.EnumId);
                if ((macroAttribute != null)
                        && (macroAttribute.LocalizationKey.IsNotNullOrEmpty()))
                {
                    attribute.LocalizationKey = macroAttribute.LocalizationKey;
                }

                if (attribute.EnumId == AttributeDefinitionEnum.Image)
                {
                    var macroAttributeName = macroAttributes.FirstOrDefault(a => a.DisplayName == AttributeDefinitionEnum.MacroName);
                    // Gitea #172 => Se l'attributo è Image legato ad MCut allora la chiave di localizzazione è il nome della macro                             
                   
                    attribute.LocalizedText= macroAttributeName != null ? macroAttributeName.Value : "";
                }
            }
        }

        //quando la struttura degli attributi è stata creata (in base alle impostazioni del DB) occorre apportare delle eccezioni ai default da DB
        //cosa che non va fatta invece sui valori già inseriti nel DB(Ad es potrei modificare un DataFormat di un attributeValue, cambiandogli di significato
        //e rendendo il dato non più valido)
        private static void SetAttributeDefaultException(ClusteredAttributeDetailItem attribute
                                                , IServiceFactory serviceFactory
                                                , IEnumerable<ProtectionLevelEnum> protectionLevels
                                                , IEnumerable<AttributeConfigurationItem> macroAttributesConfiguration
                                                , MeasurementSystemEnum conversionSystemTo)
        {
            switch (attribute.AttributeKind)
            {
                case AttributeKindEnum.Number:
                    {
                        //Assegnazione della localizzazione unità di misura e della precisione (sempre su valori di default = 0 per i numeri )
                        var ConvertedItem = ConvertToHelper.Convert(conversionSystemFrom: conversionSystemTo
                                                            , conversionSystemTo: conversionSystemTo
                                                            , attribute.ItemDataFormat
                                                            , 0);
                        if (ConvertedItem != null)
                        {
                            attribute.UMLocalizationKey = ConvertedItem.UmLocalizationKey;
                            attribute.DecimalPrecision = ConvertedItem.DecimalPrecision;
                        }
                        //default = 0
                        attribute.Value.CurrentValue = 0;
                    }
                    break;
                case AttributeKindEnum.Enum:
                    {
                        //Assegno i valori di default per gli enumerativi
                        IAttributeDefinitionEnumManagement SourcesService = serviceFactory.Resolve<IAttributeDefinitionEnumManagement,AttributeDefinitionEnum>(attribute.EnumId);
                        var attributeDefault = SourcesService.GetDefaultValue();
                        if ((attributeDefault != null) && (int.TryParse(attributeDefault.Value.ToString(), out var enumValue)))
                        {
                            attribute.Value.CurrentValueId = enumValue;
                            attribute.Value.CurrentValue = attributeDefault.Code;
                        }
                            
                    }
                    break;
                case AttributeKindEnum.String:
                    //default è stringa vuota
                    attribute.Value.CurrentValue = "";
                    if (attribute.EnumId == AttributeDefinitionEnum.PatternImage)
                    {
                        attribute.Value.CurrentValue = CachedOperationExtensions.IMAGE_PATTERN.ToUpper();
                        attribute.LocalizationKey = string.Empty;
                    }
                    break;
                case AttributeKindEnum.Bool:
                    //Default false
                    attribute.Value.CurrentValue = 0;
                    break;
                default:
                    break;

            }
            if (macroAttributesConfiguration != null)
            {
                //Modifica del valore del DataFormat in caso di operazione Macro
                var macroAttribute = macroAttributesConfiguration.FirstOrDefault(a => a.DisplayName == attribute.EnumId);
                if ((macroAttribute != null) && (macroAttribute.DataFormat.HasValue))
                {
                    attribute.ItemDataFormat = macroAttribute.DataFormat.Value;
                    if (attribute.AttributeKind == AttributeKindEnum.Number)
                    {
                        var ConvertedItem = ConvertToHelper.Convert(conversionSystemFrom: conversionSystemTo
                                                                , conversionSystemTo: conversionSystemTo
                                                                , attribute.ItemDataFormat
                                                                , 0);
                        if (ConvertedItem != null)
                        {
                            attribute.UMLocalizationKey = ConvertedItem.UmLocalizationKey;
                            attribute.DecimalPrecision = ConvertedItem.DecimalPrecision;
                        }
                    }
                }
                //Modifico il valore dell'immagine con quello caricato da configurazione delle macro
                if (attribute.EnumId == AttributeDefinitionEnum.Image)
                {
                    attribute.Value.CurrentValue = macroAttribute != null ? macroAttribute.Value : "";
                }

                if (attribute.EnumId == AttributeDefinitionEnum.MacroName)
                {
                    attribute.Value.CurrentValue = macroAttribute != null ? macroAttribute.Value : "";
                }
            }
            attribute.IsReadonly = attribute.ProtectionLevel == ProtectionLevelEnum.ReadOnly;
        }

        private static string GetMacroName(this CachedPieceOperation operation)
        {
            var attributeMacroName = operation.Attributes.SelectMany(group => group.Details)
                   .SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MacroName);
            if (attributeMacroName != null)
            { 
                if (attributeMacroName.Value.CurrentValue!=null) //se arriva da altra struttura
                    return attributeMacroName.Value.CurrentValue.ToString();
                else if (attributeMacroName.DbValue!=null) //se arriva da DB
                    return attributeMacroName.DbValue.ToString();

            }
            return "";
        }


        private static IEnumerable<AttributeConfigurationItem> GetMacroAttributesConfiguration(this CachedPieceOperation operation
                                                , IMachineConfigurationService MachineConfigurationService)

        {
            IEnumerable<AttributeConfigurationItem> macroAttributes = null;
            if (operation.Type.IsMacro())
            {
                var attributeMacroName = GetMacroName(operation);
                if (attributeMacroName != "")
                {
                    var attributesConfiguration = MachineConfigurationService.GetMacroAttributes(new MacroConfigurationFilter()
                    {
                        //nel futuro ci sarà la necessità di avere più valori..
                        MacroName = attributeMacroName,
                        MacroType = DomainExtensions.GetMacroTypeFromOperationType(operation.Type),
                        ProfileType = CachedPieceFactory.CachedPiece.ProfileType
                    });
                    if (attributesConfiguration.Success)
                    {
                        macroAttributes = ((IEnumerable<AttributeConfigurationItem>)attributesConfiguration.Value).Cast<AttributeConfigurationItem>().ToList();
                    }
                }
            }
            return macroAttributes;
        }

        public static void SetAdditionalItemsForGraphics(this CachedPieceOperation operation)
        {
            operation.AdditionalItemInfosForGraphics = Array.Empty<AdditionalItemForGraphics>();
            if (operation.AdditionalItems.Any())
            {
                operation.AdditionalItemInfosForGraphics = operation.AdditionalItems.Select(ai =>
                                       new AdditionalItemForGraphics
                                       {
                                           Index = ai.Index,
                                           OperationType = ai.OperationType,
                                           Attributes = ai.Attributes
                                                       .Where(a => a.UsedForGraphicsPreview)
                                                       .ToDictionary(attr => Enum.Parse<DatabaseDisplayNameEnum>(attr.DisplayName),
                                                               attr => attr.AttributeKind == AttributeKindEnum.Enum
                                                                   ? (object)attr.Value.CurrentValueId
                                                                   : attr.Value.CurrentValue)
                                       });
            }

            
        }

        public static void SetAdditionalItemsForGraphics(this CachedPieceOperation operation
                    , IEnumerable<SlaveOperationTypeCodeGenerator> slaveCodeGenerators)
        {
            operation.AdditionalItemInfosForGraphics = Array.Empty<AdditionalItemForGraphics>();
            if (slaveCodeGenerators.Any())
            {
                operation.AdditionalItemInfosForGraphics = slaveCodeGenerators.GroupBy(cg => (cg.Index, cg.SubParentTypeId))
                .Select(group => new AdditionalItemForGraphics
                {
                    Index = group.Key.Index,
                    OperationType = (OperationTypeEnum)group.Key.SubParentTypeId,
                    Attributes = group.AsEnumerable()
                                    .OrderBy(a => a.Index)
                                    .ToDictionary(a => Enum.Parse<DatabaseDisplayNameEnum>(a.DisplayName),
                                            a =>
                                            {

                                                if (!a.TypeName.IsNullOrEmpty())
                                                {
                                                    var enumType = Type.GetType(a.TypeName);
                                                    var typeConverter = TypeDescriptor.GetConverter(enumType);
                                                    if (typeConverter is EnumCustomNameTypeConverter)
                                                        return typeConverter
                                                                    .ConvertFrom(null, CultureInfo.InvariantCulture
                                                                    , a.Value.GetValueOrDefault(0))?.ToString()
                                                                    ?? string.Empty;
                                                    else
                                                        return a.Value;
                                                }
                                                else
                                                {
                                                    return a.AttributeKindId == AttributeKindEnum.Number
                                                            ? a.Value.HasValue ? (object)a.Value : 0
                                                            : a.TextValue;
                                                }
                                            })
                });
            }
            


        }

        public static void SetIcons(this CachedPieceOperation cachedPieceOperation)
        {
            var icons = new List<string>();

            //TO DO: Special Cycle

            if (cachedPieceOperation.CodeGenerators.TryGetValue(DatabaseDisplayNameEnum.ProbeM, out var probe) 
                && Enum.TryParse<ProbeCodeEnum>(probe.ToString(), out var probeValue)
                && probeValue != ProbeCodeEnum.None)
                icons.Add($"PATH_{DatabaseDisplayNameEnum.ProbeM.ToString().ToUpper()}");

            //Se OriginTypeX o OriginTypeY non sono Standard allora accendo l'icona "OriginType"
            if (cachedPieceOperation.CodeGenerators.TryGetValue(DatabaseDisplayNameEnum.TypeOX, out var originTypeX) &&
                Enum.TryParse<OperationOriginTypeEnum>(originTypeX.ToString(), out var originTypeXValue)
                && originTypeXValue != OperationOriginTypeEnum.Standard
                ||
                (cachedPieceOperation.CodeGenerators.TryGetValue(DatabaseDisplayNameEnum.TypeOY, out var originTypeY) &&
                Enum.TryParse<OperationOriginTypeEnum>(originTypeY.ToString(), out var originTypeYValue)
                && originTypeYValue != OperationOriginTypeEnum.Standard))
            {
                icons.Add($"PATH_{IMAGE_ORIGINTYPE.ToUpper()}");
            }

            if (cachedPieceOperation.CodeGenerators.TryGetValue(DatabaseDisplayNameEnum.DisableTorchControlHeight, out var 
                        disabledTorchHeight) && 
                        Convert.ToBoolean(disabledTorchHeight))
            {
                icons.Add($"PATH_POINT_{DatabaseDisplayNameEnum.DisableTorchControlHeight.ToString().ToUpper()}_OPERATION");
            }

            cachedPieceOperation.Icons = icons;
        }

        public static void SetIcons(this CachedAdditionalItem cachedAdditionalItem)
        {
            List<string> iconAttributeDisplayNames =
                new List<string> { DatabaseDisplayNameEnum.CurrentReduction.ToString()
                            , DatabaseDisplayNameEnum.SpeedReduction.ToString()
                            , DatabaseDisplayNameEnum.CutOffEnable.ToString()
                            , DatabaseDisplayNameEnum.DisableTorchControlHeight.ToString() };

            var icons = new List<string>();

            cachedAdditionalItem.Attributes
                .Where(a => iconAttributeDisplayNames.Contains(a.DisplayName))
                .ForEach(a =>
                {
                    var boolValue = System.Convert.ToBoolean(a.Value.CurrentValue);
                    if (boolValue)
                    {
                        icons.Add($"PATH_POINT_{a.DisplayName.ToUpper()}");
                    }
                });

            cachedAdditionalItem.Icons = icons;
        }
    }
}
