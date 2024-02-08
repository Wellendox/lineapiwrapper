// ReSharper disable IdentifierTypo
namespace LineApiWrapper.Webhooks.Events;

/// <summary>
///     Event object for when your account is blocked.
/// </summary>
public class UnfollowEvent : WebhookEvent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UnfollowEvent" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="timestamp">The timestamp.</param>
    public UnfollowEvent(WebhookEventSource source, long timestamp)
        : base(WebhookEventType.Unfollow, source, timestamp)
    {
    }
}