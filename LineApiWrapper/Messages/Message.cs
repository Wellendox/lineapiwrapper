// ReSharper disable UnusedMember.Global
using System.Diagnostics.CodeAnalysis;

// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global

// ReSharper disable MemberCanBePrivate.Global

namespace LineApiWrapper.Messages;

/// <summary>
///     https://developers.line.me/en/docs/messaging-api/reference/#text
/// </summary>
public sealed class TextMessage : IMessage
{
    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="TextMessage" /> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="text">The text.</param>
    /// <param name="quickReply"></param>
    public TextMessage(MessageType type, string text, QuickReply? quickReply = null)
    {
        Type = type;
        Text = text;
        QuickReply = quickReply;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TextMessage" /> class.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="quickReply"></param>
    public TextMessage(string text, QuickReply? quickReply = null)
    {
        Text = text;
        QuickReply = quickReply;
    }

    #endregion Constructors

    #region Properties

    /// <inheritdoc />
    [JsonProperty("type")]
    [JsonConverter(typeof(EnumConverter<MessageType>))]
    public MessageType Type { get; set; } = MessageType.Text;

    /// <inheritdoc />
    public QuickReply? QuickReply { get; set; }

    /// <inheritdoc />
    [JsonProperty("text")]
    public string Text { get; set; }

    #endregion Properties

    #region Methods

    /// <summary>
    ///     Validates the messages.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">
    ///     The maximum number of messages is {LineApiConfiguration.MaxMessagesInRequest}. or An error
    ///     during message validation occurred.
    /// </exception>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static bool ValidateMessages(IEnumerable<IMessage> messages)
    {
        if (messages.Count() > LineApiConfiguration.MaxMessagesInRequest)
            throw new InvalidOperationException(
                $"The maximum number of messages is {LineApiConfiguration.MaxMessagesInRequest}.");

        foreach (var message in messages)
            if (!ValidateMessage(message))
                throw new InvalidOperationException("An error during message validation occurred.");

        return true;
    }

    /// <summary>
    ///     Validates the message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">
    ///     The message should not be null. or The text cannot be null or empty. or You cannot exceed
    ///     the maximum of {LineApiConfiguration.MaxMessageSize} characters in a message text.
    /// </exception>
    public static bool ValidateMessage(IMessage message)
    {
        if (message == null)
            throw new InvalidOperationException("The message should not be null.");

        if (string.IsNullOrWhiteSpace(message.Text))
            throw new InvalidOperationException("The text cannot be null or empty.");

        if (message.Text.Length > LineApiConfiguration.MaxMessageSize)
            throw new InvalidOperationException(
                $"You cannot exceed the maximum of {LineApiConfiguration.MaxMessageSize} characters in a message text.");

        return true;
    }

    #endregion Methods
}