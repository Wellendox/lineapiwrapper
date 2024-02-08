// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
namespace LineApiWrapper.Enums;

/// <summary>
///     The RetryMode used to validate whether to retry sending requests to the line API or not, and in
///     which intervals.
/// </summary>

//todo: Implementierung des RetryMode, ist bisher nicht im Einsatz
[Flags]
public enum RetryMode
{
    // <summary>
    // If a request fails, an exception is thrown immediately.
    // </summary>
    AlwaysFail = 1 << 0,

    /// <summary>
    ///     Retry if a request timed out.
    /// </summary>
    RetryTimeouts = 1 << 1,

    /// <summary>
    ///     Retry if a request failed due to a rate-limit.
    /// </summary>
    RetryRatelimit = 1 << 2,

    /// <summary>
    ///     Retry if a request failed due to an HTTP error 502.
    /// </summary>
    Retry502 = 1 << 3,

    /// <summary>
    ///     Continuously retry a request until it times out, its cancel token is triggered, or the
    ///     server responds with a non-502 error.
    /// </summary>
    AlwaysRetry = RetryTimeouts | RetryRatelimit | Retry502,
}