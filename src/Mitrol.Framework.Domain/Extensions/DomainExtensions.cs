namespace Mitrol.Framework.Domain
{
    using Microsoft.Extensions.Configuration;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    public static class DomainExtensions
    {
        public const string GENERIC_LABEL = "LBL";

        private static DirectoryInfo StartUpDirectoryInfo { get; set; }

        public static DirectoryInfo GetStartUpDirectoryInfo(string currentPath = null, string fileFilter = "*.init")
        {
            if (StartUpDirectoryInfo == null)
            {
                var directory = new DirectoryInfo(
                    currentPath ?? Directory.GetCurrentDirectory());
                while (directory != null && !directory.GetFiles(fileFilter).Any())
                {
                    directory = directory.Parent;
                }
                StartUpDirectoryInfo = directory;
            }
            return StartUpDirectoryInfo;
        }

        /// <summary>
        /// Get Config Files Directory
        /// </summary>
        /// <returns></returns>
        public static DirectoryInfo GetConfigDirectory()
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var solutionLocation = GetStartUpDirectoryInfo(currentPath: assemblyLocation).FullName;
            return !string.IsNullOrEmpty(solutionLocation) ? new DirectoryInfo($"{solutionLocation}\\bin\\config") : null;
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



        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static string GenerateRandomPhoneNumber()
        {
            return string.Format("07{0}", GeneratRandomNumber(8));
        }
        /// <summary>
        /// Create a random number as a string with a maximum length.
        /// </summary>
        /// <param name="length">Length of number</param>
        /// <returns>Generated string</returns>
        public static string GeneratRandomNumber(int length)
        {
            if (length > 0)
            {
                var sb = new StringBuilder();

                var rnd = SeedRandom();
                for (int i = 0; i < length; i++)
                {
                    sb.Append(rnd.Next(0, 9).ToString());
                }

                return sb.ToString();
            }

            return string.Empty;
        }
        private static Random SeedRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode());
        }

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
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>Custom Attribute if exists</returns>
        public static T GetEnumAttribute<T>(this Enum item)
            => GetEnumAttributes<T>(item).FirstOrDefault();

        /// <summary>
        /// Get Custom T Attribute from TEnum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
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

    }
}
