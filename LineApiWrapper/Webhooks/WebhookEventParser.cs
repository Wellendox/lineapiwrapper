// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace LineApiWrapper.Webhooks;

/// <summary>
///     The WebhookEventParser
/// </summary>
public static class WebhookEventParser
{
    /// <summary>
    ///     Parses the specified webhook content.
    /// </summary>
    /// <param name="webhookContent">Content of the webhook.</param>
    /// <returns></returns>
    public static IEnumerable<WebhookEvent> Parse(string webhookContent)
    {
        dynamic dynamicObject = JsonConvert.DeserializeObject(webhookContent) ?? string.Empty;
        if (dynamicObject == null) { yield break; }

        foreach (var ev in dynamicObject.events)
        {
            var webhookEvent = WebhookEvent.CreateFrom(ev);
            if (webhookEvent == null) { continue; }

            yield return webhookEvent;
        }
    }

    /// <summary>
    ///     Parses the events.
    /// </summary>
    /// <param name="events">The events.</param>
    /// <returns></returns>
    public static IEnumerable<WebhookEvent> ParseEvents(dynamic events)
    {
        foreach (var ev in events)
        {
            var webhookEvent = WebhookEvent.CreateFrom(ev);
            if (webhookEvent == null) { continue; }

            yield return webhookEvent;
        }
    }
}