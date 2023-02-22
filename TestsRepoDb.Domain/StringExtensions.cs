

namespace RepoDbVsEF.Domain
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(this string value) => !string.IsNullOrEmpty(value);

        public static bool IsNotNullOrWhiteSpace(this string value) => !string.IsNullOrWhiteSpace(value);

        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// Funzione di utilità per separare il percorso del file eseguibile dai suoi argomenti.
        /// </summary>
        /// <param name="pathAndArgs">
        /// Stringa che contiene il path del file eseguibile seguito dai suoi argomenti separati da
        /// spazio.
        /// </param>
        /// <returns>
        /// Tupla che contiene il percorso del file eseguibile ed i suoi argomenti.
        /// </returns>
        public static (string fileName, string arguments) SplitPathAndArgs(this string pathAndArgs)
        {
            if (pathAndArgs.IsNullOrEmpty())
                return (string.Empty, string.Empty);

            var first = Regex.Matches(pathAndArgs, @"(?<=^[^\""]*(?:\""[^\""]*\""[^\""]*)*)\s(?=(?:[^\""]*\""[^\""]*\"")*[^\""]*$)")
                .OfType<Match>()
                .FirstOrDefault();

            return first?.Success ?? false
                ? (fileName: pathAndArgs.Substring(0, first.Index), arguments: pathAndArgs.Substring(first.Index + 1))
                : (fileName: pathAndArgs, arguments: "");
        }

        public static string GetPathDiff(this string fullPath, string basePath)
        {
            basePath = basePath.EnsureDirectoryTerminatesWithSeparatorChar();

            if (!fullPath.StartsWith(basePath))
                throw new Exception("Could not find rootPath in fullPath when calculating relative path.");

            return fullPath.Substring(basePath.Length);
        }

        public static bool IsFullPath(this string path)
        {
            if (string.IsNullOrWhiteSpace(path)
                || path.IndexOfAny(Path.GetInvalidPathChars()) != -1
                || !Path.IsPathRooted(path))
            {
                return false;
            }

            var pathRoot = Path.GetPathRoot(path);
            if (pathRoot.Length <= 2 && pathRoot != "/") // Accepts X:\ and \\UNC\PATH, rejects empty string, \ and X:, but accepts / to support Linux
            {
                return false;
            }

            if (pathRoot[0] != '\\' || pathRoot[1] != '\\')
            {
                return true; // Rooted and not a UNC path
            }

            return pathRoot.Trim('\\').IndexOf('\\') != -1; // A UNC server name without a share name (e.g "\\NAME" or "\\NAME\") is invalid
        }

        public static string EnsureDirectoryTerminatesWithSeparatorChar(this string source)
            => source.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
    }
}
