// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace LineApiWrapper.Webhooks;

/// <summary>
///     The webhook event generated on the LINE Platform.
///     https://developers.line.me/en/docs/messaging-api/reference/#webhook-event-objects
/// </summary>
public abstract class WebhookEvent
{
    /// <summary>
    ///     Identifier for the type of event
    /// </summary>
    public WebhookEventType Type { get; set; }

    /// <summary>
    ///     JSON object which contains the source of the event
    /// </summary>
    public WebhookEventSource Source { get; }

    /// <summary>
    ///     Time of the event in milliseconds
    /// </summary>
    public long Timestamp { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="WebhookEvent" /> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="source">The source.</param>
    /// <param name="timestamp">The timestamp.</param>
    public WebhookEvent(WebhookEventType type, WebhookEventSource source, long timestamp)
    {
        Type = type;
        Source = source;
        Timestamp = timestamp;
    }

    /// <summary>
    ///     Creates from.
    /// </summary>
    /// <param name="dynamicObject">The dynamic object.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">dynamicObject</exception>
    internal static WebhookEvent? CreateFrom(dynamic dynamicObject)
    {
        if (dynamicObject == null) { throw new ArgumentNullException(nameof(dynamicObject)); }

        var eventSource = WebhookEventSource.CreateFrom(dynamicObject.source);

        if (eventSource == null)
        {
            return null;
        }

        if (!Enum.TryParse((string)dynamicObject.type, true, out WebhookEventType eventType))
        {
            return null;
        }

        switch (eventType)
        {
            case WebhookEventType.Message:
                EventMessage eventMessage = EventMessage.CreateFrom(dynamicObject);
                if (eventMessage == null)
                    return null;
                return new MessageEvent(eventSource, (long)dynamicObject.timestamp, eventMessage,
                    (string)dynamicObject.replyToken);

            case WebhookEventType.Follow:
                return new FollowEvent(eventSource, (long)dynamicObject.timestamp, (string)dynamicObject.replyToken);

            case WebhookEventType.Unfollow:
                return new UnfollowEvent(eventSource, (long)dynamicObject.timestamp);

            default:
                return null;
        }
    }
}