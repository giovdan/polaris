namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

 
    public class StockItemToAdd
    {
        [JsonProperty("ProfileTypeId")]
        public long ProfileTypeId { get; set; }
        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }
    }
}
