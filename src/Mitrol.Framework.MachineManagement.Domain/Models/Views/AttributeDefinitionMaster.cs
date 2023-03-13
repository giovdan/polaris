using Mitrol.Framework.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitrol.Framework.MachineManagement.Domain.Models.Views
{
    public class AttributeDefinitionMaster
    {
        public long Id { get; set; }
        public AttributeDefinitionEnum EnumId { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public AttributeTypeEnum AttributeTypeId { get; set; }
        public ToolTypeEnum ToolTypeId { get; set; }
        public EntityTypeEnum ParentTypeId { get; set; }
        public int Priority { get; set; }
        public GroupEnum Owner { get; set; }
        
        public ClientControlTypeEnum ControlTypeId { get; set; }
        public OverrideTypeEnum OverrideTypeId { get; set; }
        public AttributeDataFormatEnum DataFormatId { get; set; }
        public AttributeKindEnum AttributeKindId { get; set; }
        public string TypeName { get; set; }
        public AttributeDefinitionGroupEnum GroupId { get; set; }
        public string HelpImage { get; set; }
    }
}
