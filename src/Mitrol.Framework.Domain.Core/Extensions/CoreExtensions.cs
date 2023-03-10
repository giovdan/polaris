namespace Mitrol.Framework.Domain.Core.Extensions
{
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Cors.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;


    public static class CoreExtensions
    {
        public const string CORS_POLICIES = "MitrolCorsPolicies";


        /// <summary>
        /// Calculate Tool HashCode
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        public static string CalculateHash<T>(IEnumerable<T> values)
        {
            string hashCode = string.Empty;
            foreach (var value in values)
            {
                if (decimal.TryParse(value.ToString(), out decimal decimalValue))
                {
                    hashCode += decimalValue.ToHex();
                }
                else
                    hashCode += value.ToString().ToHex();
            }

            return hashCode;
        }

        /// <summary>
        /// Calcola la percentuale di number nei confronti di masterNumber
        /// </summary>
        /// <param name="life"></param>
        /// <param name="masterNumber"></param>
        /// <returns></returns>
        public static (int? Percentage, StatusColorEnum StatusColor) CalculatePercentage(decimal life
                                                                            , decimal warningThreshold
                                                                            , decimal maxLife)
        {
            // Se non è gestita la vita massima: vita infinita, blue
            if (maxLife == 0)
                return (null, StatusColorEnum.Blue);

            // Calcolo del valore %
            var percentage = decimal.ToInt32(100 - (life / maxLife * 100));

            // Se la vita è maggiore della vita massima il colore dello stato è di errore (ROSSO)
            // Se la vita supera la soglia di warning il colore dello stato è GIALLO
            // Altrimenti è verde
            return life >= maxLife ?
                        (percentage, StatusColorEnum.Red) :
                        life >= warningThreshold ?
                                (percentage, StatusColorEnum.Orange)
                                : (percentage, StatusColorEnum.Green);
        }

        /// <summary>
        /// Get Config Files Directory
        /// </summary>
        /// <returns></returns>
        public static DirectoryInfo GetConfigDirectory()
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var solutionLocation = DomainExtensions.GetStartUpDirectoryInfo(currentPath: assemblyLocation).FullName;
            return !string.IsNullOrEmpty(solutionLocation) ? new DirectoryInfo($"{solutionLocation}\\bin\\config") : null;
        }

        /// <summary>
        /// Check if a list is contained in another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool ContainsAllItems<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return !second.Except(first).Any();
        }

        public static string ToErrorString<T>(this ImportResult<T> importResult)
        {
            return ToErrorString(importResult.ErrorDetails);
        }

        [Obsolete]
        public static string ToErrorString(this IEnumerable<ErrorDetail> errorDetails)
        {
            StringBuilder sb = new StringBuilder();

            errorDetails.ForEach(e =>
            {
                sb.AppendLine(e.ToString());
            });

            return sb.ToString().Replace(System.Environment.NewLine, string.Empty); ;
        }

        /// <summary>
        /// Converts a ValidationRule to a response body.
        /// </summary>
        public static Dictionary<string, string> ToErrorResponseBody(this IEnumerable<ErrorDetail> errorDetails)
        {
            return errorDetails?
                .ToDictionary(keySelector: errorDetail => errorDetail.Field.IsNullOrEmpty() ? "error" : errorDetail.Field
                , elementSelector: errorDetail => errorDetail.ErrorCodes.First())
                ?? null;
        }

        /// <summary>
        /// Converts an Exception to a response body.
        /// </summary>
        public static Dictionary<string, string> ToErrorResponseBody(this Exception exception, Exception additionalException = null)
        {
            var errorMessage = exception.InnerException?.Message ?? exception.Message;

            if (additionalException != null)
            {
                errorMessage = string.Join(Environment.NewLine,
                       additionalException.InnerException?.Message ?? additionalException.Message,
                       errorMessage);
            }

            return new Dictionary<string, string>
            {
                { "error", errorMessage }
            };
        }

        public static double? GetDouble(string value)
        {
            double? result = null;

            if (double.TryParse(value, out double resultValue))
                result = resultValue;

            return result;
        }

        /// <summary>
        /// Check if Culture Code exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsValidCulture(string code)
        {
            CultureInfo[] availableCultures =
                CultureInfo.GetCultures(CultureTypes.AllCultures);

            foreach (CultureInfo culture in availableCultures)
            {
                if (culture.Name.Equals(code))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Generate new GUID
        /// </summary>
        /// <returns></returns>
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }


        /// <summary>
        /// Convert ValidationRule to ErrorDetails List
        /// </summary>
        /// <param name="validationRule"></param>
        /// <returns></returns>
        [Obsolete]
        public static List<ErrorDetail> ToErrors(this ValidationResult validationRule)
        {
            var errorList = new List<ErrorDetail>();
            if (validationRule.Errors.Any())
            {
                var keys = validationRule.Errors.Select(x => x.PropertyName).Distinct();
                foreach (var key in keys)
                {
                    errorList.Add(new ErrorDetail(key, validationRule.Errors.Where(x => x.PropertyName == key).Select(e => e.ErrorCode).ToList()));
                }
            }

            return errorList;
        }

        public static Dictionary<string, string> ToErrorResponseBody(this Result result)
            => result.Errors.ToErrorResponseBody();

        /// <summary>
        /// Convert ValidationRule to response body.
        /// </summary>
        public static Dictionary<string, string> ToErrorResponseBody(this ValidationResult validationRule)
        {
            var errors = new Dictionary<string, string>();
            if (validationRule.Errors.Any())
            {
                var keys = validationRule.Errors.Select(x => x.PropertyName).Distinct();
                foreach (var key in keys)
                {
                    errors.Add(key, validationRule.Errors.FirstOrDefault(x => x.PropertyName == key).ErrorCode);
                }
            }
            return errors;
        }

        public static string ToErrorString(this ValidationResult validationRule)
        {
            string result = string.Empty;
            if (validationRule.Errors.Any())
            {
                StringBuilder sb = new StringBuilder();
                validationRule.Errors.ForEach(e =>
                {
                    sb.AppendLine(e.ErrorMessage);
                });
                result = sb.ToString();
            }

            return result;
        }

        public static T GetAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttribute(typeof(T)) as T;
        }



        public static T GetOperationTypeEnumAttribute<T>(this OperationTypeEnum operationType)
        {
            T attribute = DomainExtensions.GetEnumAttribute<OperationTypeEnum, T>(operationType);
            if (typeof(T)
                == typeof(OperationInfoAttribute))
            {
                (attribute as OperationInfoAttribute).OperationType = operationType;
            }
            return attribute;
        }


        /// <summary>
        /// Recupera il valore dell'enumerato dal displayname
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="displayName"></param>
        /// <returns>Il valore dell'enumerato se il displayname specificato è corretto. Altrimenti lancia l'eccezione ArgumentOutOfRangeException</returns>
        public static TEnum GetValueFromDisplayName<TEnum>(string displayName)
        {
            var type = typeof(TEnum);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                //var attribute = Attribute.GetCustomAttribute(field,
                //    typeof(DatabaseDisplayNameAttribute)) as DatabaseDisplayNameAttribute;
                //if (attribute != null)
                //{

                //    if (attribute.DisplayName == displayName)
                //    {
                //        return (TEnum)field.GetValue(null);
                //    }
                //}
                //else
                //{
                //    if (field.Name == displayName)
                //        return (TEnum)field.GetValue(null);
                //}
                return (TEnum)field.GetValue(null);
            }

            throw new KeyNotFoundException($"{ErrorCodesEnum.ERR_GEN011} displayName:{displayName}");
        }

        //Restituisce il EnumSerializationName relativo al valore dell'enumerativo, in intero, che gli viene passato
         public static string GetEnumSerializationNameFromIntEnumValue(string typeName, int EnumValue)
        {
            if (typeName == null)
                return null;
            Type t = Type.GetType(typeName);
            var typeConverter = TypeDescriptor.GetConverter(t);
            //trovo l'enumerativo corrisponde a EnumValue
            var enumerative = typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, EnumValue);
            //converto l'enumerativo in stringa, come EnumSerializationName
            var serializationName = typeConverter.ConvertTo(null, CultureInfo.InvariantCulture, enumerative, typeof(string));
            if (serializationName == null) //casi in cui il valore passato non rispetta l'enumerativo (ad es perchè inserito in DB senza rispettare gli Enum)
            {
                var DefaultValue = DomainExtensions.GetDefaultEnumValueFromTypename(typeName);
                enumerative = typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, DefaultValue);
                //converto l'enumerativo in stringa, come EnumSerializationName
                serializationName = typeConverter.ConvertTo(null, CultureInfo.InvariantCulture, enumerative, typeof(string));
            }
            return serializationName.ToString();
        }

        //
        /// <summary>
        /// Rimuove gli zeri non significativi
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal Normalize(this decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException("description");
        }

        public static string ToFullDateString(this DateTime date)
        {
            return $"{date.ToShortDateString()} {date.ToShortTimeString()}";
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            source.Select(item =>
            {
                action(item);
                return item;
            }).ToList();
        }

        public static void ForEach<T>(this IQueryable<T> source, Action<T> action)
        {
            source.AsEnumerable().ForEach(action);
        }

        /// <summary>
        /// Get root directory for current Path. Assume that ".init" file is present in root directory
        /// </summary>
        /// <param name="currentPath"></param>
        /// <returns></returns>
        public static DirectoryInfo TryGetStartUpDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.init").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }

        /// <summary>
        /// Check if a class has an attribute of type attribType
        /// </summary>
        /// <param name="clsType"></param>
        /// <param name="attribType"></param>
        /// <returns></returns>
        public static object HasClassAttribute(Type clsType, Type attribType)
        {
            if (clsType == null)
                throw new ArgumentNullException("clsType");

            return clsType.GetCustomAttributes(attribType, false).FirstOrDefault();
        }

        /// <summary>
        /// Json Serialization of an Object into a stream
        /// </summary>
        /// <param name="value">Object to serailize</param>
        /// <param name="stream">Stream for serialization</param>
        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        /// <summary>
        /// Get Value from FNC File for FNC Import File
        /// </summary>
        /// <param name="fncItem">Row of FNC File</param>
        /// <param name="code">Code to Find</param>
        /// <returns></returns>
        public static string GetFNCValue(string fncItem, string code)
        {
            string matchPattern = @"(?<displayName>[A-Z_]{1,7})\:(?<value>[\w\\.\/\w]+)|(?<displayName>[A-Z_]{1,7})(?<value>[-\d.]+)";
            MatchCollection collection = Regex.Matches(fncItem, matchPattern);
            var match = collection.FirstOrDefault(x => x.Groups["displayName"].Value == code);
            if (match != null)
            {
                return match.Groups["value"].Value;
            }
            return string.Empty;
        }

        public static string ParseFromDisplayName<TEnum>(string displayName)
        {
            string result = string.Empty;
            var values = Enum.GetValues(typeof(TEnum));
            //foreach (TEnum value in values)
            //{
            //    try
            //    {
            //        string itemToCheck = DomainExtensions.GetEnumAttribute<TEnum, DatabaseDisplayNameAttribute>(value).DisplayName;
            //        if (itemToCheck.ToUpper() == displayName.ToUpper())
            //        {
            //            result = value.ToString();
            //            break;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            return result;
        }

        public static void SetEntity<T>(this DbContext dbContext, T entity, EntityState state) where T : BaseEntity
        {
            var local = dbContext.Set<T>()
                                     .Local
                                     .SingleOrDefault(f => f.Id == entity.Id);
            if (local != null)
            {
                dbContext.Entry(local).State = EntityState.Detached;
            }

            dbContext.Entry(entity).State = state;
        }

        /// <summary>
        /// Set Cors Policy on service collection
        /// </summary>
        /// <param name="services"></param>
        public static void SetSiteCorsPolicy(IServiceCollection services)
        {
            CorsPolicyBuilder corsBuilder = new CorsPolicyBuilder();
            corsBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials() // allow credentials
            ; 

            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICIES, corsBuilder.Build());
            });
        }

        public static bool IsInteger(this decimal value)
        {
            return value % 1 == 0;
        }

        public static string ToSQLiteDateTime(this DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year,
                                 datetime.Month, datetime.Day,
                                 datetime.Hour, datetime.Minute,
                                  datetime.Second, datetime.Millisecond);
        }

        /// <summary>
        /// Estenzione Between per byte
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <param name="isInclusive"></param>
        /// <returns></returns>
        public static bool Between<T>(this T value, T lower, T upper
                , bool isInclusive = true) where T : IComparable<T>
        {
            return isInclusive ?
                    value.CompareTo(lower) >= 0 && value.CompareTo(upper) <= 0
                    : value.CompareTo(lower) > 0 && value.CompareTo(upper) < 0;
        }

        
        

        public static int WeekOfYear(this DateTime date, string culture)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(culture);

            return cultureInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek
                        , cultureInfo.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Get First Day of Week based on Year and Culture
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear, CultureInfo culture)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            int firstWeek = culture.Calendar.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek
                , firstDayOfWeek);
            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);



            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            var off = DayOfWeek.Thursday - firstDayOfWeek;
            return result.AddDays(-off);
        }

        public static T SetManagedFlag<T>(this T entity, bool isManaged)
            where T : IHasIsManaged
        {
            entity.IsManaged = isManaged;
            return entity;
        }

        /// <summary>
        /// Convert byte array to string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ConvertToString(this byte[] source)
        {
            return source != null ? System.Text.Encoding.Unicode.GetString(source) : null;
        }

        /// <summary>
        /// Deep clone
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T source, ReferenceLoopHandling referenceLoopHandling = ReferenceLoopHandling.Serialize)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            var deserializeSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
                ,
                ContractResolver = new IgnoreJsonAttributesResolver()
            };
            var serializeSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = referenceLoopHandling
                ,
                ContractResolver = new IgnoreJsonAttributesResolver()
            };

            try
            {
                var serializedObject = JsonConvert.SerializeObject(source, serializeSettings);
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source, serializeSettings), deserializeSettings);
            }
            catch (Exception ex)
            {

                throw;
            }

            
        }

        /// <summary>
        /// Convert from Epoch To DateTime
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static string ToUniversalTimeString(this DateTime date)
        {
            return date.ToString("s") + "Z";
        }

        /// <summary>
        /// Convert DateTime to Epoch
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }

        ///// <summary>
        ///// Analisi di due valori in base alla tolleranza
        ///// </summary>
        ///// <param name="teo"></param>
        ///// <param name="ril"></param>
        ///// <returns></returns>
        //public static bool IsInTolerance(this ToleranceConfiguration tolerance, float Teo, float Ril)
        //{
        //    if(tolerance.LowerValue.HasValue && tolerance.UpperValue.HasValue)
        //    {
        //        // Analisi con tolleranza assoluta
        //        if (tolerance.Type == ToleranceTypeEnum.ABS)
        //        {
        //            // Se rilevato è minore del teorico meno il valore inferiore
        //            // o  rilevato è maggiore del teorico più il valore superiore
        //            // I valori sono fuori range e torna false; altrimenti true
        //            return (Ril < (Teo - tolerance.LowerValue)) || (Ril > (Teo + tolerance.UpperValue)) is false;
        //        }
        //        // Analisi con tolleranza percentuale
        //        else if (tolerance.Type == ToleranceTypeEnum.PERC)
        //        {
        //            // Se rilevato è minore del teorico meno il valore percentuale inferiore
        //            // o  rilevato è maggiore del teorico più il valore percentuale superiore
        //            // I valori sono fuori range e torna false; altrimenti true
        //            return (Ril < (Teo - ((Teo / 100.0F) * tolerance.LowerValue))) ||
        //                (Ril > (Teo + ((Teo / 100.0F) * tolerance.UpperValue))) is false;
        //        }
        //    }

        //    // Analisi senza tolleranza se valori di range o tipo di tolleranza non specificati
        //    return (DomainExtensions.CompareFloatWithInchTolerance(Teo, Ril));
        //}
    }
}
