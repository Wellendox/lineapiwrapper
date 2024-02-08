// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
// ReSharper disable ConvertSwitchStatementToSwitchExpression
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace LineApiWrapper.Webhooks.Events;

/// <summary>
///     Contents of the message
/// </summary>
public class EventMessage
{
    /// <summary>
    ///     Message ID
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     EventMessageType
    /// </summary>
    public EventMessageType Type { get; set; }

    public EventMessage(EventMessageType type, string id)
    {
        Type = type;
        Id = id;
    }

    /// <summary>
    ///     Creates from.
    /// </summary>
    /// <param name="dynamicObject">The dynamic object.</param>
    /// <returns></returns>
    internal static EventMessage? CreateFrom(dynamic dynamicObject)
    {
        var message = dynamicObject?.message;
        if (message == null) { return null; }

        if (!Enum.TryParse((string)message.type, true, out EventMessageType messageType))
        {
            return null;
        }

        switch (messageType)
        {
            case EventMessageType.Text:
                return new TextEventMessage((string)message.id, (string)message.text);

            default:
                return null;
        }
    }
}