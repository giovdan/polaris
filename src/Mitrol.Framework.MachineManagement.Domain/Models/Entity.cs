namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models.Database;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Models.General;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Entity")]
    public class Entity : BaseAuditableEntityWithRowVersion
    {
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        public string HashCode { get; set; }
        public long? SecondaryKey { get; set; }
        [Column(TypeName = "ENUM ('Available','Unavailable','Warning','Alarm','NoIconToDisplay','ToBeDeleted','Original','ModifiedByCustomer','ModifiedByFICEP','ToBeSkipped','Empty','InProgress','Processed','Executed','RQLoad','RQAck','NotAvailable','Aborted','NotReady')")]
        public EntityStatusEnum Status { get; set; }
        public int Priority { get; set; }
    }
}
