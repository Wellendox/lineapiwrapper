// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace LineApiWrapperTests;

/// <summary>
/// NUnit Tests for the HttpResponseMessageExtension Methods
/// </summary>
public class HttpExtensionsTests
{
    /// <summary>
    /// Ensures the success status code asynchronous test.
    /// </summary>
    [Fact]
    public async Task EnsureSuccessStatusCodeTestAsync()
    {
        HttpResponseMessage message = new(HttpStatusCode.Conflict);
        await Assert.ThrowsAsync<ApplicationException>(message.EnsureSuccessStatusCodeAsync);
    }

    /// <summary>
    /// Gets the webhook events asynchronous test.
    /// </summary>
    [Fact]
    [SuppressMessage("ReSharper", "MethodHasAsyncOverload")]
    public async Task GetWebhookEventsTestAsync()
    {
        //Null Request
        HttpRequest nullRequest = Substitute.For<HttpRequest>();

        //Request 1
        HttpRequest request = Substitute.For<HttpRequest>();
        StringContent requestBody = new(string.Empty, Encoding.UTF8, "application/json");
        HeaderDictionary request1Headers = new() { { "X-Line-Signature", string.Empty } };
        request.Body.Returns(await requestBody.ReadAsStreamAsync());
        request.Headers.Returns(request1Headers);

        //Signature Hash for Request 2
        Dictionary<string, string> content = new() { { "destination", string.Empty } };
        string contentJson = JsonConvert.SerializeObject(content);
        const string channelSecret = "123456";
        byte[] key = Encoding.UTF8.GetBytes(channelSecret);
        byte[] body = Encoding.UTF8.GetBytes(contentJson);

        HMACSHA256 hmac = new(key);
        byte[] hash = hmac.ComputeHash(body, 0, body.Length);

        HttpRequest request2 = Substitute.For<HttpRequest>();
        HeaderDictionary request2Headers = new() { { "X-Line-Signature", Convert.ToBase64String(hash) } };
        requestBody = new StringContent(contentJson, Encoding.UTF8, "application/json");
        request2.Body.Returns(requestBody.ReadAsStream());
        request2.Headers.Returns(request2Headers);

        //Request Null Test
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await nullRequest.GetWebhookEventsAsync("Test", "test"));

        //Request 1 Tests
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await request.GetWebhookEventsAsync(string.Empty));
        await Assert.ThrowsAsync<InvalidSignatureException>(
            async () => await request.GetWebhookEventsAsync("123456", "1337"));

        //Request 2 Test
        await Assert.ThrowsAsync<UserIdMismatchException>(async () =>
            await request2.GetWebhookEventsAsync("123456", "5552332"));
    }
}