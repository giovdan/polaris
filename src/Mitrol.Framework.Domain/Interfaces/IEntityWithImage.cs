namespace Mitrol.Framework.Domain.Interfaces
{
    using Newtonsoft.Json;

    public interface IEntityWithImage
    {
        [JsonProperty("ImageCode")]
        string ImageCode { get; set; }
    }
}
