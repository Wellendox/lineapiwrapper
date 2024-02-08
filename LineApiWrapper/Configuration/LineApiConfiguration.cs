// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable RegionWithSingleElement
namespace LineApiWrapper.Configuration;

/// <summary>
///     The Line API Configuration. Includes static properties as well as non-static for instances of
///     the class.
/// </summary>
/// <seealso cref="ILineApiConfiguration" />
public class LineApiConfiguration : ILineApiConfiguration
{
    #region Constructors

    public LineApiConfiguration(string channelAccessToken, string channelSecret)
    {
        ChannelAccessToken = channelAccessToken;
        ChannelSecret = channelSecret;
    }

    #endregion Constructors

    #region Properties

    /// <summary>
    ///     The domain
    /// </summary>
    public const string Domain = "api-data.line.me";

    /// <summary>
    ///     The base domain URL for other APIs
    /// </summary>
    public const string Other = "api.line.me";

    /// <summary>
    ///     The domain URL
    /// </summary>
    public const string DomainUrl = "https://api-data.line.me/api/v2";

    /// <summary>
    ///     The other URL
    /// </summary>
    public const string OtherUrl = "https://api.line.me/v2";

    /// <summary>
    ///     The maximum message size
    /// </summary>
    public const int MaxMessageSize = 5000;

    /// <summary>
    ///     Gets the default channel access token.
    /// </summary>
    /// <value>
    ///     The default channel access token.
    /// </value>
    public const string DefaultChannelAccessToken =
        "ggIOv7C9Ln8ly5WE7D2nnZZfdVkx57QC5pgQQzVEq8Fxn29iH3Bi+iLv1bO/mXWeBw+pZOcsXypZhpAK7EDscsiRmssEniCoWfWqNEwbE/JKDG0X/OkJPbWpHxxLgExB4vk3j5md8u1pb8gfMGIa/QdB04t89/1O/w1cDnyilFU=";

    /// <summary>
    ///     Gets the default channel secret.
    /// </summary>
    /// <value>
    ///     The default channel secret.
    /// </value>
    public const string DefaultChannelSecret = "6991eeb03da03c30dbab10ddfacbaba5";

    /// <summary>
    ///     The maximum messages in request
    /// </summary>
    public const int MaxMessagesInRequest = 5;

    /// <summary>
    ///     The default request timeout
    /// </summary>
    public const int DefaultRequestTimeout = 15000;

    /// <summary>
    ///     Gets or sets the default retry mode.
    /// </summary>
    /// <value>
    ///     The default retry mode.
    /// </value>
    public const RetryMode DefaultRetryMode = RetryMode.AlwaysRetry;

    /// <inheritdoc />
    public int RequestTimeout { get; set; }

    /// <inheritdoc />
    public RetryMode RetryMode { get; set; }

    /// <inheritdoc />
    public string ChannelAccessToken { get; set; }

    /// <inheritdoc />
    public string ChannelSecret { get; set; }

    #endregion Properties
}