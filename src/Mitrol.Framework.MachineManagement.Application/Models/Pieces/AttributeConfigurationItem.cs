using Mitrol.Framework.Domain.Enums;
using Mitrol.Framework.Domain.Macro;
using Mitrol.Framework.MachineManagement.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Mitrol.Framework.MachineManagement.Application.Models.Production.Pieces
{
    public class AttributeConfigurationItem
    {
        public AttributeDefinitionEnum DisplayName { get; private set; }

        public AttributeDataFormatEnum? DataFormat { get; set; }

        public string LocalizationKey { get;  set; }

        public string Value { get; set; }

        public List<string> SourceValues { get; set; }

        public AttributeConfigurationItem(AttributeConfiguration attributeConfiguration)
        {
            LocalizationKey = attributeConfiguration.LocalizationKey;
            var enumCustomName = TypeDescriptor.GetConverter(typeof(AttributeDefinitionEnum));
            try
            {
                DisplayName = (AttributeDefinitionEnum)enumCustomName.ConvertFrom(null, CultureInfo.InvariantCulture, attributeConfiguration.Parameter);
            
                if ((attributeConfiguration.DataFormat != null) && (!string.Empty.Equals(attributeConfiguration.DataFormat)))
                {
                    var enumCustomSerialDataFormat = TypeDescriptor.GetConverter(typeof(AttributeDataFormatEnum));                   
                    DataFormat = (AttributeDataFormatEnum)enumCustomSerialDataFormat.ConvertFrom(null, CultureInfo.InvariantCulture, attributeConfiguration.DataFormat);
                }
            }
            catch
            { }
        }

        public AttributeConfigurationItem(MacroAttributeStructure attributeConfiguration)
        {
            SourceValues = new List<string>();
           
            try
            {
                DisplayName = attributeConfiguration.AttributeDefinitionName;
            }
            catch 
            { }
        }

    }

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum ConfigurationTypeEnum
    {
        [EnumSerializationName("AlwaysOn")]
        AlwaysOn = 0,
        
        [EnumSerializationName("Absent")] 
        Absent,
        
        [EnumSerializationName("AttributeConfiguration")]
        AttributeConfiguration,

        [EnumSerializationName("GroupConfiguration")]
        GroupConfiguration

    }

    public class MacroAttributeStructure
    {
        [JsonProperty("AttributeName")]
        public string AttributeName { get; set; }

        [JsonProperty("GroupName")]
        public string GroupName { get; set; }

        [JsonProperty("ConfigurationType")]
        public ConfigurationTypeEnum ConfigurationType { get; set; }

        [JsonIgnore]
        public AttributeDefinitionEnum AttributeDefinitionName 
        {
            get 
            {
                var enumCustomName = TypeDescriptor.GetConverter(typeof(AttributeDefinitionEnum));
                return  (AttributeDefinitionEnum)enumCustomName.ConvertFrom(null, CultureInfo.InvariantCulture, AttributeName);          
            }
        }
    }
}
