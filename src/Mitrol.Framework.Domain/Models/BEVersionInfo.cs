namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.NetworkInformation;

    public class GitVersionInfo
    {
        [JsonProperty("gitCommitHash")]
        public string GitCommitHash { get; set; }
        [JsonProperty("gitCommitDate")]
        public string GitCommitDate { get; set; }
        [JsonProperty("gitRefs")]
        public string GitCommitRefs { get; set; }

    }

    public class BEVersionInfo: GitVersionInfo
    {
        [JsonProperty("BEVersion")]
        public string BEVersion { get; set; }
        [JsonProperty("DBVersion")]
        public string DBVersion { get; set; }
        [JsonProperty("HostName")]
        public string HostName { get; set; }
        [JsonProperty("NetworkInfos")]
        public IEnumerable<NetworkInfo> NetworkInfos { get; set; }
        [JsonProperty("buildDate")]
        public string BuildDate { get; set; }
    }

    public class NetworkInfo
    {
        [JsonProperty("InterfaceName")]
        public string InterfaceName { get; set; }

        [JsonProperty("PhysicalAddress")]
        public string PhysicalAddress { get; set; }

        [JsonProperty("IpAddresses")]
        public IEnumerable<IPNetworkAddress> IpAddresses { get; set; }
    }

    public class IPNetworkAddress
    {
        public IPNetworkAddress()
        {

        }

        public IPNetworkAddress(UnicastIPAddressInformation unicastAddress)
        {
            Address = unicastAddress.Address.ToString();
            IPv4Mask = unicastAddress.IPv4Mask.Address == default ? null : unicastAddress.IPv4Mask.ToString();
        }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("IPv4Mask")]
        public string IPv4Mask { get; set; }
    }
}
