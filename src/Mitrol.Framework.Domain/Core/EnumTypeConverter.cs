namespace Mitrol.Framework.Domain.Core
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumCustomNameAttribute : DescriptionAttribute
    {
        public EnumCustomNameAttribute(string text) : base(text) { }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumSerializationNameAttribute : EnumCustomNameAttribute
    {
        public EnumSerializationNameAttribute(string text) : base(text) { }
    }

    public class EnumCustomNameTypeConverter : EnumConverter
    {
        public EnumCustomNameTypeConverter(Type type) : base(type)
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || TypeDescriptor.GetConverter(typeof(Enum)).CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {

            if (value is decimal decimalValue)
            {
                return Enum.ToObject(EnumType, decimal.ToInt32(decimalValue));
            }
            else if (value is int integer)
            {
                return Enum.ToObject(EnumType, integer);
            }
            else if (value is long @long)
            {
                return Enum.ToObject(EnumType, @long);
            }
            else if (value is double @double)
            {
                return Enum.ToObject(EnumType, (int)@double);
            }
            // Per convertire da una stringa verso il tipo enumerato
            // interrogo gli eventuali CsvEnumDescriptionAttribute
            else if (value is string text)
            {
                return GetEnumValue(EnumType, text);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var member = EnumType.GetMember(value.ToString()).FirstOrDefault();
                if (member != null)
                {
                    var serializationAttribute = ((EnumSerializationNameAttribute[])member.GetCustomAttributes(typeof(EnumSerializationNameAttribute), inherit: false)).SingleOrDefault();
                    return serializationAttribute.Description;
                }
                return null;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        //Fornisce l'Enumerativo corrispondente, in base alla stringa che può essere
        //o il nome del displayName oppure il valore Description di un EnumCustomNameAttribute
        public static object GetEnumValue(Type enumType, string value)
        {
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var @decimal))
                return Enum.ToObject(enumType, decimal.ToInt32(@decimal));

            var fields = enumType.GetFields();

            foreach (var fieldInfo in fields)
            {
                //var databaseDisplayNameAttributes = (DatabaseDisplayNameAttribute[])fieldInfo.GetCustomAttributes(typeof(DatabaseDisplayNameAttribute), false);
                //if (databaseDisplayNameAttributes.Length > 0 && databaseDisplayNameAttributes.Any(a => a.DisplayName == value))
                //    return fieldInfo.GetValue(fieldInfo.Name);

                if (fieldInfo.Name == value)
                    return fieldInfo.GetValue(fieldInfo.Name);

                var enumCustomNameAttributes = (EnumCustomNameAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumCustomNameAttribute), inherit: false);
                if (enumCustomNameAttributes.Length > 0 && enumCustomNameAttributes.Any(a => a.Description == value))
                    return fieldInfo.GetValue(fieldInfo.Name);
            }

            return value;
        }
    }
}
