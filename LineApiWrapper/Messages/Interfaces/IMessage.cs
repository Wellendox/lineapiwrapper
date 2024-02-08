namespace LineApiWrapper.Messages.Interfaces;

/// <summary>
/// The message
/// </summary>
public interface IMessage : IBaseMessage
{
    /// <summary>
    /// Message text
    /// </summary>
    /// <remarks>Max characters: <seealso cref="LineApiConfiguration.MaxMessageSize"/></remarks>
    string Text { get; }
}