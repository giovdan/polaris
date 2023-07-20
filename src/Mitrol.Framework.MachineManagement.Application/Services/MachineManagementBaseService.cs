


namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Conversions;
    using System.Linq;
    using System.Globalization;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.Domain.Core.Extensions;
    using System.Text.RegularExpressions;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Production;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System.Linq.Expressions;
    using Mitrol.Framework.MachineManagement.Application.Models.General;
    using Mitrol.Framework.Domain.Core.Enums;

    public class MachineManagementBaseService : BaseService
    {
        protected IEntityRepository EntityRepository => ServiceFactory.GetService<IEntityRepository>();
        protected IAttributeValueRepository AttributeValueRepository => ServiceFactory.GetService<IAttributeValueRepository>();
        protected IAttributeDefinitionLinkRepository AttributeDefinitionLinkRepository =>
                        ServiceFactory.GetService<IAttributeDefinitionLinkRepository>();
        protected IUnitOfWorkFactory<IMachineManagentDatabaseContext> UnitOfWorkFactory => 
                        ServiceFactory.GetService<IUnitOfWorkFactory<IMachineManagentDatabaseContext>>();
        protected IDetailIdentifierRepository DetailIdentifierRepository =>
                            ServiceFactory.GetService<IDetailIdentifierRepository>();

        protected IQuantityBackLogRepository QuantityBackLogRepository =>
                            ServiceFactory.GetService<IQuantityBackLogRepository>();

        protected IEntityLinkRepository EntityLinkRepository =>
                            ServiceFactory.GetService<IEntityLinkRepository>();
        #region < ApplyCustomMapping >
        /// <summary>
        /// Custom Mapping from AttributeValue to AttributeDetailItem
        /// </summary>
        /// <param name="attributeValue"></param>
        /// <param name="measurementSystemTo"></param>
        /// <returns></returns>
        private AttributeDetailItem ApplyCustomMapping(AttributeValue attributeValue, MeasurementSystemEnum measurementSystemTo)
        {
            var value = attributeValue.GetAttributeValue(measurementSystemTo);

            var attributeDetailItem = Mapper.Map<AttributeDetailItem>(attributeValue);

            attributeDetailItem.DbValue = attributeValue.GetAttributeValue();
            attributeDetailItem.Value = new AttributeValueItem
            {
                CurrentValue = attributeValue.AttributeDefinitionLink.AttributeDefinition.AttributeKind != AttributeKindEnum.Enum
                                ? value : null,
                CurrentValueId = attributeValue.AttributeDefinitionLink.AttributeDefinition.AttributeKind == AttributeKindEnum.Enum
                                ? Convert.ToInt32(value) : 0

            };

            return attributeDetailItem;
        }
        #endregion

        public MachineManagementBaseService(IServiceFactory serviceFactory) : base(serviceFactory)
        { }

        #region < Protected Methods >
        /// <summary>
        /// Get entity identifiers by hashCode
        /// </summary>
        /// <param name="hashCode"></param>
        /// <param name="conversionSystemTo"></param>
        /// <returns></returns>
        protected List<IdentifierDetailItem> GetIdentifiers(string hashCode
                    , MeasurementSystemEnum conversionSystemTo)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            DetailIdentifierRepository.Attach(uow);
            return Mapper.Map<IEnumerable<IdentifierDetailItem>>(
                            DetailIdentifierRepository.FindBy(di => di.HashCode == hashCode, di => di.Priority))
                            .Select(identifier => identifier.ConvertIdentifier(conversionSystemTo))
                            .ToList();


        }

        /// <summary>
        /// Add Quantity backlogs for a specific entity
        /// </summary>
        /// <param name="quantiyBackLog"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        protected Result AddQuantityBackLog(QuantityBackLogItem quantiyBackLog, IUnitOfWork<IMachineManagentDatabaseContext> uow)
        {
            try
            {
                QuantityBackLogRepository.Attach(uow);
                QuantityBackLogRepository.Add(Mapper.Map<QuantityBackLog>(quantiyBackLog));
                uow.Commit();
                return Result.Ok();
            }
            catch (Exception ex)
            {

                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }

        protected IEnumerable<AttributeDetailItem> InnerGetAttributeDefinitions(
            EntityTypeEnum entityType
            , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
            , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem
            , IUnitOfWork<IMachineManagentDatabaseContext> uow = null)
        {
            var unitOfWork = uow ?? UnitOfWorkFactory.GetOrCreate(UserSession);
            AttributeDefinitionLinkRepository.Attach(unitOfWork);
            return Mapper.Map<IEnumerable<AttributeDetailItem>>(AttributeDefinitionLinkRepository
                        .FindBy(adl => adl.EntityTypeId == entityType))
                .Select(a => ApplyCustomMapping(a, conversionSystemFrom
                                                    , conversionSystemTo));

        }
        /// <summary>
        /// Get attribute definitions for specified entityType
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public IEnumerable<AttributeDetailItem> GetAttributeDefinitions(EntityTypeEnum entityType
                        , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                        , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem)
        {
            using var unitOfWork = UnitOfWorkFactory.GetOrCreate(UserSession);
            return InnerGetAttributeDefinitions(entityType, conversionSystemFrom
                                                        , conversionSystemTo, unitOfWork);
        }

        protected virtual void SetAttributeDetailValue(AttributeDetailItem attribute
                                        , IEnumerable<ProtectionLevelEnum> protectionLevels
                                        , MeasurementSystemEnum conversionSystemFrom
                                        , MeasurementSystemEnum conversionSystemTo
                                        , Dictionary<AttributeDefinitionEnum, object> additionalInfos = null
                                        , bool isReadOnly = true)
        {
            (var attributeValue, var convertedItem) = CreateAttributeValueItem(attribute
                                                  , conversionSystemFrom
                                                  , conversionSystemTo
                                                  , additionalInfos);
            attribute.Value = attributeValue;
            if (convertedItem != null)
            {
                attribute.UMLocalizationKey = convertedItem.UmLocalizationKey;
                attribute.DecimalPrecision = convertedItem.DecimalPrecision;
            }

            attribute.IsReadonly = isReadOnly &&
                    (attribute.ProtectionLevel == ProtectionLevelEnum.ReadOnly || !protectionLevels.Contains(attribute.ProtectionLevel));
        }

        /// <summary>
        /// Crea il record AttributeValue e gli assegna il valore passato in input
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="subParentTypeId"></param>
        /// <param name="parentTypeId"></param>
        /// <param name="attributeDefinition"></param>
        /// <param name="value">Valore da assegnare all'attributo</param>
        /// <param name="conversionSystemFrom"></param>
        /// <param name="conversionSystemTo"></param>
        /// <returns></returns>
        protected AttributeValue CreateAttributeValueFromSingle(
                                         long entityId
                                         , AttributeDetailItem attributeDetailItem
                                         , object value
                                         , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                                         , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem)
        {
            var attributeToAdd = CreateAttributeValueFromDefinition(attributeDetailItem, entityId);

            if (value != null)
            {
                attributeToAdd = UpdateAttributeValueFromObject(attributeDetailItem
                                                    , value
                                                    , conversionSystemFrom
                                                    , conversionSystemTo
                                                    , attributeToAdd);
            }


            return attributeToAdd;
        }

        /// <summary>
        /// Crea il record AttributeValue e gli assegna il valore di default
        /// </summary>
        /// <param name="attributeDefinitionLink"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        protected AttributeValue CreateAttributeValueFromDefinition(
                AttributeDetailItem attributeDetailItem
                , long entityId)
        {
            var attributeValue = Mapper.Map<AttributeValue>(attributeDetailItem);
            attributeValue.EntityId = entityId;

            switch (attributeDetailItem.AttributeKind)
            {
                case AttributeKindEnum.String:
                    attributeValue.TextValue = "";
                    break;
                case AttributeKindEnum.Enum:
                    //Recupero il valore di Default del tipo di enumerativo rappresentato dall'attributo "attributeDefinition" 
                    IAttributeDefinitionEnumManagement SourcesService = ServiceFactory
                                    .Resolve<IAttributeDefinitionEnumManagement, AttributeDefinitionEnum>
                                        (attributeDetailItem.EnumId);
                    var attributeDefault = SourcesService.GetDefaultValue();
                    if ((attributeDefault != null) && (int.TryParse(attributeDefault.Value.ToString(), out var enumValue)))
                    {
                        attributeValue.Value = enumValue;
                        attributeValue.TextValue = string.Empty;
                    }
                    break;
                case AttributeKindEnum.Number:
                    attributeValue.Value = 0;
                    attributeValue.TextValue = string.Empty;
                    break;
                case AttributeKindEnum.Bool:
                    attributeValue.Value = 0;
                    attributeValue.TextValue = string.Empty;
                    break;
            }
            return attributeValue;
        }

        protected AttributeValue UpdateAttributeValueFromObject(AttributeDetailItem attributeDetailItem
                                                    , object Value
                                                    , MeasurementSystemEnum ConversionSystemFrom
                                                    , MeasurementSystemEnum ConversionSystemTo
                                                    , AttributeValue attribute)
        {
            switch (attributeDetailItem.AttributeKind)
            {
                case AttributeKindEnum.String:
                    attribute.TextValue = Value?.ToString() ?? string.Empty;
                    break;
                case AttributeKindEnum.Bool:
                    try
                    {
                        if (Value != null)
                        {
                            if (bool.TryParse(Value.ToString(), out bool boolres))
                                attribute.Value = boolres == true ? 1 : 0;//per stringhe "true o false"
                            else
                            {
                                if (decimal.TryParse(Value.ToString(), out var dresult))//per stringhe "0 o 1"
                                    attribute.Value = dresult;
                            }
                        }
                        else
                            attribute.Value = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case AttributeKindEnum.Number:
                    if (Value != null)
                    {
                        var value = Value;
                        if (attributeDetailItem.ControlType == ClientControlTypeEnum.Override)
                        {
                            try
                            {
                                var OvValue = OverrideValueItem.GetOverrideObject(Value.ToString());
                                value = OvValue.Value ?? 0;
                            }
                            catch (Exception exc)
                            {
                                throw;
                            }
                        }
                        try
                        {
                            var decimalResult = Convert.ToDecimal(value);
                            attribute.Value = ConvertToHelper.Convert(conversionSystemFrom: ConversionSystemFrom
                                                        , conversionSystemTo: ConversionSystemTo
                                                        , attributeDetailItem.ItemDataFormat
                                                        , decimalResult).Value;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    else
                        attribute.Value = 0;
                    break;
                case AttributeKindEnum.Enum:
                    if (Value != null)
                    {
                        try
                        {
                            var v = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>(Value.ToString());
                            attribute.Value = v.Id;
                            attribute.TextValue = v.Value;
                        }
                        catch (Exception exc)
                        {

                            //Programma i dati arrivano dal FE sotto forma di Dizionario e gli enumerativi possono arrivare anche come stringhe.. 
                            if (decimal.TryParse(Value.ToString(), out var decimalValue))
                                attribute.Value = decimalValue;
                            else
                            {
                                IAttributeDefinitionEnumManagement enumManagement = ServiceFactory
                                     .Resolve<IAttributeDefinitionEnumManagement, AttributeDefinitionEnum>(
                                        attributeDetailItem.EnumId);
                                var enumfound = enumManagement.GetEnumFromStringValue(Value.ToString());
                                if (enumfound != null)
                                    attribute.Value = Convert.ToDecimal(enumfound);
                            }
                        }
                    }
                    else
                    {
                        IAttributeDefinitionEnumManagement enumManagement = ServiceFactory
                                                             .Resolve<IAttributeDefinitionEnumManagement
                                                             , AttributeDefinitionEnum>(attributeDetailItem.EnumId);
                        var attributeDefault = enumManagement.GetDefaultValue();
                        if ((attributeDefault != null) && (int.TryParse(attributeDefault.Value.ToString(), out var enumValue)))
                        {
                            attribute.Value = enumValue;
                            attribute.TextValue = string.Empty;
                        }
                    }
                    break;
            }
            return attribute;
        }


        /// <summary>
        /// Crea un record AttributeValue 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="subParentTypeId"></param>
        /// <param name="parentType">Identificativo Parent</param>
        /// <param name="attributeDefinition"></param>
        /// <param name="values"></param>
        /// <param name="conversionSystem"></param>
        /// <returns></returns>
        protected AttributeValue CreateAttributeValue(
                                            long entityId
                                            , AttributeDetailItem attributeDetailItem
                                            , Dictionary<DatabaseDisplayNameEnum, object> values
                                            , MeasurementSystemEnum conversionSystemFrom
                                            , MeasurementSystemEnum conversionSystemTo)
        {
            if (values != null)
            {
                if (Enum.TryParse<DatabaseDisplayNameEnum>(attributeDetailItem.DisplayName
                                , true, out var displayName)
                    && values.TryGetValue(displayName, out var attributeValue))
                {

                    return CreateAttributeValueFromSingle(entityId
                                                        , attributeDetailItem
                                                        , attributeValue
                                                        , conversionSystemFrom
                                                        , conversionSystemTo);
                }
            }

            // attributoValue con valori di default
            return CreateAttributeValueFromDefinition(attributeDetailItem, entityId);
        }

        protected virtual (AttributeValueItem AttributeValue, ConvertedItem ConvertedItem)
            CreateAttributeValueItem(AttributeDetailItem attribute
                                  , MeasurementSystemEnum conversionSystemFrom
                                  , MeasurementSystemEnum conversionSystemTo
                                  , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        {
            AttributeValueItem attributeValue = new AttributeValueItem();

            ConvertedItem convertedItem = null;
            decimal decimalValue = 0;

            if (attribute.DbValue != null
                && attribute.AttributeKind == AttributeKindEnum.Number
                && decimal.TryParse(attribute.DbValue.ToString(), out decimalValue))
                convertedItem = ConvertToHelper.Convert(conversionSystemFrom: conversionSystemFrom
                                                        , conversionSystemTo: conversionSystemTo
                                                        , attribute.ItemDataFormat
                                                        , decimalValue
                                                        , applyRound: true);
            var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            AttributeValueRepository.Attach(uow);
            switch (attribute.ControlType)
            {
                case ClientControlTypeEnum.Check:
                    attributeValue.CurrentValue = attribute.DbValue;
                    break;
                case ClientControlTypeEnum.ListBox:
                case ClientControlTypeEnum.Combo:
                    {
                        //Recupero la lista di tutti i possibili valori
                        IAttributeDefinitionEnumManagement sourcesService
                            = ServiceFactory.Resolve<IAttributeDefinitionEnumManagement
                                    , AttributeDefinitionEnum>(attribute.EnumId);

                        var attributeSourceList = sourcesService.FindAttributeSourceValues();

                        //Se il controllo è una ComboBox applico i filtri sui valori in base al parentType
                        if (attribute.ControlType == ClientControlTypeEnum.Combo)
                        {
                            attributeSourceList = ServiceFactory.GetEntityRulesHandlers(attribute.EntityType.ToParentType())
                                .GetFilteredAttributes(attributeSourceList
                                                , additionalInfos);
                        }

                        var filteredValues = Mapper.Map<IEnumerable<AttributeSourceValueItem>>(attributeSourceList).ToList();

                        //Recupero l'insieme dei valore dell'enumerato
                        if (filteredValues.Any())
                        {
                            var firstValue = filteredValues.FirstOrDefault();
                            attributeValue.ValueType = sourcesService.GetValueType();
                            //Se è un solo valore ed è un uri allora il source è l'uri
                            if (attributeValue.ValueType == ValueTypeEnum.DynamicEnum)
                            {
                                //ListBox
                                attributeValue.Source = firstValue.Value.ToString();
                                attributeValue.CurrentValue = attribute.TextValue;
                                attributeValue.CurrentValueId = Convert.ToInt32(attribute.DbValue);
                            }
                            else
                            {
                                var defaultEnumValue = -1;
                                //Recupero il valore di Default del tipo di enumerativo rappresentato dall'attributo "attribute"
                                if (attribute.DbValue == null)
                                {
                                    var defaultSource = sourcesService.GetDefaultValue();
                                    if (defaultSource != null && int.TryParse(defaultSource.Value.ToString(), out defaultEnumValue))
                                    { }
                                    attributeValue.CurrentValueId = defaultEnumValue;
                                }
                                else
                                {
                                    if (decimal.TryParse(attribute.DbValue.ToString(), out var dbValue))
                                    {
                                        attributeValue.CurrentValueId = decimal.ToInt64(dbValue);
                                    }
                                    else if (!string.IsNullOrEmpty(attribute.TypeName))
                                    {
                                        //di qui non passa mai perchè nessuno degli oggetti mappati ha il TypeName valorizzato
                                        var enumType = Type.GetType(attribute.TypeName);
                                        var typeConverter = TypeDescriptor.GetConverter(enumType);
                                        if (typeConverter is EnumCustomNameTypeConverter)
                                        {
                                            attribute.DbValue = typeConverter
                                                        .ConvertFrom(null, CultureInfo.InvariantCulture
                                                        , attribute.DbValue.ToString());
                                        }
                                    }

                                }
                                //Combo
                                attributeValue.Source = filteredValues.OrderNatural(a => a.Value.ToString());
                            }
                        }
                    }
                    break;
                case ClientControlTypeEnum.Edit:
                case ClientControlTypeEnum.Label:
                case ClientControlTypeEnum.Image:
                    {
                        // Caso particolare attributo AttributeDefinitionEnum.RepetitionsNumber

                        if (attribute.EnumId == AttributeDefinitionEnum.RepetitionsNumber
                            && (attribute.DbValue == null ||
                                (attribute.DbValue != null &&
                                int.TryParse(attribute.DbValue.ToString(), out var dbValue)
                                && dbValue == 0)))
                        {
                            attribute.DbValue = 1;
                        }

                        if (attribute.AttributeKind == AttributeKindEnum.String)
                        {
                            //Se il valore del db non è inizializzato o è inizializzato a 0
                            //allora associa il valore string.Empty (default)
                            if (attribute.DbValue == null ||
                                (attribute.DbValue != null &&
                                int.TryParse(attribute.DbValue.ToString(), out dbValue)
                                && dbValue == 0))
                            {
                                attribute.DbValue = string.Empty;
                            }
                        }

                        attributeValue.CurrentValue = attribute.DbValue;

                        attributeValue.ValueType = ValueTypeEnum.Flat;
                    }
                    break;
                case ClientControlTypeEnum.Override:
                    {
                        attributeValue.CurrentValue = attribute.DbValue;
                        attributeValue.ValueType = ValueTypeEnum.Override;

                        var attributeOverride = AttributeValueRepository.GetOverrideValue(attribute.Id);
                        //Recupera l'override se esiste
                        if (attributeOverride != null)
                        {
                            var convertedItemOverride = ConvertToHelper.Convert(conversionSystemFrom: conversionSystemFrom
                                                , conversionSystemTo: conversionSystemTo
                                                , attribute.ItemDataFormat
                                                , attributeOverride.Value);
                            //Assegna il valore convertito
                            attributeValue.CurrentOverrideValue.Value = convertedItem.Value;

                            attributeValue.CurrentOverrideValue = Mapper.Map<AttributeOverrideValueItem>(attributeOverride);
                        }
                        else
                        {
                            //Altrimenti setta il valore di default
                            attributeValue.CurrentOverrideValue = new AttributeOverrideValueItem()
                            {
                                Id = 0,
                                Value = 0,
                                OverrideType = OverrideTypeEnum.DeltaValue
                            };
                        }
                    }
                    break;
            }

            if (convertedItem != null && attribute.AttributeKind == AttributeKindEnum.Number)
            {
                attributeValue.CurrentValue = convertedItem.Value % 1 == 0
                                    ? Math.Truncate(convertedItem.Value)
                                    : convertedItem.Value;
            }

            return (attributeValue, convertedItem);
        }

        protected string CalculateDisplayName(EntityTypeEnum entityType
                                            , Dictionary<string, string> identifiers
                                            , MeasurementSystemEnum conversionSystemFrom
                                            , long? parentId = null)
        {
            string displayName = string.Empty;

            var serializationNameAttribute = entityType.GetEnumAttribute<EnumSerializationNameAttribute>();

            if (parentId.HasValue)
            {
                displayName = $"{parentId}";
            }

            if (serializationNameAttribute != null)
            {
                if (!string.IsNullOrEmpty(displayName))
                {
                    displayName += "_";
                }

                displayName += serializationNameAttribute.Description.ToUpper();
            }

            foreach (var identifier in identifiers)
            {
                if (identifier.Value.IsNotNullOrEmpty() && identifier.Value.ToUpper() != "BEVEL")
                {
                    // Se il valore è un numero decimale allora formatto la stringa a 2
                    if (decimal.TryParse(identifier.Value, out var decimalValue))
                    {
                        displayName = $"{displayName}-{decimalValue.ToString("F2")}";
                    }
                    else
                    {
                        displayName = $"{displayName}-{identifier.Value}";
                    }
                }
            }

            return displayName;
        }

        /// <summary>
        /// Calculate entity HashCode based on identifiers
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="identifiers"></param>
        /// <param name="conversionSystemFrom"></param>
        /// <param name="parentId"> Specified only for tool subranges</param>
        /// <returns></returns>
        protected string CalculateHashCode(EntityTypeEnum entityType
                                            , Dictionary<string, string> identifiers
                                            , MeasurementSystemEnum conversionSystemFrom
                                            , long parentId = 0
                                            , IAttributeDefinitionLinkRepository attributeLinkDefinitionRepository = null
                                            , IUnitOfWork<IMachineManagentDatabaseContext> unitOfWork = null)
        {
            // Dizionario di identificatori normalizzati 
            // con l'aggiunta dell'informazione sulla priorità di visualizzazione
            var normalizedIdentifiers = new Dictionary<string, (string Value, int Priority)>();

            var uow = unitOfWork ?? UnitOfWorkFactory.GetOrCreate(UserSession);

            attributeLinkDefinitionRepository = attributeLinkDefinitionRepository
                                                ?? AttributeDefinitionLinkRepository;
            attributeLinkDefinitionRepository.Attach(uow);

            var attributeDefinitionLinks =
                            attributeLinkDefinitionRepository.FindBy(al => al.EntityTypeId == entityType
                                && al.AttributeDefinition.AttributeType == AttributeTypeEnum.Identifier)
                            .ToDictionary(a => a.AttributeDefinition.DisplayName);

            // Normalizzazione degli identifiers
            foreach (var identifier in identifiers)
            {
                string value = identifier.Value;

                if (attributeDefinitionLinks.TryGetValue(identifier.Key, out var attributeDefinitionLink))
                {
                    //Se l'attributo è NozzleShape legato al Tool allora: 
                    if (attributeDefinitionLink.AttributeDefinition.EnumId == AttributeDefinitionEnum.NozzleShape
                        && entityType.ToParentType() == ParentTypeEnum.Tool)
                    {
                        if (Enum.TryParse<NozzleShapeEnum>(value, out var enumValue))
                        {
                            value = enumValue.ToString();
                        }
                    }
                    else
                    {
                        if (decimal.TryParse(identifier.Value, out var decimalValue))
                        {
                            // Fix Conversion System
                            // Se il conversionSystem della sessione corrente non è metrico decimale
                            // allora occorre convertire gli identificatori numerici
                            decimalValue = ConvertToHelper.Convert(
                                conversionSystemFrom: conversionSystemFrom,
                                conversionSystemTo: MeasurementSystemEnum.MetricSystem,
                                dataFormat: attributeDefinitionLink.AttributeDefinition.DataFormat,
                                value: decimalValue).Value;

                            value = decimalValue.IsInteger() ? decimalValue.ToString("F0", CultureInfo.InvariantCulture)
                                    : decimalValue.ToString("F2", CultureInfo.InvariantCulture);

                        }
                    }

                    normalizedIdentifiers.Add(identifier.Key, (value, attributeDefinitionLink.Priority));
                }
            }

            // Recupera l'insieme dei valori con cui calcolare l'hash Code
            var normalizedValues = normalizedIdentifiers
                .OrderBy(x => x.Value.Priority)
                .Select(x => x.Value.Value)
                .ToList();

            if (parentId > 0)
            {
                normalizedValues.Add(parentId.ToString());
            }


            return string.Join("", normalizedValues).SHA256();
        }

        protected AttributeOverrideValue UpdateAttributeOverrideValueFromAttributeDetailItem(AttributeDetailItem source, MeasurementSystemEnum conversionSystemFrom, AttributeOverrideValue attributeOverrideValue)
        {
            attributeOverrideValue.Value = source.Value.CurrentOverrideValue.Value;
            attributeOverrideValue.OverrideType = source.Value.CurrentOverrideValue.OverrideType;
            return attributeOverrideValue;
        }

        protected AttributeValue UpdateAttributeValueFromAttributeDetailItem(AttributeDetailItem source,
                                                                          MeasurementSystemEnum conversionSystemFrom,
                                                                          AttributeValue attributeValue)
        {
            return UpdateAttributeValueFromValueItem(source.Value, source.AttributeKind, source.ItemDataFormat, conversionSystemFrom, attributeValue);
        }


        protected AttributeValue UpdateAttributeValueFromValueItem(AttributeValueItem source,
                                                                AttributeKindEnum attributeKind,
                                                                AttributeDataFormatEnum attributeDataFormat,
                                                                MeasurementSystemEnum conversionSystemFrom,
                                                                AttributeValue attributeValue)
        {
            switch (attributeKind)
            {
                case AttributeKindEnum.String:
                    attributeValue.TextValue = source.CurrentValue?.ToString() ?? string.Empty;
                    break;
                case AttributeKindEnum.Bool:
                    try
                    {
                        if (bool.TryParse(source.CurrentValue.ToString(), out bool boolres))
                        {
                            attributeValue.Value = boolres == true ? 1 : 0;//per stringhe "true o false"
                        }
                        else
                        {
                            if (decimal.TryParse(source.CurrentValue.ToString(), out var dresult))//per stringhe "0 o 1"
                                attributeValue.Value = dresult;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                case AttributeKindEnum.Number:
                    {
                        try
                        {
                            var decimalResult = Convert.ToDecimal(source.CurrentValue);
                            var convertedItem = ConvertToHelper.Convert(conversionSystemFrom: conversionSystemFrom
                                                     , conversionSystemTo: MeasurementSystemEnum.MetricSystem
                                                     , attributeDataFormat
                                                     , decimalResult);
                            attributeValue.Value = Math.Round(convertedItem.Value, convertedItem.DecimalPrecision);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    break;
                case AttributeKindEnum.Enum:
                    attributeValue.Value = source.CurrentValueId;
                    if (source.ValueType == ValueTypeEnum.DynamicEnum)
                    {
                        attributeValue.TextValue = source.CurrentValue.ToString();
                    }
                    break;
            }

            return attributeValue;
        }

        #region < Apply Custom Mapping >
        /// <summary>
        /// Create AttributeDetail mapping
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        protected AttributeDetailItem ApplyCustomMapping(AttributeDetailItem attribute
                , MeasurementSystemEnum conversionSystemFrom
                , MeasurementSystemEnum conversionSystemTo
                , AttributeDataFormatEnum? dataFormat = null
                , Dictionary<DatabaseDisplayNameEnum, object> innerValues = null
                , Dictionary<AttributeDefinitionEnum, object> additionalInfos = null)
        {
            var protectionLevels = UserSession.GetProtectionLevels();

            SetAttributeDetailValue(attribute, protectionLevels
                        , conversionSystemFrom, conversionSystemTo
                        , additionalInfos: additionalInfos);

            if (innerValues != null &&
                innerValues.TryGetValue(Enum.Parse<DatabaseDisplayNameEnum>(attribute.DisplayName),
                    out var attributeValue))
            {
                switch (attribute.AttributeKind)
                {
                    case AttributeKindEnum.Enum:
                        {
                            if (attribute.ControlType == ClientControlTypeEnum.ListBox)
                            {
                                var baseItemValue = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>
                                            (attributeValue.ToString());
                                attribute.Value.CurrentValueId = baseItemValue.Id;
                                attribute.Value.CurrentValue = baseItemValue.Value;
                            }
                            else
                            {
                                if (attribute.TypeName.IsNotNullOrEmpty())
                                {
                                    var enumType = Type.GetType(attribute.TypeName);
                                    var converter = TypeDescriptor.GetConverter(enumType);
                                    attribute.Value.CurrentValueId = Convert.ToInt64(converter.ConvertFrom(attributeValue));
                                }
                                else
                                    attribute.Value.CurrentValueId = Convert.ToInt32(attributeValue);
                            }

                        }
                        break;
                    case AttributeKindEnum.String:
                        attribute.Value.CurrentValue = attributeValue.ToString();
                        break;
                    default:
                        {
                            attribute.Value.CurrentValue = attributeValue;
                            if (attribute.AttributeKind == AttributeKindEnum.Number && dataFormat.HasValue
                                && decimal.TryParse(attributeValue.ToString(), out var decimalValue))
                            {
                                var convertedItem =
                                            ConvertToHelper.Convert(
                                                conversionSystemFrom: conversionSystemFrom,
                                                conversionSystemTo: conversionSystemTo,
                                                dataFormat: dataFormat.GetValueOrDefault(),
                                                value: decimalValue,
                                                applyRound: true);
                                attribute.Value.CurrentValue = (object)convertedItem?.Value ?? decimalValue;
                                attribute.DecimalPrecision = convertedItem?.DecimalPrecision ?? 2;
                            }
                        }
                        break;
                }

            }

            if (attribute.AttributeKind != AttributeKindEnum.String)
            {
                attribute.UMLocalizationKey =
                    $"{DomainExtensions.GENERIC_LABEL}_{attribute.ItemDataFormat.ToString().ToUpper()}_{conversionSystemTo.ToString().ToUpper()}";
            }

            return attribute;
        }

        protected ClusteredAttributeDetailItem ApplyCustomMapping(EntityAttribute attributeValue
                                                        , IEnumerable<ProtectionLevelEnum> protectionLevels
                                                        , MeasurementSystemEnum measurementSystemTo)
        {
            var attributeDetail = Mapper.Map<ClusteredAttributeDetailItem>(attributeValue);
            SetAttributeDetailValue(attributeDetail, protectionLevels, conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                                    , conversionSystemTo: measurementSystemTo);
            attributeDetail.Hidden = (attributeDetail.EnumId == AttributeDefinitionEnum.Length);
            return attributeDetail;
        }

        #endregion < Apply Custom Mapping >



        /// <summary>
        /// Recupero attributi legati ad un tool
        /// </summary>
        /// <param name="toolId"></param>
        /// <param name="conversionSystem"></param>
        /// <returns></returns>
        protected IEnumerable<AttributeDetailItem> GetAttributes(long entityId
                                    , EntityTypeEnum entityType
                                    , bool onlyQuickAccess
                                    , MeasurementSystemEnum conversionSystemTo
                                    , ProcessingTechnologyEnum processingTechnology = ProcessingTechnologyEnum.Default)
        {
            Expression<Func<EntityAttribute, bool>> predicate = a => a.EntityId == entityId;
            if (onlyQuickAccess)
                predicate = predicate.AndAlso(a => a.AttributeScopeId == AttributeScopeEnum.Fundamental);

            var additionalInfos = new Dictionary<AttributeDefinitionEnum, object>();
            var parentType = entityType.ToParentType();
            if (parentType == ParentTypeEnum.Tool)
            {
                additionalInfos.Add(AttributeDefinitionEnum.ToolType, entityType.ToToolType());
            }
            else if (parentType == ParentTypeEnum.ToolRange)
            {
                // Recupero il plantUnit
                var plantUnit =
                    (entityType.ToToolType()).GetEnumAttribute<PlantUnitAttribute>()?.PlantUnit
                                ?? PlantUnitEnum.None;

                additionalInfos.Add(AttributeDefinitionEnum.ToolRangeType, parentType.GetToolRangeType(plantUnit));
            }

            var attributes = AttributeValueRepository.FindEntityAttributes(predicate)
                                    .ToList();

            return ServiceFactory.GetEntityRulesHandlers(entityType.ToParentType())
                    .GetFilteredAttributes(attributes
                            , additionalInfos)
                    .OrderBy(a => a.Priority)
                    .ToList()
                .Select(attribute =>
                {
                    var attributeDetail = Mapper.Map<AttributeDetailItem>(attribute);
                    SetAttributeDetailValue(attributeDetail
                                    , UserSession.GetProtectionLevels()
                                    , conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                                    , conversionSystemTo: conversionSystemTo);

                    // PlasmaGas e ShieldGas sono attributi ReadOnly
                    attributeDetail.IsReadonly = attributeDetail.EnumId is AttributeDefinitionEnum.PlasmaGas or AttributeDefinitionEnum.ShieldGas;
                    return attributeDetail;
                })
                .OrderBy(a => a.Order);
        }

        /// <summary>
        /// Remove single entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        protected Result RemoveEntity(long entityId
                            , IUnitOfWork<IMachineManagentDatabaseContext> unitOfWork)
        {
            try
            {
                EntityRepository.Attach(unitOfWork);
                AttributeValueRepository.Attach(unitOfWork);

                var dbEntity = EntityRepository.Get(entityId);
                if (dbEntity == null)
                {
                    return Result.Fail(ErrorCodesEnum.ERR_GEN002.ToString());
                }

                AttributeValueRepository.Remove(a => a.EntityId == entityId);
                EntityRepository.Remove(dbEntity);
                unitOfWork.Commit();
                return Result.Ok();
            }
            catch (Exception ex)
            {

                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }
        #endregion

    }
}
