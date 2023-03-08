namespace Mitrol.Framework.Domain
{
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.Configuration;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    public static class DomainExtensions
    {
        private static DirectoryInfo s_startUpDirectoryInfo { get; set; }

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

        public static IConfigurationRoot GetConfiguration()
        {
            try
            {

                return new ConfigurationBuilder()
                    .SetBasePath(GetStartUpDirectoryInfo().FullName)
                    .AddJsonFile(@"bin\config\RepoDbVsEF.Data.json", optional: false, reloadOnChange: true)
                    .Build();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static MasterEntity GenerateEntity(EntityTypeEnum entityType)
        {
            return new MasterEntity
            {
                EntityTypeId = entityType
            };
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

        public static void SetAuditableFields(this IAuditableEntity auditableEntity, string userName)
        {
            auditableEntity.CreatedBy = userName;
            auditableEntity.CreatedOn = auditableEntity.CreatedOn == DateTime.MinValue ?  DateTime.UtcNow: auditableEntity.CreatedOn;
            auditableEntity.UpdatedBy = userName;
            auditableEntity.UpdatedOn = DateTime.UtcNow;
        }

        public static void SetAuditableFields(this IEnumerable<IAuditableEntity> auditableEntities, string userName)
        {
            foreach(var auditableEntity in auditableEntities)
            {
                auditableEntity.CreatedBy = userName;
                auditableEntity.CreatedOn = auditableEntity.CreatedOn == DateTime.MinValue ? DateTime.UtcNow : auditableEntity.CreatedOn;
                auditableEntity.UpdatedBy = userName;
                auditableEntity.UpdatedOn = DateTime.UtcNow;

            }
        }

        public static IDbTransaction GetDbTransaction(this IDbContextTransaction source)
        {
            return (source as IInfrastructure<IDbTransaction>).Instance;
        }
    }
}
