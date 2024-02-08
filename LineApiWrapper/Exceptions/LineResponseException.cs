// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LineApiWrapper.Exceptions;

/// <summary>
///     Capture Error from LINE platform
/// </summary>
public class LineResponseException : Exception
{
    /// <summary>
    ///     HTTP Status Code
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    ///     Error returned from LINE Platform
    /// </summary>
    public ErrorResponseMessage? ResponseMessage { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineResponseException" /> class.
    /// </summary>
    public LineResponseException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineResponseException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public LineResponseException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineResponseException" /> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    ///     The exception that is the cause of the current exception, or a null reference (
    ///     <see langword="Nothing" /> in Visual Basic) if no inner exception is specified.
    /// </param>
    public LineResponseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///     Converts to string.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
        return $"StatusCode={StatusCode}, {ResponseMessage}" + Environment.NewLine
                                                             + base.ToString();
    }
}