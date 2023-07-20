namespace Mitrol.Framework.MachineManagement.Application.Models.General
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class OverrideValueItem
    {
        [JsonProperty("Value", Order = 1)]
        public object Value { get; set; }

        [JsonProperty("OverrideValue", Order = 2)]
        public object OverrideValue { get; set; }

        [JsonProperty("OverrideType", Order = 3)]
        public string OverrideType { get; set; }

        static readonly string s_defaultOverrideType = DomainExtensions.GetDefaultEnumValueFromTypename(typeof(OverrideTypeEnum).AssemblyQualifiedName.ToString()).ToString();

        public static OverrideValueItem GetOverrideObject(string objectToSerialize)
        {
            try
            {
                var OvValue = JsonConvert.DeserializeObject<OverrideValueItem>(objectToSerialize.ToString());
                if (OvValue == null)
                    OvValue = new OverrideValueItem();
                OvValue.OverrideValue ??= 0;
                OvValue.OverrideType ??= s_defaultOverrideType;
                OvValue.Value ??= 0;
                return OvValue;
            }
            catch
            {
                if (decimal.TryParse(objectToSerialize.ToString(), out decimal decresult))
                {
                    var OvValue = new OverrideValueItem
                    {
                        OverrideValue = 0,
                        OverrideType = s_defaultOverrideType,
                        Value = decresult
                    };
                    return OvValue;
                }
                else
                    throw;
            }
        }

    }
}
