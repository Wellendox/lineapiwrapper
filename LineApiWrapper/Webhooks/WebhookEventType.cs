// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
namespace LineApiWrapper.Webhooks;

/// <summary>
///     Webhook Event Type
/// </summary>
public enum WebhookEventType
{
    Message = 0,
    Follow = 1,
    Unfollow = 2,

    //Join = 3,
    //Leave = 4,
    //Postback = 5,
    //Beacon = 6,
    //AccountLink = 7,
    //MemberJoined = 8,
    //MemberLeft = 9,
    //Things = 10
}