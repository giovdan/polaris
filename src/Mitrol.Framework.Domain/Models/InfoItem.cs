namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;
    using System;
    using System.IO;

    public class CodeInfoItem : BaseInfoItem<long, string>
    {
        [JsonProperty("Code")]
        public override string Value { get => base.Value; set => base.Value = value; }

        public CodeInfoItem() : base()
        {

        }

        public CodeInfoItem(long key, string value) : base(key, value)
        {

        }
    }

    public class BaseInfoItem<TKey, TValue>
    {
        [JsonProperty("Id")]
        public TKey Id { get; set; }
        [JsonProperty("Value")]
        public virtual TValue Value { get; set; }

        public BaseInfoItem()
        {
            Value = default;
        }

        public BaseInfoItem(TKey id, TValue value)
        {
            Id = id;
            Value = value;
        }
    }

    public class ImportExportPath
    {
        public ImportExportPath() { }

        public ImportExportPath(long id, string path, DriveType type, string displayName, bool available = true)
        {
            Id = id;
            Path = path;
            Type = type;
            DisplayName = displayName;
            Available = available;
        }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }

        [JsonProperty("Type")]
        public DriveType Type { get; set; }

        [JsonProperty("Available")]
        public bool Available { get; set; } // is the path currently available?

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

    }

    public class ImportExportExtension
    {

        public ImportExportExtension()
        {
        }

        public ImportExportExtension(string extension, string localizationKey)
        {
            Extension = extension;
            LocalizationKey = localizationKey;
        }

        /**
         * File extension (without the leading dot
         */
        [JsonProperty("Extension")]
        public string Extension { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; }
    }

    /**
     * Describes a file/folder
     */
    public class FileSystemItem
    {

        public FileSystemItem() : base()
        {

        }
        //extension senza "."-> ie. polaris oppure piece.polaris
        public FileSystemItem(FileSystemInfo fsi, string extension = "*")
        {
            if (string.Compare(extension, "*") != 0 && fsi.Name.LastIndexOf(extension, StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                Name = fsi.Name.Substring(0, fsi.Name.LastIndexOf(extension, StringComparison.InvariantCultureIgnoreCase) - 1);
            }
            else
                Name = fsi.Name;
            IsFolder = fsi is DirectoryInfo;
            Size = (fsi as FileInfo)?.Length ?? 0;
            LastUpdated = fsi.LastWriteTime;
        }

        [JsonProperty("Name")]
        public string Name { get; }

        [JsonProperty("IsFolder")]
        public bool IsFolder { get; }

        [JsonProperty("Size")]
        public long Size { get; }

        [JsonProperty("LastUpdated")]
        public DateTime LastUpdated { get; }
    }

    public class InfoItem<TValue> : BaseInfoItem<long, TValue>
    {
        public InfoItem() : base()
        {

        }

        public InfoItem(long id, TValue value) : base(id, value)
        {

        }

        [JsonProperty("SuggestedFormat")]
        public string SuggestedFormat { get; set; }
    }

    public class LocalizedInfoItem<T>
    {
        [JsonProperty("Value")]
        public T Value { get; set; }
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
        [JsonProperty("UMLocalizationKey")]
        public string UMLocalizationKey { get; set; }
    }

    public class LocalizedInfoItemWithImage<T> : LocalizedInfoItem<T>
    {
        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }
        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// Class that represent a list box item
    /// </summary>
    public class ListBoxItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }
    }

    /// <summary>
    /// Class that represent a combo box item
    /// </summary>
    public class ComboBoxItem
    {

        [JsonProperty("Value")]
        public string Value { get; set; }
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
        [JsonProperty("LocalizedText")]
        public string LocalizedText { get; set; }
    }
}
