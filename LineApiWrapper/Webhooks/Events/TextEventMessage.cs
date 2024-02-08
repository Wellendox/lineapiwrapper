// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace LineApiWrapper.Webhooks.Events;

/// <summary>
///     Message object which contains the text sent from the source.
/// </summary>
public class TextEventMessage : EventMessage
{
    /// <summary>
    ///     Gets the text.
    /// </summary>
    /// <value>The text.</value>
    public string Text { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TextEventMessage" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="text">The text.</param>
    public TextEventMessage(string id, string text)
        : base(EventMessageType.Text, id)
    {
        Text = text;
    }
}