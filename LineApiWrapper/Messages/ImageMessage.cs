// ReSharper disable InvalidXmlDocComment
// ReSharper disable UnusedType.Global
// ReSharper disable RegionWithSingleElement
namespace LineApiWrapper.Messages;

/// <summary>
///     Image
/// </summary>
/// <seealso cref="https://developers.line.biz/en/reference/messaging-api/#image-message" />
public sealed class ImageMessage : IImageMessage
{
    #region ctor

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="originalContentUrl">
    ///     Image URL (Max: 1000 characters) HTTPS JPEG
    ///     Max: 1024 x 1024
    ///     Max: 1 MB
    /// </param>
    /// <param name="previewImageUrl">
    ///     Preview image URL (Max: 1000 characters) HTTPS JPEG
    ///     Max: 240 x 240
    ///     Max: 1 MB
    /// </param>
    /// <param name="quickReply">QuickReply</param>
    public ImageMessage(string originalContentUrl, string previewImageUrl, QuickReply? quickReply = null)
    {
        OriginalContentUrl = originalContentUrl;
        PreviewImageUrl = previewImageUrl;
        QuickReply = quickReply;
    }

    #endregion ctor

    #region Properties

    /// <inheritdoc />
    public MessageType Type => MessageType.Image;

    /// <inheritdoc />
    public QuickReply? QuickReply { get; set; }

    /// <inheritdoc />
    public string OriginalContentUrl { get; }

    /// <inheritdoc />
    public string PreviewImageUrl { get; }

    #endregion Properties
}