namespace LineApiWrapper.Json;

/// <summary>
/// The CamelCaseJsonSerializerSettings
/// </summary>
/// <seealso cref="Newtonsoft.Json.JsonSerializerSettings"/>
internal class CamelCaseJsonSerializerSettings : JsonSerializerSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CamelCaseJsonSerializerSettings"/> class.
    /// </summary>
    public CamelCaseJsonSerializerSettings()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver();
        Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
        NullValueHandling = NullValueHandling.Ignore;
    }
}