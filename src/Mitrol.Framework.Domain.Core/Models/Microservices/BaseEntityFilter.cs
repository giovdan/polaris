namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class BaseEntityFilter
    {
        [JsonProperty("Id")]
        public long? Id { get; set; }
        MeasurementSystemEnum _conversionSystemTo;
        [JsonIgnore()]
        public MeasurementSystemEnum ConversionSystemTo { 
                get {
                    return _conversionSystemTo == MeasurementSystemEnum.FractionalImperialSystem 
                    ? MeasurementSystemEnum.ImperialSystem
                    : _conversionSystemTo; 
                } 
            set { _conversionSystemTo = value; } 
        }
        MeasurementSystemEnum _conversionSystemFrom;
        [JsonIgnore()]
        public MeasurementSystemEnum ConversionSystemFrom {
            get {
                    return _conversionSystemFrom == MeasurementSystemEnum.FractionalImperialSystem
                    ? MeasurementSystemEnum.ImperialSystem
                    : _conversionSystemFrom;
            }
            set { _conversionSystemFrom = value; } 
        }

        public BaseEntityFilter()
        {
            ConversionSystemFrom = MeasurementSystemEnum.MetricSystem;
            ConversionSystemTo = MeasurementSystemEnum.MetricSystem;
        }

        public override string ToString()
        {
            var properties = this.GetType().GetProperties();
            var propertyValues = new List<string>();
            foreach (var property in properties)
            {
                var value = property.GetValue(this);
                if (value != null)
                    propertyValues.Add(value.ToString());
            }

            return string.Join("_", propertyValues);
        }
    }
}
