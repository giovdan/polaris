namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MachineParameter")]
    public class MachineParameter: AuditableExtendedEntity
    {
        [Required()]
        public string Code { get; set; }
        [Required()]
        public string DescriptionLocalizationKey { get; set; }
        [Required()]
        public string HelpLocalizationKey { get; set; }
        public decimal DefaultValue { get; set; }
        public decimal Value { get; set; }
        public decimal MinimumValue { get; set; }
        public decimal MaximumValue { get; set; }
        public string ImageCode { get; set; }
        public string IconCode { get; set; }
        [Required()]
        public long DataFormatId { get; set; }
        [Required()]
        [Column(TypeName = "ENUM('Critical','High','Medium','Normal','ReadOnly')")]
        public ProtectionLevelEnum ProtectionLevel { get; set; }
        [Required()]
        [Column(TypeName = "ENUM('GEN','BAR','TMP','PAR','VAR','PAXE')")]
        public ParameterCategoryEnum Category { get; set; }
        public virtual ICollection<MachineParameterLink> Links { get; set; }
        
    }
}
