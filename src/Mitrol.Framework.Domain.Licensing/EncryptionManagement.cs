namespace Mitrol.Framework.Domain.Licensing
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public class EncryptionManagement
    {
        private const string VALIDATIONFILE_REGEX = @"^(//FILE_VALIDATION=)(?<value>[\S]*)?";
        private const string header = @"//FILE_VALIDATION=";
        

        public static bool ValidateFileContent(string fileContent)
        {
            // Cerco la stringa di validazione del contenuto
            var _regex = new Regex(VALIDATIONFILE_REGEX,RegexOptions.Multiline);

            var m = _regex.Match(fileContent);
            if (m.Success) // se è presente la chiave di validazione 
            {
                // Recupero l'Hash 
                var fileHashCode = m.Groups["value"].Value;

                return VerificaHashCode(fileHashCode, fileContent.Substring(m.Value.Length).TrimStart().TrimEnd());
            }
            return false;
        }

        public static bool ValidateFileContent(string hashCode,string content)
        {
            return VerificaHashCode(hashCode, content);
        }

        private static bool VerificaHashCode(string hashCode,string content)
        {
            // Recupero il contenuto effettivo del file(esclusa la chiave di validazione) e ne calcolo l'hash
            var contentHashCode = GetHashValue(content);

            // Verifico se l'Hash del file è lo stesso di quello che è stato calcolato 
            if (string.Compare(contentHashCode, hashCode, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                // File valido
                return true;
            }
            return false;
        }

        public static string GetValidationKey(string objectToCript)
        {
            return $"{header}{GetHashValue(objectToCript)}";
        }

        private static string GetHashValue(string objectToEncrypt)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            //Add encryption key
            objectToEncrypt += "Mitrol Encryption File";
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(objectToEncrypt));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
