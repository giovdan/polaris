namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    [DependencyObjectForExport(ImportExportObjectEnum.MaterialObject)]
    [DependencyObjectForExport(ImportExportObjectEnum.ProfileObject)]
    public class BaseStockImportItem : BaseImportItem
    {
     
        public BaseStockImportItem() : base()
        {
        }
        public virtual ProfileTypeEnum GetProfile()
        {
            //Recupero il serializationName del ProfileType
            var serializationNameProfileType = AttributeDefinitionConverter.ConvertTo(null
                                                                            , CultureInfo.InvariantCulture
                                                                            , AttributeDefinitionEnum.ProfileType
                                                                            , typeof(string))
                                                                            .ToString();
            //Recupero l'attributo corrispondente al ProfileType
            var profileTypeObject = Attributes.Where(attribute => attribute.Key == serializationNameProfileType).Single();

            //Recupero gli attributi relativi al AttributeDefinition del ProfileTypeEnum
            var res = DomainExtensions.GetIntEnumValueFromString(typeof(ProfileTypeEnum).AssemblyQualifiedName.ToString(), profileTypeObject.Value.ToString());

            if (res.Success)
                return (ProfileTypeEnum)res.Value;

            return (ProfileTypeEnum)DomainExtensions.GetDefaultEnumValueFromTypename(typeof(ProfileTypeEnum).AssemblyQualifiedName.ToString());

        }
  
        public override void AddAttribute(AttributeDefinitionEnum enumId, object value)
        {
            //Recupero il nome dell'attributo da esportare: sarà un EnumcustomNameAttribute se definito altrimenti sarà il DisplayName 
            string displayNameToExport = AttributeDefinitionConverter.ConvertTo(null
                                            , CultureInfo.InvariantCulture
                                            , enumId
                                            , typeof(string))
                                            .ToString();


            if (enumId == AttributeDefinitionEnum.ProfileCode)
            {
                var EnumVal = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>(JsonConvert.SerializeObject(value));
                Attributes.Add(displayNameToExport, EnumVal.Value);
                Identifiers.Add(displayNameToExport, EnumVal.Value);
                if (EnumVal.Id>0)
                    Dependencies.Add(new KeyValuePair<ImportExportObjectEnum, long>(ImportExportObjectEnum.ProfileObject, EnumVal.Id));
            }
            else if (enumId == AttributeDefinitionEnum.MaterialCode)
            {
                var EnumVal = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>(JsonConvert.SerializeObject(value));
                Attributes.Add(displayNameToExport, EnumVal.Value);
                Identifiers.Add(displayNameToExport, EnumVal.Value);
                if (EnumVal.Id > 0)
                    Dependencies.Add(new KeyValuePair<ImportExportObjectEnum, long>(ImportExportObjectEnum.MaterialObject, EnumVal.Id));
            }
            else
            {
                if (enumId==AttributeDefinitionEnum.Thickness || enumId == AttributeDefinitionEnum.Width || enumId == AttributeDefinitionEnum.Length)
                    Identifiers.Add(displayNameToExport, value);

                Attributes.Add(displayNameToExport, value);
            }
            
        }

        public override void Convert(JObject jobject)
        {
            throw new System.NotImplementedException();
        }

        public override string GetIdentifiersString()
        {
            var profileType = GetProfile();

            //Recupero l'attributo corrispondente alla Lunghezza
            var lenghtObject = Identifiers.Where(attribute => attribute.Key == AttributeDefinitionConverter.ConvertTo(null
                                                                            , CultureInfo.InvariantCulture
                                                                            , AttributeDefinitionEnum.Length
                                                                            , typeof(string))
                                                                            .ToString()).Single();
            var materialObject = Identifiers.Where(attribute => attribute.Key == AttributeDefinitionConverter.ConvertTo(null
                                                                            , CultureInfo.InvariantCulture
                                                                            , AttributeDefinitionEnum.MaterialCode
                                                                            , typeof(string))
                                                                            .ToString()).Single();

            if (GetProfile() == ProfileTypeEnum.P)
            {
                var thicknessObject = Identifiers.Where(attribute => attribute.Key == AttributeDefinitionConverter.ConvertTo(null
                                                                           , CultureInfo.InvariantCulture
                                                                           , AttributeDefinitionEnum.Thickness
                                                                           , typeof(string))
                                                                           .ToString()).Single();
                var widthObject = Identifiers.Where(attribute => attribute.Key == AttributeDefinitionConverter.ConvertTo(null
                                                                          , CultureInfo.InvariantCulture
                                                                          , AttributeDefinitionEnum.Width
                                                                          , typeof(string))
                                                                          .ToString()).Single();

                return $"{profileType}" +
                        $"-{Math.Truncate((decimal)widthObject.Value)}" +
                        $"-{Math.Truncate((decimal)lenghtObject.Value)}" +
                        $"-{Math.Truncate((decimal)thicknessObject.Value)}" +
                        $"-{materialObject.Value}";
            }
            else
            {
                var profileObject = Identifiers.Where(attribute => attribute.Key == AttributeDefinitionConverter.ConvertTo(null
                                                                          , CultureInfo.InvariantCulture
                                                                          , AttributeDefinitionEnum.ProfileCode
                                                                          , typeof(string)).ToString()).Single();
                return $"{profileType}" +
                        $"-{Math.Truncate((decimal)lenghtObject.Value)}" +
                        $"-{materialObject.Value}" +
                        $"-{profileObject.Value}";
            }
        }
    }
}
