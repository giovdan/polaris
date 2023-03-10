namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;

    public class ProductionWorkDoneEvent : Event
    {
        //[JsonProperty("Entity")]
        //public WorkDoneEntity Entity { get; set; }
        [JsonProperty("LoggedUser")]
        public string LoggedUser { get; set; }
        [JsonProperty("MachineName")]
        public string MachineName { get; set; }
    }

    
}
