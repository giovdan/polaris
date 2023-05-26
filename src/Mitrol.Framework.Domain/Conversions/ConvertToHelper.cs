namespace Mitrol.Framework.Domain.Conversions
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class ConvertableItem : IConvertable
    {
        [JsonProperty("ItemDataFormat")]
        public AttributeDataFormatEnum ItemDataFormat { get; set; }
        [JsonProperty("UMLocalizationKey")]
        public string UMLocalizationKey { get; set; }
        [JsonProperty("DecimalPrecision")]
        public int DecimalPrecision { get; set; }
    }

    /// <summary>
    /// Classe che gestisce le conversioni
    /// </summary>
    public abstract class Converter
    {
        public abstract decimal ConvertLength(decimal value);
        public abstract decimal ConvertPressure(decimal value);
        public abstract decimal ConvertSpecificWeight(decimal value);
        public abstract decimal ConvertSection(decimal value);
        public abstract decimal ConvertLinearWeight(decimal value);
        public abstract decimal ConvertWeight(decimal value);
        public abstract decimal ConvertArea(decimal value);
        public abstract decimal ConvertSurface(decimal value);
        public abstract decimal ConvertSpeed(decimal value);
        public abstract decimal ConvertPeripheralSpeed(decimal value);
        public abstract decimal ConvertDistance(decimal value);
        public abstract decimal ConvertLinearSurface(decimal value);
    }

    public class ConvertedItem
    {
        public decimal Value { get; set; }
        public int DecimalPrecision { get; set; }
        public string UmLocalizationKey { get; set; }
        public ConvertedItem()
        {
            DecimalPrecision = 0;
            UmLocalizationKey = default;
        }

        public ConvertedItem(decimal value, int decimalPrecision, string localizationKey)
        {
            Value = value;
            DecimalPrecision = decimalPrecision;
            UmLocalizationKey = localizationKey;
        }

        

    }

    public static class ConvertToHelper
    {
        
        public static ConvertedItem Convert(MeasurementSystemEnum conversionSystemFrom
                                        , MeasurementSystemEnum conversionSystemTo
                                        , AttributeDataFormatEnum dataFormat, decimal value
                                        , bool applyRound = false)
        {
            string umLocalizationKey = $"LBL_{dataFormat.ToString().ToUpper()}_{conversionSystemTo.ToString().ToUpper()}";
            var decimalDigits = DomainExtensions.GetEnumAttributes<AttributeDataFormatEnum
                                , DecimalPrecisionAttribute>(
                                dataFormat)?.SingleOrDefault(x => x.SystemOfMeasure == conversionSystemTo)?.NumberOfDigits ?? 1;
            if (value == 0)
                return new ConvertedItem(0, decimalDigits, umLocalizationKey);

            decimal convertedValue = value;

            if (conversionSystemFrom == conversionSystemTo)
            {
                return new ConvertedItem(convertedValue, decimalDigits, umLocalizationKey);
            }

            dynamic converter = new MetricToImperialSystemConverter();
            if (conversionSystemTo == MeasurementSystemEnum.MetricSystem)
                converter = new ImperialToMetricSystemConverter();

            switch (dataFormat)
            {
                case AttributeDataFormatEnum.Length:
                case AttributeDataFormatEnum.Diameter:
                    convertedValue = converter.ConvertLength(value);
                    break;
                case AttributeDataFormatEnum.Distance:
                    convertedValue = converter.ConvertDistance(value);
                    break;
                case AttributeDataFormatEnum.Surface:
                    convertedValue = converter.ConvertSurface(value);
                    break;
                case AttributeDataFormatEnum.LinearWeight:
                    convertedValue = converter.ConvertLinearWeight(value);
                    break;
                case AttributeDataFormatEnum.Pressure:
                    convertedValue = converter.ConvertPressure(value);
                    break;
                case AttributeDataFormatEnum.Weight:
                    convertedValue = converter.ConvertWeight(value);
                    break;
                case AttributeDataFormatEnum.SpecificWeight:
                    convertedValue = converter.ConvertSpecificWeight(value);
                    break;
                case AttributeDataFormatEnum.Section:
                    convertedValue = converter.ConvertSection(value);
                    break;
                case AttributeDataFormatEnum.LinearSpeed:
                case AttributeDataFormatEnum.RevolutionSpeed:
                case AttributeDataFormatEnum.LinearSpeedSec:
                    convertedValue = converter.ConvertSpeed(value);
                    break;
                case AttributeDataFormatEnum.PeripheralSpeed:
                    convertedValue = converter.ConvertPeripheralSpeed(value);
                    break;
                case AttributeDataFormatEnum.LinearSurface:
                    convertedValue = converter.ConvertLinearSurface(value);
                    break;
            }

            //Se non ha cifre decimali
            if (convertedValue % 1 == 0)
                decimalDigits = 1;

            if (decimalDigits > 0 && applyRound)
                convertedValue = decimal.Round(convertedValue, decimalDigits);


            return new ConvertedItem(convertedValue, decimalDigits, umLocalizationKey);
        }

        public static ConvertedItem ConvertForLabel(MeasurementSystemEnum conversionSystemFrom
                                     , MeasurementSystemEnum conversionSystemTo
                                     , AttributeDataFormatEnum dataFormat, decimal value)
        {
            var convertedItem = Convert(conversionSystemFrom,conversionSystemTo,dataFormat,value,false);
            var decimalDigitsForLabel = DomainExtensions.GetEnumAttributes<AttributeDataFormatEnum, DecimalPrecisionAttribute>(
                               dataFormat)?.SingleOrDefault(x => x.SystemOfMeasure == conversionSystemTo)?.NumberOfDigitsForLabel ?? 1;


            if (decimalDigitsForLabel < convertedItem.DecimalPrecision)
            {
                convertedItem.Value = decimal.Round(convertedItem.Value, decimalDigitsForLabel);
                convertedItem.DecimalPrecision = decimalDigitsForLabel;
            }
            return convertedItem;
           
        }
    }


    public class ConvertToHelper<T>
        where T : IConvertable, new()
    {
        /// <summary>
        /// Conversion of Enumerable<IConvertable>
        /// </summary>
        /// <param name="conversionSystemFrom"></param>
        /// <param name="conversionSystemTo"></param>
        /// <param name="entities"></param>
        /// <param name="setUMLocalizationKey">Flag che forza l'assegnazione della chiave di localizzazione per l'unità di misura</param>
        /// <returns></returns>
        public static IEnumerable<T> Convert(MeasurementSystemEnum conversionSystemFrom 
                                        ,MeasurementSystemEnum conversionSystemTo, IEnumerable<T> entities
                                        ,bool setUMLocalizationKey = true)
        {
            var list = new List<T>();
            foreach(var e in entities)
            {
                list.Add(Convert(conversionSystemFrom, conversionSystemTo, e, setUMLocalizationKey));
            }
            
            return list;
        }

        /// <summary>
        /// Converte i valori decimali dell'entità in base al sistema di conversione
        /// </summary>
        /// <param name="conversionSystem"></param>
        /// <param name="entity"></param>
        /// <param name="doConversion">Flag che forza la conversione nel sistema di misurazione specificato</param>
        /// <param name="setUMKey">Flag che forza il settaggio della codifica di localizzazione per l'unità di misura</param>
        /// <returns></returns>
        public static T Convert(MeasurementSystemEnum conversionSystemFrom
                            , MeasurementSystemEnum conversionSystemTo
                            , T entity
                            , bool setUMKey = true
                            , bool applyRound = true)
        {
            var props = typeof(T).GetProperties();
            T result = new T();
            int decimalPrecision = 0;

            foreach (var prop in props)
            {
                var value = prop.GetValue(entity);

                if (value != null && value.GetType() == typeof(decimal))
                {

                    if (decimal.TryParse(value.ToString(), out decimal decimalValue))
                    {
                        var convertedItem = ConvertToHelper.Convert(conversionSystemFrom, conversionSystemTo, entity.ItemDataFormat
                                    , decimalValue);

                        if (decimalPrecision == 0)
                            decimalPrecision = convertedItem.DecimalPrecision;
                        //Applica la conversione
                        value = convertedItem.Value;

                        //effettua l'arrotondamento se richiesto
                        if (applyRound)
                            value = decimal.Round(convertedItem.Value, decimalPrecision);
                    }
                }
                prop.SetValue(result, value);
            }

            if (setUMKey)
                result.UMLocalizationKey = $"LBL_{result.ItemDataFormat.ToString().ToUpper()}_{conversionSystemTo.ToString().ToUpper()}";

            result.DecimalPrecision = decimalPrecision;
            return result;
        }

    }
}
