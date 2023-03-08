﻿namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeDefinition")]
    public class AttributeDefinition: BaseEntityWithRowVersion
    {
        public AttributeDefinitionEnum EnumId { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
    }
}
