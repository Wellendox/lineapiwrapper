// ReSharper disable IdentifierTypo
// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CommentTypo
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedMember.Global
namespace LineApiWrapper.Webhooks;

/// <summary>
///     Inherit this class to implement LINE Bot. Then override each event handler.
/// </summary>
public abstract class LineApiWebhookBase
{
    /// <summary>
    ///     The message event
    /// </summary>
    internal readonly AsyncEvent<Func<MessageEvent, Task>> MessageEvent = new();

    /// <summary>
    ///     The follow event
    /// </summary>
    internal readonly AsyncEvent<Func<FollowEvent, Task>> FollowEvent = new();

    /// <summary>
    ///     The unfollow event
    /// </summary>
    internal readonly AsyncEvent<Func<UnfollowEvent, Task>> UnfollowEvent = new();

    /// <summary>
    ///     Occurs when the line API sends a Message event through the Webhook.
    /// </summary>
    public event Func<MessageEvent, Task>? Message
    {
        add
        {
            if (value != null) MessageEvent.Add(value);
        }
        remove
        {
            if (value != null) MessageEvent.Remove(value);
        }
    }

    /// <summary>
    ///     Occurs when the line API sends a Follow event through the Webhook.
    /// </summary>
    public event Func<FollowEvent, Task>? Follow
    {
        add
        {
            if (value != null) FollowEvent.Add(value);
        }
        remove
        {
            if (value != null) FollowEvent.Remove(value);
        }
    }

    /// <summary>
    ///     Occurs when the line API sends an Unfollow event through the Webhook.
    /// </summary>
    public event Func<UnfollowEvent, Task>? Unfollow
    {
        add
        {
            if (value != null) UnfollowEvent.Add(value);
        }
        remove
        {
            if (value != null) UnfollowEvent.Remove(value);
        }
    }

    /// <summary>
    ///     Runs the events asynchronously.
    /// </summary>
    /// <param name="events">The events.</param>
    public async Task RunAsync(IEnumerable<WebhookEvent> events)
    {
        foreach (var ev in events)
        {
            switch (ev)
            {
                case MessageEvent message:
                    if (MessageEvent.HasSubscribers)
                        foreach (var subscription in MessageEvent.Subscriptions)
                            await subscription.Invoke(message);
                    break;

                case FollowEvent follow:
                    if (MessageEvent.HasSubscribers)
                        foreach (var subscription in FollowEvent.Subscriptions)
                            await subscription.Invoke(follow);
                    break;

                case UnfollowEvent unfollow:
                    if (MessageEvent.HasSubscribers)
                        foreach (var subscription in UnfollowEvent.Subscriptions)
                            await subscription.Invoke(unfollow);
                    break;
            }
        }
    }
}