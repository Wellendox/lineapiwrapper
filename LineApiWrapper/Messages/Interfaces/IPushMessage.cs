// ReSharper disable UnusedMemberInSuper.Global
namespace LineApiWrapper.Messages.Interfaces;

/// <summary>
/// </summary>
public interface IPushMessage
{
    /// <summary>
    ///     Gets where to send the push message to.
    /// </summary>
    /// <value>Where to send the message to.</value>
    string To { get; }

    /// <summary>
    ///     Gets the messages.
    /// </summary>
    /// <value>The messages.</value>
    IEnumerable<IMessage> Messages { get; }
}