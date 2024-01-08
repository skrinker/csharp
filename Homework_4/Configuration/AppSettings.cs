using Newtonsoft.Json;

namespace HttpServer.Configuration;

public class AppSettingsConfig
{
    [JsonProperty("address")]
    public string Address { get; set; }
    
    [JsonProperty("port")]
    public uint Port { get; set; }

    [JsonProperty("staticFilesPath")]
    public string StaticPathFiles { get; set; }
}