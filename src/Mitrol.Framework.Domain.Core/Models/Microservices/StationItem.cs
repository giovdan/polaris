namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class StationItem : Station
    {
        public StationItem()
        {

        }

        public StationItem(Station station)
            : base(station.Id, station.NX, station.NY, station.NA, station.OriginPointsValidity)
        {

        }

        [JsonProperty("CodeGenerators")]
        public Dictionary<string, object> CodeGenerators { get; set; }

        // TODO: resolve around production row
        //[JsonProperty("ProfileType")]
        //public ProfileTypeEnum ProfileType { get; set; }

        //[JsonProperty("SuggestedCodeFormat")]
        //public string SuggestedCodeFormat
        //{
        //    get
        //    {
        //        var suggstedCodeFormat =
        //                    DomainExtensions.GetEnumAttribute<ProfileTypeEnum, SuggestedFormatAttribute>(ProfileType);
        //        return suggstedCodeFormat?.SuggestedFormat ?? string.Empty;
        //    }
        //}

        [JsonProperty("OriginModality")]
        public byte OriginModality { get; set; }
    }
}