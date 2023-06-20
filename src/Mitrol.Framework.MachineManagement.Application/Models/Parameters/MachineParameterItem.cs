namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    public class MachineParameterToImportValidator: AbstractValidator<MachineParameterToImport>
    {
        public MachineParameterToImportValidator()
        {
            RuleFor(x => x.Category)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_PAR001.ToString());

            RuleFor(x => x.DefaultValue)
                .GreaterThanOrEqualTo(x => x.MinimumValue)
                .When(x => x.MinimumValue != 0)
                .WithErrorCode(ErrorCodesEnum.ERR_PAR004.ToString());

            RuleFor(x => x.DefaultValue)
                .LessThanOrEqualTo(x => x.MaximumValue)
                .When(x => x.MaximumValue != 0)
                .WithErrorCode(ErrorCodesEnum.ERR_PAR004.ToString());

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithErrorCode(ErrorCodesEnum.ERR_PAR002.ToString());

            RuleForEach(x => x.CncAssociations)
                .SetValidator(new MachineParameterLinkValidator())
                .When(x => x.CncAssociations.Any());
            
        }
    }
    public class MachineParameterToImport
    {
        #region < Property Constants >
        internal const string s_Code = "Code";
        internal const string s_Description = "Description";
        internal const string s_Help = "Help";
        internal const string s_ImageCode = "ImageCode";
        internal const string s_IconCode = "IconCode";
        internal const string s_DefaultValue = "DefaultValue";
        internal const string s_MaximumValue = "MaximumValue";
        internal const string s_MinimumValue = "MinimumValue";
        internal const string s_DataFormat = "DataFormat";
        internal const string s_Category = "Category";
        internal const string s_CncAssociations = "CncAssociations";
        internal const string s_ProtectionLevel = "ProtectionLevel";
        internal const string s_Owners = "Owners";
        #endregion

        [JsonProperty(s_Code)]
        public string Code { get; set; }

        [JsonProperty(s_Description)]
        public string Description { get; set; }

        [JsonProperty(s_Help)]
        public string Help { get; set; }

        [JsonProperty(s_ImageCode)]
        public string ImageCode { get; set; }

        [JsonProperty(s_IconCode)]
        public string IconCode { get; set; }

        [JsonProperty(s_DefaultValue)]
        public decimal DefaultValue { get; set; }

        [JsonProperty(s_MaximumValue)]
        public decimal MaximumValue { get; set; }

        [JsonProperty(s_MinimumValue)]
        public decimal MinimumValue { get; set; }

        [JsonProperty(s_DataFormat)]
        public AttributeDataFormatEnum ParameterDataFormat { get; set; }

        [JsonProperty(s_Category)]
        public ParameterCategoryEnum Category { get; set; }

        [JsonProperty(s_CncAssociations)]
        public List<MachineParameterLinkToImport> CncAssociations { get; set; }

        [JsonProperty(s_ProtectionLevel)]
        public ProtectionLevelEnum ProtectionLevel { get; set; }

        public MachineParameterToImport()
        {
            CncAssociations = new List<MachineParameterLinkToImport>();
        }

        [JsonConstructor]
        public MachineParameterToImport([JsonProperty(s_Code)] string code,
                                        [JsonProperty(s_Description)] string description,
                                        [JsonProperty(s_Help)] string help,
                                        [JsonProperty(s_ImageCode)] string imageCode,
                                        [JsonProperty(s_IconCode)] string iconCode,
                                        [JsonProperty(s_DefaultValue)] decimal defaultValue,
                                        [JsonProperty(s_MaximumValue)] decimal maximumValue,
                                        [JsonProperty(s_MinimumValue)] decimal minimumValue,
                                        [JsonProperty(s_DataFormat)] AttributeDataFormatEnum parameterDataFormat,
                                        [JsonProperty(s_Category)] ParameterCategoryEnum category,
                                        [JsonProperty(s_CncAssociations)] List<MachineParameterLinkToImport> cncAssociations,
                                        [JsonProperty(s_ProtectionLevel)] ProtectionLevelEnum protectionLevel,
                                        [JsonProperty(s_Owners)] string owners)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Help = help ?? throw new ArgumentNullException(nameof(help));
            ImageCode = imageCode ?? throw new ArgumentNullException(nameof(imageCode));
            IconCode = iconCode ?? throw new ArgumentNullException(nameof(iconCode));
            DefaultValue = defaultValue;
            MaximumValue = maximumValue;
            MinimumValue = minimumValue;
            ParameterDataFormat = parameterDataFormat;
            Category = category;
            CncAssociations = cncAssociations ?? new List<MachineParameterLinkToImport>();
            // Retro compatibilità 
            if (!string.IsNullOrEmpty(owners))
            {
                var typeConverter = TypeDescriptor.GetConverter(typeof(ProtectionLevelEnum));
                ProtectionLevel =  (ProtectionLevelEnum)typeConverter.ConvertFromString(null, CultureInfo.InvariantCulture
                        , owners);
            }
            else
            {
                ProtectionLevel = protectionLevel;
            }
        }
    }

    
}