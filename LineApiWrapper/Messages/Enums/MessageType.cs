// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
namespace LineApiWrapper.Messages.Enums;

/// <summary>
///     The message type
/// </summary>
public enum MessageType
{
    Text = 0,
    Image = 1,
    Video = 2,
    Audio = 3,
    Location = 4,
    Sticker = 5,
    Imagemap = 6,
    Template = 7,
    File = 8,
    Flex = 9,
}