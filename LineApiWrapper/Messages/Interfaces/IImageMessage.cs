// ReSharper disable UnusedMemberInSuper.Global
namespace LineApiWrapper.Messages.Interfaces;

internal interface IImageMessage : IBaseMessage
{
    /// <summary>
    ///     Image URL (Max: 1000 characters) HTTPS JPEG
    ///     Max: 1024 x 1024
    ///     Max: 1 MB
    /// </summary>
    public string OriginalContentUrl { get; }

    /// <summary>
    ///     Preview image URL (Max: 1000 characters) HTTPS JPEG
    ///     Max: 240 x 240
    ///     Max: 1 MB
    /// </summary>
    public string PreviewImageUrl { get; }
}