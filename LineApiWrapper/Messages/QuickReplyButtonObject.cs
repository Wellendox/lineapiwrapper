// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable NotAccessedField.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace LineApiWrapper.Messages;

/// <summary>
///     This is a quick reply option that is displayed as a button.
///     https://developers.line.me/en/reference/messaging-api/#quick-reply-button-object
/// </summary>
public class QuickReplyButtonObject
{
    public string Type;

    /// <summary>
    ///     URL of the icon that is displayed at the beginning of the button
    ///     Max: 1000 characters URL scheme: https Image format: PNG Aspect ratio: 1:1 Data size: Up to
    ///     1 MB There is no limit on the image size. If the action property has a camera action, camera
    ///     roll action, or location action, and the imageUrl property is not set, the default icon is displayed.
    /// </summary>
    public string? ImageUrl { get; set; }

    public QuickReplyButtonObject(string type = "action", string? imageUrl = null)
    {
        Type = type;
        ImageUrl = imageUrl;
    }
}