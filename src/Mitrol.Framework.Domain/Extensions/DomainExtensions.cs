namespace Mitrol.Framework.Domain
{
    using Microsoft.Extensions.Configuration;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Configuration.Models;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Macro;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class DomainExtensions
    {
        #region < Costants >

        public const string GENERIC_LABEL = "LBL";

        /// <summary>
        /// Fattore di conversione da gradi a radianti
        /// </summary>
        public const double FattoreConverGradiARadianti = Math.PI / 180.0f;

        #endregion < Costants >

        // Associazione oggetto cartella estensione del file da aggiungere dopo gli identificatori
        public static Dictionary<ImportExportObjectEnum, string> FileConfigurationForImportExport = new Dictionary<ImportExportObjectEnum, string>
            {
                { ImportExportObjectEnum.AccountObject, "Account" },
                { ImportExportObjectEnum.UserGroupObject, "Group" },
                { ImportExportObjectEnum.MaintenanceObject, "Maintenance" },
                { ImportExportObjectEnum.MaterialObject, "Material" },
                { ImportExportObjectEnum.PieceObject, "Piece" },
                { ImportExportObjectEnum.ProfileObject, "Profile" },
                { ImportExportObjectEnum.ProgramObject, "Program" },
                { ImportExportObjectEnum.StockObject, "Stock" },
                { ImportExportObjectEnum.ToolHolderObject, "ToolHolder" },
                { ImportExportObjectEnum.ToolObject, "Tool" },
                { ImportExportObjectEnum.ToolTableObject, "ToolTable" }
            };

        public static bool IsNaN(this float value) => float.IsNaN(value);
        public static bool IsNotNaN(this float value) => !value.IsNaN();

        public static string ResolvePathPropertyValue(string absoluteOrRelativePath, string basePath)
            => basePath is null || absoluteOrRelativePath.IsFullPath() ? absoluteOrRelativePath : Path.Combine(basePath, absoluteOrRelativePath);

        public static T JsonClone<T>(this T item) where T : class
        {
            var serObj = JsonConvert.SerializeObject(item);
            return JsonConvert.DeserializeObject<T>(serObj);
        }

        //public static TSource MergeWith<TSource>(this TSource source, TSource fragment)
        //{
        //    if (fragment == null && source == null)
        //    {
        //        return default;
        //    }
        //    else if (fragment == null)
        //    {
        //        return source;
        //    }
        //    else if (source == null)
        //    {
        //        return fragment;
        //    }

        //    foreach (var propertyInfo in source.GetType().GetProperties())
        //    {
        //        var sourceValue = propertyInfo.GetGetMethod().Invoke(source, null);
        //        var fragmentValue = propertyInfo.GetGetMethod().Invoke(fragment, null);

        //        if (propertyInfo.CanWrite)
        //        {
        //            if (propertyInfo.PropertyType != typeof(string))
        //            {
        //                if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
        //                {
        //                    if (fragmentValue == null)
        //                    {
        //                        fragmentValue = sourceValue;
        //                    }
        //                    else if (sourceValue != null)
        //                    {
        //                        var fragmentArray = (fragmentValue as IEnumerable)?.Cast<object>();
        //                        var sourceArray = (sourceValue as IEnumerable)?.Cast<object>();
        //                        fragmentValue = sourceArray.Concat(fragmentArray);
        //                    }
        //                }
        //                else if (propertyInfo.PropertyType.IsValueType is false)
        //                {
        //                    fragmentValue = sourceValue.MergeWith(fragmentValue);
        //                }
        //            }
        //            propertyInfo.GetSetMethod(nonPublic: true)
        //                .Invoke(source, new object[] { fragmentValue });
        //        }
        //    }
        //    return source;
        //}

        #region < bitwise operations >

        /// <summary>
        /// Per ogni bit contenuto in source, restituisce un bool.
        /// </summary>
        public static bool[] BitsToBools(this uint[] source)
        {
            var bits = new BitArray(source.Cast<int>().ToArray());
            var result = bits.Cast<bool>().ToArray();
            return result.ToArray();
        }

        /// <summary>
        /// Per ogni bit contenuto in source, restituisce un bool.
        /// </summary>
        public static bool[] BitsToBools(this byte[] source)
        {
            var bits = new BitArray(source);
            var result = bits.Cast<bool>().ToArray();
            return result.ToArray();
        }

        #endregion < bitwise operations >

        #region < System.Enum >
        /// <summary>
        /// Gt Custom T Attribute  from TEnum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>Custom Attribute if exists</returns>
        public static T GetEnumAttribute<TEnum, T>(TEnum item)
        {
            return GetEnumAttributes<TEnum, T>(item).FirstOrDefault();
        }

        /// <summary>
        /// Gt Custom T Attribute  from TEnum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>Custom Attribute if exists</returns>
        public static List<T> GetEnumAttributes<TEnum, T>(TEnum item)
        {
            Type type = item.GetType();

            return type.GetField(item.ToString())?.GetCustomAttributes(typeof(T), false)?.Cast<T>().ToList() ?? new List<T>();
        }

        /// <summary>
        /// Get Custom T Attribute from TEnum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>Custom Attribute if exists</returns>
        public static T GetEnumAttribute<T>(this Enum item)
            => GetEnumAttributes<T>(item).FirstOrDefault();

        /// <summary>
        /// Get Custom T Attribute from TEnum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>Custom Attribute if exists</returns>
        public static List<T> GetEnumAttributes<T>(this Enum item)
        {
            try
            {
                return item.GetType()
                    .GetField(item.ToString()).GetCustomAttributes(typeof(T), false)
                    .Cast<T>().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public static T Parse<T>(this string stringEnum) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), stringEnum);
        }

        #endregion < System.Enum >

        #region < System.DateTime >

        public static long TotalMilliseconds(this DateTime dateTime) => ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();

        #endregion < System.DateTime >

        #region < IEnumerable<TSource> >

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
                    => source.DistinctBy(keySelector, EqualityComparer<TKey>.Default);

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<TKey> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source?.GroupBy(keySelector)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
        }

        public static TSource SingleWhenOnlyOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var results = source.Take(2).ToArray();
            return results.Count() == 1 ? results.Single() : default;
        }

        public static TSource SingleWhenOnlyOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));

            var results = source.Where(predicate).Take(2);
            return results.Count() == 1 ? results.Single() : default;
        }

        public static IEnumerable<IEnumerable<TSource>> Chunk<TSource>(this IEnumerable<TSource> list, int size)
        {
            int i = 0;
            var chunks = from item in list
                         group item by i++ % size into part
                         select part.AsEnumerable();
            return chunks;
        }

        [DebuggerStepThrough]
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> destination,
                                                                   IEnumerable<KeyValuePair<TKey, TValue>> source)
            => Merge(destination, source, (acc, curr) => curr);

        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> destination,
                                                                   IEnumerable<KeyValuePair<TKey, TValue>> source,
                                                                   Func<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>> aggregateFunc)
        {
            if (destination is null && source is null) { return null; }

            IEnumerable<KeyValuePair<TKey, TValue>> result;
            if (destination is null) { result = source; }
            else if (source is null) { result = destination; }
            else
            {
                var merged = destination.Concat(source).GroupBy(item => item.Key).Where(group => group.Count() > 1)
                    .Select(group => group.Aggregate(aggregateFunc));

                result = merged.Union(destination.Union(source
                    , DictionaryComparer<TKey, TValue>.Default)
                    , DictionaryComparer<TKey, TValue>.Default);
            }

            return result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static FlagsDictionary<Configuration.GFunctionEnum> Merge(this IReadOnlyDictionary<Configuration.GFunctionEnum, bool> destination, IReadOnlyDictionary<Configuration.GFunctionEnum, bool> source)
        {
            var merged = Merge(destination.AsEnumerable(), source.AsEnumerable());
            return merged != null ? new FlagsDictionary<Configuration.GFunctionEnum>(merged) : null;
        }

        [DebuggerStepThrough]
        public static IEnumerable<TSource> Merge<TSource>(this IEnumerable<TSource> destination, IEnumerable<TSource> source)
            => Merge(destination, source, EqualityComparer<TSource>.Default);

        public static IEnumerable<TSource> Merge<TSource>(this IEnumerable<TSource> destination, IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (comparer is null) { throw new ArgumentNullException(nameof(comparer)); }

            if (destination is null && source is null) { return null; }

            IEnumerable<TSource> result;
            if (destination is null) { result = source; }
            else if (source is null) { result = destination; }
            else
            {
                result = destination.Union(source, comparer);
            }
            return result.ToArray();
        }

        #endregion < IEnumerable<TSource> >

        public static DirectoryInfo s_startUpDirectoryInfo;

        /// <summary>
        /// Get Application Root Directory
        /// </summary>
        public static DirectoryInfo GetStartUpDirectoryInfo(string currentPath = null, string fileFilter = "*.init")
        {
            if (s_startUpDirectoryInfo == null)
            {
                var directory = new DirectoryInfo(
                    currentPath ?? Directory.GetCurrentDirectory());
                while (directory != null && !directory.GetFiles(fileFilter).Any())
                {
                    directory = directory.Parent;
                }
                s_startUpDirectoryInfo = directory;
            }
            return s_startUpDirectoryInfo;
        }

        public static bool IsNumber(this string stringa)
        {
            return !string.IsNullOrEmpty(stringa) && !stringa.Any(char.IsLetter);
        }

        public static IEnumerable<BaseInfoItem<DateRangeFilterEnum, string>> GetDateRangeFilters()
        {
            var enumList = Enum.GetValues(typeof(DateRangeFilterEnum));

            var list = new List<BaseInfoItem<DateRangeFilterEnum, string>>();

            foreach (var @enum in enumList)
            {

                list.Add(new BaseInfoItem<DateRangeFilterEnum, string>((DateRangeFilterEnum)@enum
                                            , $"LBL_{@enum.ToString().ToUpper()}"));
            }

            return list;
        }

        public static string ToValidationErrorString(this List<ImportItemValidationResult> importItemValidations)
        {
            var builder = new StringBuilder();

            importItemValidations.ForEach(e =>
            {
                builder.AppendLine(e.ValidationError);
            });

            return builder.ToString();
        }


        public static string ToHex(this long number)
        {
            return ToHex(number.ToString());
        }

        public static string ToHex(this double number)
        {
            return ToHex(number.ToString());
        }

        public static string ToHex(this decimal number)
        {
            return ToHex(number.ToString());
        }

        public static string ToHex(this string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        internal static string GetStringSha256Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }


        public static T GetAttributeValue<T>(this Dictionary<DatabaseDisplayNameEnum, object> dictionary, DatabaseDisplayNameEnum displayName)
        {
            var destinationType = typeof(T);

            T result = default;

            if (dictionary.TryGetValue(displayName, out var value) && value != null)
            {
                // Handle string value type default
                if (value != null)
                {
                    if (value.GetType() == destinationType)
                    {
                        result = (T)value;
                    }
                    else if (!value.ToString().IsNullOrEmpty())
                    {
                        result = destinationType.IsEnum
                               ? (T)TypeDescriptor.GetConverter(destinationType).ConvertFrom(value)
                               : (T)Convert.ChangeType(value, destinationType);
                    }
                }
            }

            return result;
        }

        public static T GetAttributeValue<T>(this IEntityWithAttributeValues entityWithAttributes, DatabaseDisplayNameEnum displayName)
        {
            return entityWithAttributes.Attributes.GetAttributeValue<T>(displayName);
        }

        public static T GetAttributeValue<T>(this IEntityWithAttributeValues<ExternalInterfaceNameEnum> entityWithAttributes, ExternalInterfaceNameEnum displayName)
        {
            return GetAttributeValue<T, ExternalInterfaceNameEnum>(entityWithAttributes, displayName);
        }

        public static Result<T> TryGetAttributeValue<T>(this IEntityWithAttributeValues<ExternalInterfaceNameEnum> entityWithAttributes, ExternalInterfaceNameEnum displayName)
        {
            if (entityWithAttributes.Attributes.TryGetValue(displayName, out var value) && value != null)
            {
                return Result.Ok<T>(GetAttributeValue<T, ExternalInterfaceNameEnum>(entityWithAttributes, displayName));
            }
            else
                return Result.Fail<T>(ErrorCodesEnum.ERR_GEN002.ToString());
        }

        public static T GetAttributeValue<T, TEnum>(this IEntityWithAttributeValues<TEnum> entityWithAttributes, TEnum displayName)
        {
            var destinationType = typeof(T);

            T result = default;
            //se è un enumerativo cerco il suo valore di default nel caso sia stato scelto un valore diverso da 0, altrimenti il valore di default è sempre 0..
            if (destinationType.IsEnum)
            {
                //cerco l'attributo che fornisce il valore di default
                DefaultValueAttribute[] attributes = (DefaultValueAttribute[])destinationType.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                if (attributes != null &&
                    attributes.Length > 0)
                {
                    var typeConverter = TypeDescriptor.GetConverter(destinationType);
                    result = (T)typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, attributes[0].Value);
                }
            }

            if (entityWithAttributes.Attributes.TryGetValue(displayName, out var value) && value != null)
            {
                if (value.GetType() == destinationType)
                {
                    result = (T)value;
                }
                else
                {

                    result = destinationType.IsEnum
                            ? (T)TypeDescriptor.GetConverter(destinationType).ConvertFrom(value)
                            : (T)Convert.ChangeType(value, destinationType);
                }

            }
            return result;
        }

        public static T GetAttributeValue<T>(this IEntityWithAttributeValuesAndIdentifiers entityWithAttributeValuesAndIdentifiers, DatabaseDisplayNameEnum displayName)
        {
            var destinationType = typeof(T);
            T result = default;

            if (entityWithAttributeValuesAndIdentifiers.Identifiers.TryGetValue(displayName, out var value)
                || entityWithAttributeValuesAndIdentifiers.Attributes.TryGetValue(displayName, out value))
            {
                result = destinationType.IsEnum
                        ? (T)TypeDescriptor.GetConverter(destinationType).ConvertFrom(value)
                        : (T)Convert.ChangeType(value, destinationType);
            }

            return result;
        }

        public static T FindProperty<T>(this Dictionary<string, List<string>> matches, string property)
        {
            //nel caso di nullable type il valore di default è null
            T res = default;
            var values = matches.ContainsKey(property) ? matches[property] : null;
            if ((values != null) && (values.Count > 0))
            {
                if ((values.Count == 1) && (values[0] != string.Empty))
                {
                    if (Nullable.GetUnderlyingType(typeof(T)) != null)
                    {
                        if (typeof(T) == typeof(Int32?))
                            return (T)Convert.ChangeType(Convert.ToInt32(values[0]), Nullable.GetUnderlyingType(typeof(T)));
                        else if (typeof(T) == typeof(UInt16?))
                            return (T)Convert.ChangeType(Convert.ToUInt16(values[0]), Nullable.GetUnderlyingType(typeof(T)));
                        else if (typeof(T) == typeof(float?))
                            return (T)Convert.ChangeType(Convert.ToDouble(values[0]), Nullable.GetUnderlyingType(typeof(T)));
                    }
                    else
                    {

                        if (typeof(T) == typeof(string))
                            return (T)Convert.ChangeType(values[0], typeof(T));
                        else if (typeof(T) == typeof(UInt16))
                            return (T)Convert.ChangeType(Convert.ToUInt16(values[0]), typeof(T));
                        else if (typeof(T) == typeof(Int32))
                            return (T)Convert.ChangeType(Convert.ToInt32(values[0]), typeof(T));
                        else if (typeof(T) == typeof(float))
                            return (T)Convert.ChangeType(Convert.ToDouble(values[0]), typeof(T));
                    }
                }
            }

            return res;
        }

        public static List<T> FindProperty<T>(this Dictionary<string, List<string>> matches, string property, int Count)
        {
            T res = default;
            var values = matches.ContainsKey(property) ? matches[property] : null;
            if ((values != null) && (values.Count > 0))
            {
                var valuesToRet = new List<T>(Count);
                foreach (var val in values)
                {
                    if (val != string.Empty)
                    {
                        if (Nullable.GetUnderlyingType(typeof(T)) != null)
                        {
                            if (typeof(T) == typeof(Int32?))
                                valuesToRet.Add((T)Convert.ChangeType(Convert.ToInt32(val), Nullable.GetUnderlyingType(typeof(T))));
                            else if (typeof(T) == typeof(UInt16?))
                                valuesToRet.Add((T)Convert.ChangeType(Convert.ToUInt16(val), Nullable.GetUnderlyingType(typeof(T))));
                            else if (typeof(T) == typeof(float?))
                                valuesToRet.Add((T)Convert.ChangeType(Convert.ToDouble(val), Nullable.GetUnderlyingType(typeof(T))));
                        }
                        else
                        {
                            if (typeof(T) == typeof(string))
                                valuesToRet.Add((T)Convert.ChangeType(val, typeof(T)));
                            else if (typeof(T) == typeof(UInt16))
                                valuesToRet.Add((T)Convert.ChangeType(Convert.ToUInt16(val), typeof(T)));
                            else if (typeof(T) == typeof(Int32))
                                valuesToRet.Add((T)Convert.ChangeType(Convert.ToInt32(val), typeof(T)));
                            else if (typeof(T) == typeof(float))
                                valuesToRet.Add((T)Convert.ChangeType(Convert.ToDouble(val), typeof(T)));
                        }
                    }
                }
                return valuesToRet;
            }
            var v = new List<T>(Count) { res };
            return v;
        }

        public static decimal? ToNullableDecimal(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(@this);
        }
        /// <summary>
        /// Calcola il coseno di un angolo in gradi
        /// </summary>
        /// <param name="degrees">angolo in gradi</param>
        /// <returns></returns>
        public static float CosD(double degrees)
        {
            return (float)Math.Cos(degrees * FattoreConverGradiARadianti);
        }

        /// <summary>
        /// Calcola il seno di un angolo in gradi
        /// </summary>
        /// <param name="degrees">angolo in gradi</param>
        /// <returns></returns>
        public static float SinD(double degrees)
        {
            return (float)Math.Sin(degrees * FattoreConverGradiARadianti);
        }

        /// <summary>
        /// ArcSin di un numero: l'angolo restituito è in gradi
        /// </summary>
        /// <param name="sinValue">valore del sin</param>
        /// <returns></returns>
        public static float ASinD(double sinValue)
        {
            return (float)(Math.Asin(sinValue) / FattoreConverGradiARadianti);
        }

        /// <summary>
        /// Calcola l'arcotangente
        /// </summary>
        /// <param name="tang"></param>
        /// <returns>un angolo in gradi</returns>
        public static float AtanD(double tang)
        {
            var rad = Math.Atan(tang);
            return (float)(rad / FattoreConverGradiARadianti);
        }

        /// <summary>
        /// Calcola la coordinata x di un sistema Oxy ruotato di un angolo alfa opportuno rispetto al sistema Ox'y'.
        /// Il valore calcolato e': x = rotx (x', y', alfa) = x'*cos(alfa) - y'*sin(alfa)
        /// </summary>

        /// <returns>un angolo in gradi</returns>
        public static float RotX(double x, double y, double alfa)
        {
            return (float)(x * CosD(alfa) - y * SinD(alfa));
        }

        /// <summary>
        /// Calcola la coordinata y di un sistema Oxy ruotato di un angolo alfa opportuno rispetto al sistema Ox'y'.
        /// Il valore calcolato e': y = roty (x', y', alfa) = x'*sin(alfa) + y'*cos(alfa)
        /// </summary>
        public static float RotY(double x, double y, double alfa)
        {
            return (float)(x * SinD(alfa) + y * CosD(alfa));
        }

        /// <summary>
        /// Calcola la tangente
        /// </summary>
        /// <param name="tang"></param>
        /// <returns>un angolo in gradi</returns>
        public static decimal TanD(decimal degrees)
        {
            return (decimal)Math.Tan((double)(degrees) * FattoreConverGradiARadianti);
        }

        /// <summary>
        /// Fornisce la lista degli attribute dipendenti dall'ExternalInterfaceNameEnum dato
        /// </summary>
        /// <param name="attributeToFindDependencies"> Enumerativo rappresentante l'attributo di cui si vuole conoscere le dipendenze</param>
        /// <param name="typeMacro"> Enumeratico corrispondente al tipo di macro, </param>
        /// <returns></returns>
        public static Dictionary<ExternalInterfaceNameEnum, DatabaseDisplayNameEnum>
            GetDependenciesAttributesOfExternalInterfaceEnum(ExternalInterfaceNameEnum attributeToFindDependencies, MacroTypeEnum typeMacro)
        {
            Type type = typeof(ExternalInterfaceNameEnum);
            List<ExternalInterfaceNameEnum> EnumLists = Enum.GetValues(typeof(ExternalInterfaceNameEnum)).Cast<ExternalInterfaceNameEnum>().ToList();
            Dictionary<ExternalInterfaceNameEnum, DatabaseDisplayNameEnum> lists = new Dictionary<ExternalInterfaceNameEnum, DatabaseDisplayNameEnum>();

            foreach (var enumValue in EnumLists)
            {
                var memberInfos = type.GetMember(enumValue.ToString());
                var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == type);
                var valueAttributes =
                      enumValueMemberInfo.GetCustomAttributes(typeof(MacroContextAttribute), false).Cast<MacroContextAttribute>().ToList()
                      .Where(customAttribute => (customAttribute.MacroTypeEnum.HasFlag(typeMacro)) && (customAttribute.MacroContextDataEnum == attributeToFindDependencies) && (customAttribute.DatabaseDisplayName != 0))
                      .ToDictionary(a => enumValue, a => a.DatabaseDisplayName);
                lists = lists.Concat(valueAttributes).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
            return lists;

        }

        /// <summary>
        /// Verifica se esiste una relazione tra l'attributo da aggiungere e il contesto(ExternalInterfaceNameEnum) per un determinata tipologia di Macro
        /// </summary>
        /// <param name="context">Enumerativo ExternalInterfaceNameEnum relativo al contesto</param>
        /// <param name="typeMacro">Enumerativo del tipo di Macro</param>
        /// <param name="attributeToAdd">Enumerativo ExternalInterfaceNameEnum di cui si vuole verificare la relazione</param>
        /// <returns></returns>
        public static bool ExistExternalAttributesInThisContext(ExternalInterfaceNameEnum context, MacroTypeEnum typeMacro, ExternalInterfaceNameEnum attributeToAdd)
        {
            Type type = typeof(ExternalInterfaceNameEnum);

            var memberInfos = type.GetMember(attributeToAdd.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == type);

            var attributes = enumValueMemberInfo.GetCustomAttributes(typeof(MacroContextAttribute), false).Cast<MacroContextAttribute>()
                     .Where(attribute => (attribute.MacroTypeEnum.HasFlag(typeMacro)) && (attribute.MacroContextDataEnum == context)).ToList();
            return attributes.Count > 0 ? true : false;

        }
        public static Dictionary<ExternalInterfaceNameEnum, object> ConvertToExternalInterfaceDictionary(this IEntityWithAttributeValues entityToConvert
                                                                                                        , ExternalInterfaceNameEnum attributeToFindDep
                                                                                                        , MacroTypeEnum macroType = MacroTypeEnum.MacroAll)
        {
            var depAttributes = GetDependenciesAttributesOfExternalInterfaceEnum(attributeToFindDep, macroType);
            return depAttributes.ToDictionary(a => a.Key, a => entityToConvert.GetAttributeValue<object>(a.Value));
        }

        public static OperationTypeEnum GetOperationTypeFromMacroType(MacroTypeEnum macroType)
        {
            switch (macroType)
            {
                case MacroTypeEnum.MacroCut:
                    return OperationTypeEnum.MCut;

                case MacroTypeEnum.MacroMill:
                    return OperationTypeEnum.Mill;

                case MacroTypeEnum.MacroRobot:
                    return OperationTypeEnum.Cope;

                default:
                    return OperationTypeEnum.Undefined;
            }
        }

        public static MacroTypeEnum GetMacroTypeFromOperationType(OperationTypeEnum operationType)
        {
            switch (operationType)
            {
                case OperationTypeEnum.MCut:
                    return MacroTypeEnum.MacroCut;

                case OperationTypeEnum.Mill:
                    return MacroTypeEnum.MacroMill;

                case OperationTypeEnum.Cope:
                    return MacroTypeEnum.MacroRobot;
            }
            return MacroTypeEnum.NotDefined;
        }

        public static void AddRange<T>(this ICollection<T> destination,
                               IEnumerable<T> source)
        {
            List<T> list = destination as List<T>;

            if (list != null)
            {
                list.AddRange(source);
            }
            else
            {
                foreach (T item in source)
                {
                    if (!destination.Contains(item))
                        destination.Add(item);
                }
            }
        }

        //ritorna il valore intero per poterlo inserire nel DB in base alla stringa dell'enumerativo passata
        public static Result<int> GetIntEnumValueFromString(string typeName, string enumerationName)
        {
            try
            {

                //Type dell'enumerativo
                Type t = Type.GetType(typeName);

                //get Converter
                var typeConverter = TypeDescriptor.GetConverter(t);
                //converto in un tipo enumerativo della stringa da importare
                var enumerativeValue = typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture
                        , enumerationName);
                //Ottengo il valore numerico corrispondente all'enumerativeValue
                return Result.Ok<int>((int)Convert.ChangeType(enumerativeValue, typeof(int)));
            }
            catch (Exception exc)
            {
                return Result.Fail<int>(exc.Message);
            }
        }

        //Fornisce il valore di default per il tipo indicato.
        public static int GetDefaultEnumValueFromTypename(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return 0;

            Type t = Type.GetType(typeName);
            //cerco l'attributo che fornisce il valore di default
            DefaultValueAttribute[] attributes = (DefaultValueAttribute[])t.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            if (attributes != null &&
                attributes.Length > 0)
            {
                var value = attributes[0].Value;
                return GetIntEnumValueFromString(typeName, value.ToString()).Value;
            }
            else
            {
                return 0;
            }
        }
        public static string GetImportExportExtension(ImportExportObjectEnum type, string fType = "")
        {
            // Se è vuoto assegno il valore di default dell'enumerativo
            if (string.Compare(fType, string.Empty) == 0)
            {
                var enumValue = (ImportFileTypeEnum)GetDefaultEnumValueFromTypename(typeof(ImportFileTypeEnum).AssemblyQualifiedName.ToString());
                fType = enumValue.GetEnumAttributes<ExtensionAttribute>().FirstOrDefault() == null ? "" : enumValue.GetEnumAttributes<ExtensionAttribute>().FirstOrDefault().FileExtension;
            }

            if (FileConfigurationForImportExport.TryGetValue(type, out string suffix))
            {
                // Aggiungo il suffisso dovuto al tipo do oggetto che sto importando 
                return $"{suffix}.{fType}";
            }
            return fType;
        }
        /// <summary>
        /// Compara due dati float rispettando la tolleranza mm/inch
        /// </summary>
        /// <param name="valo1"></param>
        /// <param name="valo2"></param>
        /// <param name="inch"></param>
        /// <returns></returns>
        public static bool CompareFloatWithInchTolerance(float valo1, float valo2, bool inch = false)
        {
            // Faccio la differnza
            float differenza = Math.Abs(valo1 - valo2);

            // Differenzio il calcolo per mm o inch
            // Nel caso di inch, la tolleranza giusta sperimentata e' 0.1 mm
            return (differenza <= (float)(inch ? 0.000394 : 0.01));
        }

        /// <summary>
        /// Macro di identificazione del tipo di profilo per profili angolari(L) o profili PLATS A BOUDIN(B) o angolari ad angolo variabile(V)
        /// </summary>
        /// <param name="profileType">tipo di profilo</param>
        /// <returns></returns>
        public static bool Is_ProfileType_L(this ProfileTypeEnum profileType)
        {
            return profileType == ProfileTypeEnum.L || profileType == ProfileTypeEnum.B || profileType == ProfileTypeEnum.V;
        }

        /// <summary>
        ///  Macro di identificazione del tipo di profilo per profili I
        /// </summary>
        /// <param name="profileType">tipo di profilo</param>
        /// <returns></returns>
        public static bool Is_ProfileType_I(this ProfileTypeEnum profileType)
        {
            return profileType == ProfileTypeEnum.I;
        }

        /// <summary>
        ///  Macro di identificazione del tipo di profilo per profili R
        /// </summary>
        /// <param name="profileType">tipo di profilo</param>
        /// <returns></returns>
        public static bool Is_ProfileType_R(this ProfileTypeEnum profileType)
        {
            return profileType == ProfileTypeEnum.R;
        }

        /// <summary>
        ///  Macro di identificazione del tipo di profilo per profili D
        /// </summary>
        /// <param name="profileType">tipo di profilo</param>
        /// <returns></returns>
        public static bool Is_ProfileType_D(this ProfileTypeEnum profileType)
        {
            return profileType == ProfileTypeEnum.D;
        }

        /// <summary>
        /// Macro di identificazione del tipo di profilo per profili U o C
        /// </summary>
        /// <param name="profileType"> Tipo di profilo </param>
        /// <returns></returns>
        public static bool Is_ProfileType_U(this ProfileTypeEnum profileType)
        {
            //#define IS_COD_U(x)		(BOOLEAN) (x == COD_U || x == COD_C || x == COD_O)
            return profileType == ProfileTypeEnum.U || profileType == ProfileTypeEnum.C;
        }


        /// <summary>
        /// Macro di identificazione del tipo di profilo per profili Tubi Quadri(Q,N)
        /// </summary>
        /// <param name="profileType"> Tipo di profilo </param>
        /// <returns></returns>
        public static bool Is_ProfileType_Q(this ProfileTypeEnum profileType)
        {
            //#define IS_COD_Q(x)		(BOOLEAN) (x == COD_Q || x == COD_N)
            return profileType == ProfileTypeEnum.Q || profileType == ProfileTypeEnum.N;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType">tipo di separationcut</param>
        /// <returns></returns>
        public static bool Is_MSawInitial(this SeparationCutOperationEnum operationType)
        {
            return operationType == SeparationCutOperationEnum.MSawInitial;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType">tipo di separationcut</param>
        /// <returns></returns>
        public static bool Is_MSawFinal(this SeparationCutOperationEnum operationType)
        {
            return operationType == SeparationCutOperationEnum.MSawFinal;
        }

        public static IConfigurationRoot GetConfiguration(bool isTest = false)
        {
            try
            {

                return new ConfigurationBuilder()
                    .SetBasePath(GetStartUpDirectoryInfo().FullName)
                    .AddJsonFile(isTest ?
                          @"bin\config\TestDb.Data.json"
                        : @"bin\config\RepoDbVsEF.Data.json", optional: false, reloadOnChange: true)
                    .Build();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object GetDefaultValue(Type type)
        {
            var typeConverter = TypeDescriptor.GetConverter(type);
            if (type.IsEnum)
            {
                DefaultValueAttribute[] attributes = (DefaultValueAttribute[])type.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                if (attributes != null &&
                    attributes.Length > 0)
                {
                    return typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, attributes[0].Value);
                }
            }
            return null;
        }
    }

}