
namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IConfigurationService
    {
        IEnumerable<ImportExportPath> GetImportExportPaths();
        Task<IEnumerable<ImportExportPath>> GetImportExportPathsAsync();
        IEnumerable<FileSystemItem> GetImportExportPathContent(PathContentParam param);
        IEnumerable<ImportExportExtension> GetTypesFileforImport(ImportExportObjectEnum type);
        bool IsFileValidationEnabled();
    }
}
