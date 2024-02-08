// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PublicConstructorInAbstractClass
namespace LineApiWrapper.Webhooks.Events;

public abstract class ReplyableEvent : WebhookEvent
{
    /// <summary>
    ///     Gets the reply token.
    /// </summary>
    /// <value>The reply token.</value>
    public string ReplyToken { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ReplyableEvent" /> class.
    /// </summary>
    /// <param name="eventType">Type of the event.</param>
    /// <param name="source">The source.</param>
    /// <param name="timestamp">The timestamp.</param>
    /// <param name="replyToken">The reply token.</param>
    public ReplyableEvent(WebhookEventType eventType, WebhookEventSource source, long timestamp, string replyToken)
        : base(eventType, source, timestamp)
    {
        ReplyToken = replyToken;
    }
}