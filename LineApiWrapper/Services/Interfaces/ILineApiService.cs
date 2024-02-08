// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global
namespace LineApiWrapper.Services.Interfaces;

/// <summary>
///     The interfaces of the Line API Service
/// </summary>
public interface ILineApiService
{
    /// <summary>
    ///     Gets or sets the rest HTTP client.
    /// </summary>
    /// <value>The rest HTTP client.</value>
    HttpClient RestHttpClient { get; set; }

    /// <summary>
    ///     Gets or sets the configuration.
    /// </summary>
    /// <value>The configuration.</value>
    LineApiConfiguration? Configuration { get; set; }

    /// <summary>
    ///     Gets the user profile.
    /// </summary>
    /// <remarks>Throws OperationCanceledException when given a CancellationToken</remarks>
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>UserProfile, or null if unable to deserialize profile.</returns>
    Task<UserProfile?> GetUserProfileAsync(string userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Respond to events from users, groups, and rooms
    ///     https://developers.line.me/en/docs/messaging-api/reference/#send-reply-message
    /// </summary>
    /// <param name="replyToken">ReplyToken</param>
    /// <param name="messages">Reply messages. Up to 5 messages.</param>
    /// <param name="cancellationToken"></param>
    Task ReplyAsync(string replyToken, IList<IBaseMessage> messages, CancellationToken cancellationToken);

    /// <summary>
    ///     Respond to events from users, groups, and rooms
    ///     https://developers.line.me/en/docs/messaging-api/reference/#send-reply-message
    /// </summary>
    /// <param name="replyToken">ReplyToken</param>
    /// <param name="cancellationToken"></param>
    /// <param name="messages">Set reply messages with Json string.</param>
    Task ReplyWithJsonAsync(string replyToken, CancellationToken cancellationToken, params string[] messages);

    /// <summary>
    ///     Send messages to a user, group, or room at any time.
    ///     Note: Use of push messages are limited to certain plans.
    /// </summary>
    /// <param name="to">ID of the receiver</param>
    /// <param name="messages">Reply messages. Up to 5 messages.</param>
    /// <param name="cancellationToken"></param>
    Task PushAsync(string to, IList<IBaseMessage> messages, CancellationToken cancellationToken);

    /// <summary>
    ///     Send messages to a user, group, or room at any time.
    ///     Note: Use of push messages are limited to certain plans.
    /// </summary>
    /// <param name="to">ID of the receiver</param>
    /// <param name="cancellationToken"></param>
    /// <param name="messages">Set reply messages with Json string.</param>
    Task PushWithJsonAsync(string to, CancellationToken cancellationToken, params string[] messages);
}