// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global
namespace LineApiWrapper.Configuration.Interfaces;

public interface ILineApiConfiguration
{
    /// <summary>
    ///     The default request timeout in ms
    /// </summary>
    int RequestTimeout { get; set; }

    /// <summary>
    ///     Gets or sets the default retry mode.
    /// </summary>
    /// <value>The default retry mode.</value>
    RetryMode RetryMode { get; set; }

    /// <summary>
    ///     Gets or sets the channel access token.
    /// </summary>
    /// <value>The channel access token.</value>
    string ChannelAccessToken { get; set; }

    /// <summary>
    ///     Gets or sets the channel secret.
    /// </summary>
    /// <value>The channel secret.</value>
    string ChannelSecret { get; set; }
}