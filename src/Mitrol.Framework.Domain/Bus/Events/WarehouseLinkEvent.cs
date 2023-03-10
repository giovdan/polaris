namespace Mitrol.Framework.Domain.Bus
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class WarehouseLinkEvent: Event
    {
        //[JsonProperty("OperationType")]
        //public CRUD_OperationTypeEnum OperationType { get; set; }

        [JsonProperty("WarehouseId")]
        public long WarehouseId { get; set; }

        [JsonProperty("EntityId")]
        public long EntityId { get; set; }

        [JsonProperty("EntityCode")]
        public string EntityCode { get; set; }

        [JsonProperty("EntityTypeId")]
        public EntityTypeEnum EntityTypeId { get; set; }

        [JsonProperty("Notes")]
        public string Notes { get; set; }

        public WarehouseLinkEvent()
        {
            //OperationType = CRUD_OperationTypeEnum.Create;
            //EntityTypeId = WarehouseEntityTypeEnum.Tool;
        }
    }
}