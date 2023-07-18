namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [ImportExport(ImportExportObjectEnum.StockObject, typeof(IStockService), typeof(StockExportFilter))]
    public class StockImportItem : BaseStockImportItem
    {
        [JsonProperty("TotalQuantity")]
        public int TotalQuantity { get; set; }

        [JsonProperty("ExecutedQuantity")]
        public int ExecutedQuantity { get; set; }  

        [JsonProperty("ProfileType")]
        public string ProfileType { get; set; }

        public StockImportItem():base()
        {
        }
        public override ProfileTypeEnum GetProfile()
        {
            var resProfileType = DomainExtensions.GetIntEnumValueFromString(typeof(ProfileTypeEnum).AssemblyQualifiedName.ToString(), ProfileType);

            if (resProfileType.Success)
                return (ProfileTypeEnum)resProfileType.Value;

            return ProfileTypeEnum.X;
        }

        public override void Convert(JObject jObject) //Oggetto di tipo JObject, va convertito per essere utilizzato
        {
            StockImportItem stock = jObject.ToObject<StockImportItem>();
            ProfileType = stock.ProfileType;
            TotalQuantity = stock.TotalQuantity;
            ExecutedQuantity = stock.ExecutedQuantity;
            Attributes = stock.Attributes;
        }
    }
}
