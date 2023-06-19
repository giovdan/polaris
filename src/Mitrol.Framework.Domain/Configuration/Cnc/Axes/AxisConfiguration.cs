namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Class that represents the settings for Axis configuration.
    /// </summary>
    public class AxisConfiguration
    {
        internal const string s_nameJsonName = "Name";
        internal const string s_analogInputIndexJsonName = "AnalogInputIndex";
        internal const string s_analogOutputIndexJsonName = "AnalogOutputIndex";
        internal const string s_categoryJsonName = "Category";
        internal const string s_encSyncJsonName = "EncSync";
        internal const string s_indexJsonName = "Index";
        internal const string s_localizationKeyJsonName = "LocalizationKey";
        internal const string s_nodeJsonName = "Node";
        internal const string s_pathJsonName = "Path";
        internal const string s_typeJsonName = "Type";
        internal KnownAxisNameEnum? _knownName;

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public AxisConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public AxisConfiguration([JsonProperty(s_nameJsonName)] string name,
                                 [JsonProperty(s_pathJsonName)] byte? path,
                                 [JsonProperty(s_indexJsonName)] byte? index,
                                 [JsonProperty(s_categoryJsonName)] AxisCategoryEnum? category,
                                 [JsonProperty(s_typeJsonName)] AttributeDataFormatEnum? type,
                                 [JsonProperty(s_encSyncJsonName)] byte? encSync,
                                 [JsonProperty(s_localizationKeyJsonName)] string localizationKey,
                                 [JsonProperty(s_nodeJsonName)] NodeReference node,
                                 [JsonProperty(s_analogInputIndexJsonName)] ushort? analogInputIndex,
                                 [JsonProperty(s_analogOutputIndexJsonName)] ushort? analogOutputIndex)
        {
            Name = name;
            Path = path;
            Index = index;
            Category = category;
            Type = type;
            EncSync = encSync;
            LocalizationKey = localizationKey;
            Node = node;
            AnalogInputIndex = analogInputIndex;
            AnalogOutputIndex = analogOutputIndex;
        }

        [JsonProperty(s_analogInputIndexJsonName)]
        public ushort? AnalogInputIndex { get; protected set; }

        [JsonProperty(s_analogOutputIndexJsonName)]
        public ushort? AnalogOutputIndex { get; protected set; }

        [JsonProperty(s_categoryJsonName)]
        public AxisCategoryEnum? Category { get; protected set; }

        [JsonProperty(s_encSyncJsonName)]
        public byte? EncSync { get; protected set; }

        [JsonProperty(s_indexJsonName)]
        public byte? Index { get; protected set; }

        [JsonIgnore()]
        public KnownAxisNameEnum KnownName
        {
            get
            {
                if (!_knownName.HasValue)
                {
                    if (Enum.TryParse<KnownAxisNameEnum>(Name, out var knownName))
                    {
                        _knownName = knownName;
                    }
                    else
                    {
                        _knownName = 0;
                    }
                }
                return _knownName.Value;
            }
        }

        [JsonProperty(s_localizationKeyJsonName)]
        public string LocalizationKey { get; protected set; }

        [JsonProperty(s_nameJsonName)]
        public string Name { get; protected set; }

        [JsonProperty(s_nodeJsonName)]
        public NodeReference Node { get; protected set; }

        [JsonProperty(s_pathJsonName)]
        public byte? Path { get; protected set; }

        /// <summary>
        /// Formato del valore della quota asse
        /// </summary>
        [JsonProperty(s_typeJsonName)]
        public AttributeDataFormatEnum? Type { get; protected set; }

        public bool IsDynamicPathAxis() => KnownName is KnownAxisNameEnum.XD;
    }
}