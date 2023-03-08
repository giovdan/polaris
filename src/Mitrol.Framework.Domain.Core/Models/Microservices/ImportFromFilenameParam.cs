namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ImportFromFilenameParam
    {
        [JsonProperty("FullName")]
        // Fullname of the file to be imported
        public string FullName { get; set; }
        [JsonProperty("ObjectType")]
        // Type of contained object
        public ImportExportObjectEnum ObjectType { get; set; }
    }

    public class ImportFromFilenameParam2
    {
        [JsonProperty("FullNames")]
        // Fullname of the file to be imported
        public List<string> FullNames { get; set; }
        [JsonProperty("ObjectType")]
        // Type of contained object
        public ImportExportObjectEnum ObjectType { get; set; }
    }

    public class ImportFromFilenameParam2_
    {
        [JsonProperty("Path")]
        // Path of the files to be imported
        public string Path { get; set; }

        [JsonProperty("ObjectNames")]
        // Fullname of the file to be imported
        public List<string> ObjectNames { get; set; }
        [JsonProperty("ObjectType")]
        // Type of contained object
        public ImportExportObjectEnum ObjectType { get; set; }
    }
}
