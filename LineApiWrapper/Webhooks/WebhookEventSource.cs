// ReSharper disable once IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedMember.Global

namespace LineApiWrapper.Webhooks;

/// <summary>
///     Webhook Event Source. Source could be User, Group or Room.
/// </summary>
public class WebhookEventSource
{
    /// <summary>
    ///     Gets the type.
    /// </summary>
    /// <value>The type.</value>
    public EventSourceType Type { get; }

    /// <summary>
    ///     UserId of the Group or Room
    /// </summary>
    public string UserId { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="WebhookEventSource" /> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="userId">The user identifier.</param>
    public WebhookEventSource(EventSourceType type, string userId)
    {
        Type = type;
        UserId = userId;
    }

    /// <summary>
    ///     Creates from.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "RedundantAssignment")]
    internal static WebhookEventSource? CreateFrom(dynamic source)
    {
        if (source == null) { return null; }

        if (!Enum.TryParse((string)source.type, true, out EventSourceType sourceType))
        {
            return null;
        }

        // ReSharper disable once NotAccessedVariable
        string sourceId;
        switch (sourceType)
        {
            case EventSourceType.User:
                sourceId = source.userId.ToString();
                break;

            case EventSourceType.Group:
                sourceId = source.groupId.ToString();
                break;

            case EventSourceType.Room:
                sourceId = source.roomId.ToString();
                break;

            default:
                return null;
        }

        return new WebhookEventSource(sourceType, source.userId.ToString());
    }
}