﻿namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeValue")]
    public class AttributeValue: BaseEntityWithRowVersion, IAuditableEntity
    {
        public long AttributeDefinitionId { get; set; }
        public long EntityId { get; set; }
        public decimal? Value { get; set; }
        public string TextValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual AttributeDefinition AttributeDefinition { get; set; }
        public virtual MasterEntity Entity { get; set; }
    }
}
