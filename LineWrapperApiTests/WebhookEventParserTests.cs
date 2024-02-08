// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
namespace LineApiWrapperTests;

/// <summary>
///     NUnit Tests for the LineApiWrapper
/// </summary>
public class WebhookEventParserTests
{
    /// <summary>
    ///     Tests the parsing of webhook events sent by the line API. It verifies whether the parser is
    ///     able to find and seperate the events from eachother, since line sends their events in bulk.
    ///     There can be text events mixed with follow events and so on.
    /// </summary>
    [Fact]
    public void ParseTest()
    {
        var fixture = new Fixture();
        var textEvent = fixture.Create<TextEventMessage>();
        var messageEvent = fixture.Build<MessageEvent>()
                                  .With(x => x.Message, textEvent)
                                  .Create();

        var text = messageEvent.Message as TextEventMessage;
        string json =
            $@"{{
    ""events"": [
        {{
            ""replyToken"": ""{messageEvent.ReplyToken}"",
            ""type"": ""{messageEvent.Type}"",
            ""timestamp"": {messageEvent.Timestamp},
            ""source"": {{
                 ""type"": ""{messageEvent.Source.Type}"",
                ""userId"": ""{messageEvent.Source.UserId}""
             }},
             ""message"": {{
                 ""id"": ""{text?.Id}"",
                 ""type"": ""{text?.Type}"",
                 ""text"": ""{text?.Text}""
            }}
        }}
    ]
}}";
        var expected = WebhookEventParser.Parse(json).ToList().FirstOrDefault() as MessageEvent;
        messageEvent.Should().BeEquivalentTo(expected);
    }
}