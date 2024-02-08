// ReSharper disable UnusedType.Global
// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace LineApiWrapper.Extensions;

/// <summary>
/// </summary>
internal static class StreamExtensions
{
    /// <summary>
    ///     The buffer size
    /// </summary>
    private const int BufferSize = 8192;

    /// <summary>
    ///     The UTF8 preamable
    /// </summary>
    private static readonly byte[] Utf8Preamable = Encoding.UTF8.GetPreamble();

    /// <summary>
    ///     Converts to arrayasync.
    /// </summary>
    /// <param name="self">The self.</param>
    /// <returns></returns>
    public static Task<byte[]?> ToArrayAsync(this Stream self)
    {
        return self.CanSeek ? ToArrayWithSeekableStreamAsync(self) : ToArrayWithNonSeekableStreamAsync(self);
    }

    /// <summary>
    ///     Converts to arraywithseekablestream.
    /// </summary>
    /// <param name="self">The self.</param>
    /// <returns></returns>
    private static async Task<byte[]?> ToArrayWithSeekableStreamAsync(Stream self)
    {
        if (self.Length == 0)
            return null;

        var buffer = new byte[self.Length];

        if (buffer.Length < Utf8Preamable.Length)
        {
            // ReSharper disable once MustUseReturnValue
            await self.ReadAsync(buffer, 0, buffer.Length);
            return buffer;
        }

        var offset = 0;
        var remaining = buffer.Length;

        var length = await self.ReadAsync(buffer, 0, Utf8Preamable.Length);
        remaining -= length;

        /* Ignore the UTF8 Preamable */
        if (IsUtf8Preamable(buffer))
            Array.Resize(ref buffer, buffer.Length - Utf8Preamable.Length);
        else
            offset += length;

        while (remaining > 0)
        {
            length = await self.ReadAsync(buffer, offset, Math.Min(remaining, 8192));

            remaining -= length;
            offset += length;
        }

        return buffer;
    }

    /// <summary>
    ///     Converts to arraywithnonseekablestream.
    /// </summary>
    /// <param name="self">The self.</param>
    /// <returns></returns>
    private static async Task<byte[]?> ToArrayWithNonSeekableStreamAsync(Stream self)
    {
        var buffer = new byte[BufferSize];
        using var memStream = new MemoryStream();
        var length = await self.ReadAsync(buffer, 0, Utf8Preamable.Length);
        if (length == 0)
            return null;

        /* Ignore the UTF8 Preamable */
        if (!IsUtf8Preamable(buffer))
            await memStream.WriteAsync(buffer, 0, length);

        while ((length = await self.ReadAsync(buffer, 0, BufferSize)) != 0)
        {
            await memStream.WriteAsync(buffer, 0, length);
        }

        return await memStream.ToArrayAsync();
    }

    /// <summary>
    ///     Determines whether [is UTF8 preamable] [the specified content].
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns><c>true</c> if [is UTF8 preamable] [the specified content]; otherwise, <c>false</c>.</returns>
    private static bool IsUtf8Preamable(IReadOnlyList<byte> content)
    {
        for (var i = 0; i < Utf8Preamable.Length; i++)
        {
            if (content[i] != Utf8Preamable[i])
                return false;
        }

        return true;
    }
}