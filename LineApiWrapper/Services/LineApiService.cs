// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable RegionWithSingleElement
namespace LineApiWrapper.Services;

/// <summary>
///     The Line API Service
/// </summary>
/// <seealso cref="ILineApiService" />
public class LineApiService : ILineApiService
{
    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineApiService" /> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public LineApiService(ILineApiConfiguration configuration)
    {
        Configuration = configuration as LineApiConfiguration;
        _jsonSerializerSettings = new CamelCaseJsonSerializerSettings();
        RestHttpClient = new HttpClient();
        RestHttpClient.BaseAddress = new Uri(LineApiConfiguration.OtherUrl);
        RestHttpClient.DefaultRequestHeaders.Accept.Clear();
        RestHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        RestHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration.ChannelAccessToken}");

        //SignatureValidator = new SignatureValidator(configuration);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineApiService" /> class.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="configuration">The configuration.</param>
    public LineApiService(HttpClient client, ILineApiConfiguration configuration)
    {
        Configuration = configuration as LineApiConfiguration;
        RestHttpClient = client;
        if (RestHttpClient.BaseAddress == null)
            RestHttpClient.BaseAddress = new(LineApiConfiguration.OtherUrl);

        RestHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration.ChannelAccessToken}");
        _jsonSerializerSettings = new CamelCaseJsonSerializerSettings();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineApiService" /> class.
    /// </summary>
    /// <param name="client">The client.</param>
    public LineApiService(HttpClient client)
    {
        RestHttpClient = client;
        if (RestHttpClient.BaseAddress == null)
            RestHttpClient.BaseAddress = new(LineApiConfiguration.OtherUrl);

        RestHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Configuration?.ChannelAccessToken}");
        _jsonSerializerSettings = new CamelCaseJsonSerializerSettings();
    }

    #endregion Constructors

    #region Properties

    /// <inheritdoc />
    public HttpClient RestHttpClient { get; set; }

    /// <inheritdoc />
    public LineApiConfiguration? Configuration { get; set; }

    //public SignatureValidator SignatureValidator { get; set; }
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    #endregion Properties

    #region Methods

    #region User

    /// <inheritdoc />
    public virtual async Task<UserProfile?> GetUserProfileAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var response =
            await RestHttpClient.GetAsync($"{RestHttpClient.BaseAddress}/bot/profile/{userId}", cancellationToken);
        await response.EnsureSuccessStatusCodeAsync();

        var profileData = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!string.IsNullOrEmpty(profileData))
            return JsonConvert.DeserializeObject<UserProfile>(profileData);

        return null;
    }

    #endregion User

    #region Messaging

    /// <inheritdoc />
    public virtual async Task ReplyAsync(
        string replyToken,
        IList<IBaseMessage> messages,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = new HttpRequestMessage(HttpMethod.Post, $"{RestHttpClient.BaseAddress}/bot/message/reply");
        var content = JsonConvert.SerializeObject(new { replyToken, messages }, _jsonSerializerSettings);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await RestHttpClient.SendAsync(request, cancellationToken);
        await response.EnsureSuccessStatusCodeAsync();

        OnReplyMessage?.Invoke();
    }

    /// <inheritdoc />
    public virtual async Task ReplyWithJsonAsync(
        string replyToken,
        CancellationToken cancellationToken,
        params string[] messages)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = new HttpRequestMessage(HttpMethod.Post, $"{RestHttpClient.BaseAddress}/bot/message/reply");
        var json =
            $@"{{
    ""replyToken"" : ""{replyToken}"",
    ""messages"" : [{string.Join(", ", messages)}]
}}";
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await RestHttpClient.SendAsync(request, cancellationToken);
        await response.EnsureSuccessStatusCodeAsync();
        OnReplyMessage?.Invoke();
    }

    /// <inheritdoc />
    public virtual async Task PushAsync(string to, IList<IBaseMessage> messages, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = new HttpRequestMessage(HttpMethod.Post, $"{RestHttpClient.BaseAddress}/bot/message/push");
        var content = JsonConvert.SerializeObject(new { to, messages }, _jsonSerializerSettings);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await RestHttpClient.SendAsync(request, cancellationToken);
        await response.EnsureSuccessStatusCodeAsync();
        OnPushMessage?.Invoke();
    }

    /// <inheritdoc />
    public virtual async Task PushWithJsonAsync(
        string to,
        CancellationToken cancellationToken,
        params string[] messages)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = new HttpRequestMessage(HttpMethod.Post, $"{RestHttpClient.BaseAddress}/bot/message/push");
        var json =
            $@"{{
    ""to"" : ""{to}"",
    ""messages"" : [{string.Join(", ", messages)}]
}}";
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await RestHttpClient.SendAsync(request, cancellationToken);
        await response.EnsureSuccessStatusCodeAsync();
        OnPushMessage?.Invoke();
    }

    #endregion Messaging

    #endregion Methods

    #region Events

    public delegate void ReplyMessage();

    public delegate void PushMessage();

    public event ReplyMessage? OnReplyMessage;

    public event PushMessage? OnPushMessage;

    #endregion Events
}