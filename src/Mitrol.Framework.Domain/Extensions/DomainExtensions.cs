namespace Mitrol.Framework.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    public static class DomainExtensions
    {
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


        #region < ToHex Extensions >
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
        #endregion
    }
}
