namespace Mitrol.Framework.MachineManagement.Application.Models.Setup
{
    using AutoMapper;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Transformations;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    public class GeneralSetupAttributesViewer
    {
        private static readonly Dictionary<string, string> ProfileAttributeLocalization = new Dictionary<string, string>
            {
                { "SA_L", "LBL_ATTR_SA_L" },
                { "SA_V", "LBL_ATTR_SA_L" },
                { "SA_B", "LBL_ATTR_SA_L" },
                { "TA_V", "LBL_ATTR_TA_L" },
                { "TA_L", "LBL_ATTR_TA_L" },
                { "SBA_L", "LBL_ATTR_SB_L" },
                { "SBA_V", "LBL_ATTR_SB_L" },
                { "SBA_B", "LBL_ATTR_SB_L" },
                { "SBA_D", "LBL_ATTR_SBA" },
                { "SBA_I", "LBL_ATTR_SB" },
                { "SBA_T", "LBL_ATTR_SB" },
                { "SBA_U", "LBL_ATTR_SB" },
                { "SBA_Q", "LBL_ATTR_SB" },
                { "SBA_C", "LBL_ATTR_SB" },
                { "SBA_N", "LBL_ATTR_SB" },
                { "TBA_L", "LBL_ATTR_TB_L" },
                { "TBA_V", "LBL_ATTR_TB_L" },
                { "TBA_B", "LBL_ATTR_TB_L" },
                { "TBA_D", "LBL_ATTR_TBA" },
                { "TBA_I", "LBL_ATTR_TB" },
                { "TBA_T", "LBL_ATTR_TB" },
                { "TBA_U", "LBL_ATTR_TB" },
                { "TBA_Q", "LBL_ATTR_TB" },
                { "TBA_C", "LBL_ATTR_TB" },
                { "SA_P", "LBL_ATTR_WIDTH"},
                { "TA_P", "LBL_ATTR_THICKNESS"},
                { "SA_F", "LBL_ATTR_WIDTH"},
                { "TA_F", "LBL_ATTR_THICKNESS"},
            };

        private IServiceFactory _serviceFactory;
        private Dictionary<AttributeDefinitionGroupEnum, BaseGroupItem<AttributeDetailItem>> _generalSetupGroupItem;
        private ProfileTypeEnum _profileType = ProfileTypeEnum.L;

        public GeneralSetupAttributesViewer(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _generalSetupGroupItem = new Dictionary<AttributeDefinitionGroupEnum, BaseGroupItem<AttributeDetailItem>>();
            GetAttributesGroups();
        }

        /// <summary>
        /// Prepara le strutture necessarie per creare il pacchetto di dati per la pagina del Setup Generale
        /// </summary>
        /// <returns></returns>
        private void GetAttributesGroups()
        {
            var attributes = new List<BaseGroupItem<AttributeDetailItem>>();
            var groups = new Dictionary<AttributeDefinitionGroupEnum, BaseGroupItem<AttributeDetailItem>>();
            var properties = typeof(GeneralSetup).GetProperties();
            var baseGroupItem = new BaseGroupItem<AttributeDetailItem>();

            // Ciclo tra tutte le proprietà del Setup Generale per ottenere quelle che hanno l'attributo  GeneralSetupPropertiesAttribute (indica che la proprietà
            // deve essere visualizzata)
            foreach (var proInfo in properties)
            {
                var generalSetupAttributes = (GeneralSetupPropertiesAttribute[])proInfo.GetCustomAttributes(typeof(GeneralSetupPropertiesAttribute), inherit: false);
                // Se la proprietà ha almeno un attributo di tipo GeneralSetupPropertiesAttribute
                if (generalSetupAttributes.Length > 0)
                {
                    // Recupero dall'attributo l'attributeDefinitionEnum che caratterizza la proprietà. 
                    var attributeDefinition = generalSetupAttributes[0].AttributeDefinition;
                    
                    // Recupero il gruppo e nel caso non ci sia lo creo
                    if (groups.ContainsKey(generalSetupAttributes[0].Group) == false)
                    {
                        // Creo il gruppo
                        baseGroupItem = new BaseGroupItem<AttributeDetailItem>();
                        baseGroupItem.LocalizationKey = $"{ MachineManagementExtensions.LABEL_GROUP }_{generalSetupAttributes[0].Group.ToString().ToUpper()}";
                        groups.Add(generalSetupAttributes[0].Group, baseGroupItem);
                    }
                    else
                    {
                        // Recupero il gruppo
                        baseGroupItem = groups[generalSetupAttributes[0].Group];
                    }
                    // Costruisco l'attributo 
                    var attribute = new AttributeDetailItem();
                    var attributeDefinitionInfo = DomainExtensions.GetEnumAttribute<AttributeDefinitionEnum, AttributeInfoAttribute>(attributeDefinition);
                    var DatabaseDisplayNameInfo = DomainExtensions.GetEnumAttribute<AttributeDefinitionEnum, DatabaseDisplayNameAttribute>(attributeDefinition);
                    attribute.DisplayName = DatabaseDisplayNameInfo.DisplayName;
                    // TO DO
                    //attribute.AttributeKind = attributeDefinitionInfo.AttributeKind;
                    //attribute.AttributeType = attributeDefinitionInfo.AttributeType;
                    //attribute.ControlType = attributeDefinitionInfo.ControlType;             
                    attribute.EnumId = attributeDefinition;
                    attribute.IsCodeGenerator = false;
                    attribute.IsFake = false;
                    attribute.AttributeScopeId = AttributeScopeEnum.Optional;
                    attribute.Order = generalSetupAttributes[0].AttributeOrderNumber;
                    attribute.LocalizationKey = $"{MachineManagementExtensions.LABEL_ATTRIBUTE}_{attributeDefinition.ToString().ToUpper()}";
                    attribute.ItemDataFormat = attributeDefinitionInfo.AttributeDataFormat;

                    // Se l'attributo è un enumerato devo recuperare la lista dei valori della combo/listBox
                    if (attributeDefinitionInfo.AttributeKind == Framework.Domain.Enums.AttributeKindEnum.Enum)
                    {                   
                        if (attributeDefinitionInfo.ValueType == ValueTypeEnum.DynamicEnum)
                        {
                            // Caso di enumerativi la cui lista deve essere recuperata direttamente via web api
                            attribute.Value.Source = attributeDefinitionInfo.Url;
                        }
                        else
                        {
                            // Recupero la lista degli enumerativi
                            var enumManagement = _serviceFactory.Resolve<IAttributeDefinitionEnumManagement, AttributeDefinitionEnum>(attributeDefinition);
                            var sources = enumManagement.FindAttributeSourceValues();
                            var _mapper = _serviceFactory.GetService<IMapper>();
                            var sourcesToExpose = _mapper.Map<IEnumerable<AttributeSourceValueItem>>(sources).ToList();
                            //Ordino in maniera "naturale" per LocalizedText solo se i valori 
                            //del source non devono essere tradotti perchè valori non forniti da db
                            if (sources.All(value => value.MustBeTranslated))
                            {
                                attribute.Value.Source = sourcesToExpose.OrderNatural(a => a.Value.ToString());
                            }
                            else
                            {
                                attribute.Value.Source = sourcesToExpose.OrderNatural(a => a.LocalizedText.ToString());
                            }
                            attribute.TypeName = attributeDefinitionInfo.TypeName;
                        }
                        attribute.Value.ValueType = attributeDefinitionInfo.ValueType;
                    }
                    var details2 = baseGroupItem.Details.ToList();
                    details2.Add(attribute);
                    // Attributi ordinati in base alla proprietà "Order";
                    baseGroupItem.Details = details2.OrderBy(attribute=>attribute.Order);
                }
            }
            _generalSetupGroupItem = groups;
        }

        /// <summary>
        /// Funzione che fornisce il pacchetto di attributi con i dati aggiornati in base al Setup Generale e al sistema di misura passato
        /// </summary>
        /// <param name="generalSetup">Dati del setup generale</param>
        /// <param name="measurementSystemTo">Sistema di misura in cui i dati devono essere visualizzati</param>
        /// <returns></returns>
        public GeneralSetupDetailItem UpdaterData(GeneralSetup generalSetup,MeasurementSystemEnum measurementSystemTo)
        {
            var generalSetupType = generalSetup.GetType();
            var properties = generalSetupType.GetProperties();
            // Aggiorno il tipo di profilo corrente 
            UpdateProfileType(generalSetup);

            // Ciclo tra tutte le proprietà del Setup Generale per ottenere quelle che hanno l'attributo  GeneralSetupPropertiesAttribute (indica che la proprietà
            // deve essere visualizzata)
            foreach (var proInfo in properties)
            {
                var generalSetupAttributes = (GeneralSetupPropertiesAttribute[])proInfo.GetCustomAttributes(typeof(GeneralSetupPropertiesAttribute), inherit: false);
                // Se la proprietà ha almeno un attributo di tipo GeneralSetupPropertiesAttribute
                if (generalSetupAttributes.Length > 0)
                {
                    var attributeDefinition = generalSetupAttributes[0].AttributeDefinition;
                    var attributeDefinitionInfo = DomainExtensions.GetEnumAttribute<AttributeDefinitionEnum, AttributeInfoAttribute>(attributeDefinition);
                    
                    // Recupero il valore della proprietà
                    var propertyToAdd = proInfo.GetValue(generalSetup);
                    if (_generalSetupGroupItem.ContainsKey(generalSetupAttributes[0].Group))
                    {
                        var attribute = _generalSetupGroupItem[generalSetupAttributes[0].Group].Details.Where(a => a.EnumId==generalSetupAttributes[0].AttributeDefinition).FirstOrDefault();
                        if (attribute != null)
                        {
                            // Se non ho trovato la proprietà allora l'attributo viene nascosto
                            if (propertyToAdd == null)
                            {
                                attribute.Hidden = true;
                                continue;
                            }
                            // Recupero lo stato dell'attributo
                            attribute.AttributeStatus = GetStatus((ISetupData)propertyToAdd);
                            // Nel caso in cui lo stato del Setup generale è SetupActionEnum.ToValidate allora vuol dire che i dati sono vecchi
                            // e quindi non mostro l'icona
                            if (generalSetup.SetupAction == SetupActionEnum.ToValidate)
                                attribute.AttributeStatus.Status = EntityStatusEnum.NoIconToDisplay;

                            // Recupero lo stato Hidden
                            attribute.Hidden = IsHidden((ISetupData)propertyToAdd);

                            var valueToAdd = ((ISetupData)propertyToAdd).GetValueDependingByStatus();
                            // Se l'attributo è un numero devo prima convertirlo prima di assegnarlo
                            if (attribute.AttributeKind == AttributeKindEnum.Number)
                            {
                                if (float.TryParse(valueToAdd.ToString(), out var floatvalue))
                                {
                                    ConvertedItem item = ConvertToHelper.ConvertForLabel(MeasurementSystemEnum.MetricSystem,
                                                                                measurementSystemTo,
                                                                                attribute.ItemDataFormat,
                                                                                (decimal)floatvalue);
                                    attribute.UMLocalizationKey = item.UmLocalizationKey;
                                    attribute.Value.CurrentValue = (float)item.Value;
                                    attribute.DecimalPrecision = item.DecimalPrecision;
                                }
                            }
                            else if (attributeDefinitionInfo.AttributeKind == AttributeKindEnum.Enum && valueToAdd != null)
                            {
                                // Se l'attributo è un enumerativo devo trovare l'indice (DB o Enum) corrispondente 
                                var enumManagement = _serviceFactory.Resolve<IAttributeDefinitionEnumManagement, AttributeDefinitionEnum>(attributeDefinition);
                                var enumfound = enumManagement.GetEnumValueFromSerializationName(valueToAdd.ToString());
                                if (enumfound != null)
                                {
                                    var val = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>(enumfound);
                                    attribute.Value.CurrentValueId = Convert.ToInt64(val.Id);
                                    attribute.Value.CurrentValue = valueToAdd;
                                }
                            }

                            // Sistemazione localizzazione degli attributi del profilo a seconda dell'attributo e del tipo di profilo
                            var newLocalizationKey = ProfileAttributeLocalization.GetValueOrDefault(
                                             (attributeDefinition.ToString() + "_" + _profileType).ToString().ToUpper());
                            if (newLocalizationKey != null)
                                attribute.LocalizationKey = newLocalizationKey;
                            else
                                attribute.LocalizationKey = $"{MachineManagementExtensions.LABEL_ATTRIBUTE}_{attributeDefinition.ToString().ToUpper()}";
                        }
                    }
                }
            }
            // Recupero i gruppi ordinati in base agli enumerativi dei gruppi e attributi Hidden =true dei gruppi non vengono propagati al FE
            var readOnlyGroups = _generalSetupGroupItem.Where(g => g.Key!=AttributeDefinitionGroupEnum.Generic).OrderBy(g => g.Key)
                   .Select(g =>
                    {
                        var details = g.Value.Details.Where(detail => detail.Hidden == false);
                        if (details.Count() > 0)
                        {
                            var group = new GeneralSetupBaseGroupItem();
                            group.Hidden = g.Value.Hidden;
                            group.LocalizationKey = g.Value.LocalizationKey;
                            group.Priority = g.Value.Priority;
                            group.Details = details.Where(detail => detail.Hidden == false).ToArray();
                            return group;
                        }
                        return null;
                    })
                   .Where(group =>group!=null).ToList();

            var attributes = _generalSetupGroupItem.Where(g => g.Key == AttributeDefinitionGroupEnum.Generic)
                                                    .Select(g => g.Value.Details)
                                                    .SelectMany(details => details)
                                                    .Where(details=> details.Hidden==false);
                                                  
            return  new GeneralSetupDetailItem
            {
                ReadOnlyAttributesGroups = readOnlyGroups.ToArray(),
                Attributes = attributes.ToArray(),
                ImageCode = $"PRF_{(_profileType).ToString().ToUpper()}",
                UpdatedData = generalSetup.SetupAction == SetupActionEnum.ToValidate ? false : true
            };
        }

        /// <summary>
        /// Recupero il tipo di profilo da Setup generale
        /// </summary>
        /// <param name="generalSetup"></param>
        private void UpdateProfileType(GeneralSetup generalSetup)
        {
            var generalSetupType = generalSetup.GetType();
            var properties = generalSetupType.GetProperties();

            foreach (var proInfo in properties)
            {
                var generalSetupAttributes = ((GeneralSetupPropertiesAttribute[])proInfo.GetCustomAttributes(typeof(GeneralSetupPropertiesAttribute), inherit: false))
                    .Where(cust=> cust.AttributeDefinition==AttributeDefinitionEnum.ProfileType);
                if (generalSetupAttributes.Count() > 0)
                {
                    var propertyToAdd = proInfo.GetValue(generalSetup);
                    if (propertyToAdd == null)
                        break;

                    string objectType = ((ISetupData)propertyToAdd).GetValueDependingByStatus().ToString();
                    // Assegno il nuovo profilo 
                    Enum.TryParse<ProfileTypeEnum>(objectType,out  _profileType);
                }
            }
        }

        /// <summary>
        /// In base allo stato della proprietà restituisco l'AttributeStatus corrispondente 
        /// </summary>
        /// <param name="propertyData"></param>
        /// <returns></returns>
        private AttributeStatus GetStatus(ISetupData propertyData)
        {
            var status= new AttributeStatus() { ErrorLocalizationKey = "", Status = EntityStatusEnum.Available };
            switch (propertyData.GetStatus())
            {
                case SetupActionEnum.RequiredConfirm:
                    status.Status = EntityStatusEnum.Warning;
                    break;
                case SetupActionEnum.NotUsed:
                    status.Status = EntityStatusEnum.NoIconToDisplay;
                    break;
                case SetupActionEnum.Ok:
                    status.Status = EntityStatusEnum.Available;
                    break;
                default:
                    break;
            }
            return status;
        }


        /// <summary>
        /// Se lo stato della proprietà è SetupActionEnum.NotRequired allora la proprietà viene nascosta
        /// </summary>
        /// <param name="propertyData"></param>
        /// <returns></returns>
        private bool IsHidden(ISetupData propertyData) => propertyData.GetStatus() == SetupActionEnum.NotRequired ? true : false;
      
        /// <summary>
        /// Aggiorna i dati del GeneralSetup con quelli passati dal FE
        /// </summary>
        /// <param name="details">Lista di attributi con i nuovi valori</param>
        /// <param name="generalSetup">General setup da aggiornare</param>
        /// <returns></returns>
        public bool SetGeneralSetupData(List<AttributeDetailItem> details, GeneralSetup generalSetup)
        {
            // Recupero le proprietà del GeneralSetup
            var properties = typeof(GeneralSetup).GetProperties();

            // Per ogni attributo che si vuole aggiornare
            foreach (var attribute in details)
            {
                // Identifico la proprietà che deve essere aggiornata
                var property = properties.Where(prop =>
                              ((GeneralSetupPropertiesAttribute[])prop.GetCustomAttributes(typeof(GeneralSetupPropertiesAttribute), inherit: false)).Length > 0 &&
                              ((GeneralSetupPropertiesAttribute[])prop.GetCustomAttributes(typeof(GeneralSetupPropertiesAttribute), inherit: false))[0].AttributeDefinition == attribute.EnumId
                ).ToList();
                // Se la proprietà esiste
                if (property != null && property.Count > 0)
                {
                    if (attribute.AttributeKind == AttributeKindEnum.Enum)
                    {
                        // Caso enumerato statico 
                        if (attribute.Value.ValueType == ValueTypeEnum.StaticEnum)
                        {
                            var propertyToAdd = (ISetupData)property[0].GetValue(generalSetup);
                            if (propertyToAdd != null)
                            {
                                //Type dell'enumerativo
                                Type t = Type.GetType(attribute.TypeName);

                                //Ottengo il Converter
                                var typeConverter = TypeDescriptor.GetConverter(t);

                                //converto in un tipo enumerativo il CurrentValueId
                                var enumerativeValue = typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture
                                        , attribute.Value.CurrentValueId);

                                propertyToAdd.SetCurrentValue(enumerativeValue);
                                propertyToAdd.SetRequiredValue(enumerativeValue);
                            }
                        }
                        else if (attribute.Value.ValueType == ValueTypeEnum.DynamicEnum)
                        {
                            // Devo trovare dall' attribute.Value.CurrentValueId la stringa corretta(lato DB) corrispondente al Id indicato e poi assegnarla
                            var enumManagement = _serviceFactory.Resolve<IAttributeDefinitionEnumManagement, AttributeDefinitionEnum>(attribute.EnumId);
                            var enumfound = enumManagement.GetNameToExportFromValue(new BaseInfoItem<long, string>(id: attribute.Value.CurrentValueId, value: ""));
                            var propertyToAdd = (ISetupData)property[0].GetValue(generalSetup);
                            
                            if (propertyToAdd != null && enumfound != null)
                            {
                                var val = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>(JsonConvert.SerializeObject(enumfound));
                                propertyToAdd.SetCurrentValue(val.Value);
                                propertyToAdd.SetRequiredValue(val.Value); 
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
