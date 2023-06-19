namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    public class PathContentParam
    {
        public string basePath;

        public bool includeFiles = true;
        public bool includeFolders = true;

        public ImportExportObjectEnum type = ImportExportObjectEnum.None;
    };
}
