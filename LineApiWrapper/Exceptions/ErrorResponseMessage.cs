// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LineApiWrapper.Exceptions;

/// <summary>
///     Error returned from LINE Platform https://developers.line.me/en/docs/messaging-api/reference/#error-responses
/// </summary>
public class ErrorResponseMessage
{
    /// <summary>
    ///     Summary of the error
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    ///     Details of the error
    /// </summary>
    public IList<ErrorDetails>? Details { get; set; }

    /// <summary>
    ///     Converts to string.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string? ToString()
    {
        return (Details == null) ? Message : $"{Message},[{string.Join(",", Details)}]";
    }

    /// <summary>
    ///     Details of the error
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        ///     Details of the error
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        ///     Location of where the error occurred
        /// </summary>
        public string? Property { get; set; }

        /// <summary>
        ///     Converts to string.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{{{Message}, {Property}}}";
        }
    }
}