// ReSharper disable UnusedMember.Global
namespace LineApiWrapper.Exceptions;

/// <summary>
///     Capture Invalid Signature Exception.
/// </summary>
public class InvalidSignatureException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidSignatureException" /> class.
    /// </summary>
    public InvalidSignatureException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidSignatureException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidSignatureException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidSignatureException" /> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    ///     The exception that is the cause of the current exception, or a null reference (
    ///     <see langword="Nothing" /> in Visual Basic) if no inner exception is specified.
    /// </param>
    public InvalidSignatureException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}