// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable SuggestBaseTypeForParameter
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace LineApiWrapper.Extensions;

public static class HttpExtensions
{
    /// <summary>
    /// Validate the response status.
    /// </summary>
    /// <param name="response">HttpResponseMessage</param>
    /// <returns>HttpResponseMessage</returns>
    internal static async Task<HttpResponseMessage> EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        var content = await response.Content.ReadAsStringAsync();
        throw new ApplicationException(content);
    }

    /// <summary>
    /// Verify if the request is valid, then returns LINE Webhook events from the request
    /// </summary>
    /// <param name="request">HttpRequestMessage</param>
    /// <param name="channelSecret">ChannelSecret</param>
    /// <param name="botUserId">BotUserId</param>
    /// <returns>List of WebhookEvent</returns>
    public static async Task<IEnumerable<WebhookEvent>> GetWebhookEventsAsync(
        this HttpRequest request,
        string channelSecret,
        string? botUserId = null)
    {
        if (request == null)
        {
            var ex = new ArgumentNullException(nameof(request));
            throw ex;
        }

        if (string.IsNullOrEmpty(channelSecret))
        {
            var ex = new ArgumentNullException(nameof(channelSecret));
            throw ex;
        }

        var content = await new StreamReader(request.Body).ReadToEndAsync();

        var xLineSignature =
            request.Headers.FirstOrDefault(header => header.Key == "X-Line-Signature").Value.ToString();
        if (string.IsNullOrEmpty(xLineSignature) || !VerifySignature(channelSecret, xLineSignature, content))
        {
            var ex = new InvalidSignatureException("Signature validation failed.");
            throw ex;
        }

        dynamic json = JsonConvert.DeserializeObject(content) ?? string.Empty;

        if (!string.IsNullOrEmpty(botUserId))
        {
            if (botUserId != json.destination as string)
            {
                var ex = new UserIdMismatchException("Bot user ID does not match.");
                throw ex;
            }
        }

        return WebhookEventParser.ParseEvents(json.events);
    }

    /// <summary>
    /// Verifies the signature.
    /// </summary>
    /// <param name="channelSecret">The channel secret.</param>
    /// <param name="xLineSignature">The x line signature.</param>
    /// <param name="requestBody">The request body.</param>
    /// <returns></returns>
    internal static bool VerifySignature(string channelSecret, string xLineSignature, string requestBody)
    {
        var key = Encoding.UTF8.GetBytes(channelSecret);
        var body = Encoding.UTF8.GetBytes(requestBody);

        using HMACSHA256 hmac = new(key);
        var hash = hmac.ComputeHash(body, 0, body.Length);
        var xLineBytes = Convert.FromBase64String(xLineSignature);
        return SlowEquals(xLineBytes, hash);
    }

    /// <summary>
    /// Compares two-byte arrays in length-constant time. This comparison method is used so that
    /// password hashes cannot be extracted from on-line systems using a timing attack and then
    /// attacked off-line.
    /// <para>http://bryanavery.co.uk/cryptography-net-avoiding-timing-attack/#comment-85　</para>
    /// </summary>
    private static bool SlowEquals(byte[] a, byte[] b)
    {
        uint diff = (uint)a.Length ^ (uint)b.Length;
        for (int i = 0; i < a.Length && i < b.Length; i++)
            diff |= (uint)(a[i] ^ b[i]);
        return diff == 0;
    }
}