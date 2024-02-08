// ReSharper disable IdentifierTypo
namespace LineApiWrapper.Webhooks.Events;

/// <summary>
///     Event object for when your account is added as a friend (or unblocked). You can reply to follow events.
/// </summary>
public class FollowEvent : ReplyableEvent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FollowEvent" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="timestamp">The timestamp.</param>
    /// <param name="replyToken">The reply token.</param>
    public FollowEvent(WebhookEventSource source, long timestamp, string replyToken)
        : base(WebhookEventType.Follow, source, timestamp, replyToken)
    {
    }
}