// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace LineApiWrapper.Webhooks.Events;

/// <summary>
///     Event object which contains the sent message. The message field contains a message object which
///     corresponds with the message type. You can reply to message events.
/// </summary>
public class MessageEvent : ReplyableEvent
{
    /// <summary>
    ///     Contents of the message
    /// </summary>
    public EventMessage Message { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="MessageEvent" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="timestamp">The timestamp.</param>
    /// <param name="message">The message.</param>
    /// <param name="replyToken">The reply token.</param>
    public MessageEvent(WebhookEventSource source, long timestamp, EventMessage message, string replyToken)
        : base(WebhookEventType.Message, source, timestamp, replyToken)
    {
        Message = message;
    }
}