// ReSharper disable RegionWithSingleElement
namespace LineApiWrapper.Messages;

internal sealed class PushMessage : IPushMessage
{
    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="PushMessage" /> class.
    /// </summary>
    /// <param name="to">To.</param>
    /// <param name="messages">The messages.</param>
    public PushMessage(string to, IEnumerable<IMessage> messages)
    {
        To = to;
        Messages = messages;
    }

    #endregion Constructors

    #region Properties

    /// <inheritdoc />
    [JsonProperty("to")]
    public string To { get; }

    /// <inheritdoc />
    [JsonProperty("messages")]
    public IEnumerable<IMessage> Messages { get; }

    #endregion Properties
}