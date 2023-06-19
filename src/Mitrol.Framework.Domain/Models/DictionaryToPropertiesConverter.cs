namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Linq;

    public class DictionaryToPropertiesJsonConverter : CustomCreationConverter<BaseImportItem>
    {
        #region Overrides of CustomCreationConverter<BaseImportItem>
        // Serve a serializzare gli identificatori come proprietà gestendo la priorità impostata nel JsonProperty.
        // Per la deserializzazione viene utilizzato un deserializzatore standard e l'attributo JsonExtensionData.
        // L'attributo JsonExtensionData funzionerebbe anche in serializzazione ma gli identificatori vengono messi per ultimi per cui perdono d'enfasi.
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            // Write properties.
            var propertyInfos = value.GetType().GetProperties().ToList()
                                    .Where(pi =>
                                    {
                                        // Se è stata impostata l'attributo di "JsonIgnore" per questa proprietà non deve essere serializzato
                                        JsonIgnoreAttribute[] attributes = (JsonIgnoreAttribute[])pi.GetCustomAttributes(typeof(JsonIgnoreAttribute), false);
                                        if (attributes.Count() > 0)
                                            return false;
                                        return true;
                                    })
                                    .Select(pi =>
                                    {
                                        // Se è presente l'attributo di "JsonProperty" memorizzo l'ordine e il nome della proprietà per la serializzazione
                                        JsonPropertyAttribute[] propertiesAttributes = (JsonPropertyAttribute[])pi.GetCustomAttributes(typeof(JsonPropertyAttribute), false);
                                        var order = 0;
                                        string propertyName = pi.Name;
                                        if (propertiesAttributes.Count() > 0)
                                        {
                                            order = propertiesAttributes.First().Order;
                                            propertyName = propertiesAttributes.First().PropertyName ?? pi.Name;
                                        }
                                        return new { order, propertyName, pi };
                                    })
                                    // Metto in ordine le proprietà in base all'ordine
                                    .OrderBy(pi => pi.order);

            foreach (var propertyInfo in propertyInfos)
            {
                // Per la proprietà (dictionary)"Identifiers" deve trasformarla in singole proprietà .
                //TODO@simona: togliere la stringa mettendo un attributo per evitare l'eventuale cambio di nome della proprietà "Identifiers"
                if (propertyInfo.propertyName == "Identifiers")
                {
                    var test = (BaseImportItem)value;
                    foreach (var kvp in test.Identifiers)
                    {
                        writer.WritePropertyName(kvp.Key);
                        serializer.Serialize(writer, kvp.Value);
                    }
                }
                else
                {        
                    writer.WritePropertyName(propertyInfo.propertyName);
                    var propertyValue = propertyInfo.pi.GetValue(value);
                    serializer.Serialize(writer, propertyValue);
                }
            }
            writer.WriteEndObject();
        }

        public override BaseImportItem Create(Type objectType)
        {
            return new BaseImportItem();
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        #endregion
    }

}
