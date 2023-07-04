


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

    public class MachineManagementBaseService: BaseService
    {
        protected IEntityRepository EntityRepository => ServiceFactory.GetService<IEntityRepository>();
        protected IAttributeValueRepository AttributeValueRepository => ServiceFactory.GetService<IAttributeValueRepository>();
        protected IAttributeDefinitionLinkRepository AttributeDefinitionLinkRepository 
                        => ServiceFactory.GetService<IAttributeDefinitionLinkRepository>();
        protected IUnitOfWorkFactory<IMachineManagentDatabaseContext> UnitOfWorkFactory => ServiceFactory
                    .GetService<IUnitOfWorkFactory<IMachineManagentDatabaseContext>>();

        public MachineManagementBaseService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        /// <summary>
        /// Get attribute definitions for specified entityType
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public IEnumerable<AttributeDetailItem> GetAttributeDefinitions(EntityTypeEnum entityType)
        {
            using var unitOfWork = UnitOfWorkFactory.GetOrCreate(UserSession);
            AttributeDefinitionLinkRepository.Attach(unitOfWork);
            return Mapper.Map<IEnumerable<AttributeDetailItem>>(AttributeDefinitionLinkRepository
                        .FindBy(adl => adl.EntityTypeId == entityType));
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

        #endregion < Apply Custom Mapping >
    }
}
