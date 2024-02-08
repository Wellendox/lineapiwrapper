// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace LineApiWrapper.Helpers;

/// <summary>
/// </summary>
/// <typeparam name="TEnum">The type of the enum.</typeparam>
/// <seealso cref="Newtonsoft.Json.JsonConverter" />
internal sealed class EnumConverter<TEnum> : JsonConverter
    where TEnum : struct, Enum
{
    /// <summary>
    ///     The lowercase
    /// </summary>
    private readonly bool _lowercase;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EnumConverter{TEnum}" /> class.
    /// </summary>
    public EnumConverter()
        : this(true)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="EnumConverter{TEnum}" /> class.
    /// </summary>
    /// <param name="lowercase">if set to <c>true</c> [lowercase].</param>
    public EnumConverter(bool lowercase)
        => _lowercase = lowercase;

    /// <summary>
    ///     Determines whether this instance can convert the specified object type.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <returns>
    ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanConvert(Type objectType)
        => objectType.GetTypeInfo().IsEnum;

    /// <summary>
    ///     Reads the JSON representation of the object.
    /// </summary>
    /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="existingValue">The existing value of object being read.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <returns>The object value.</returns>
    /// <exception cref="System.InvalidOperationException">Only string is supported.</exception>
    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.String)
            throw new InvalidOperationException("Only string is supported.");

        if (reader.Value is null)
            return default(TEnum);

        if (!Enum.TryParse((string)reader.Value, true, out TEnum result))
            result = default;

        return result;
    }

    /// <summary>
    ///     Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
    /// <param name="value">The value.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <exception cref="System.InvalidOperationException">Value cannot be null</exception>
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is null)
            throw new InvalidOperationException("Value cannot be null");

        var val = (Enum)value;

        var strValue = val.ToString("G");
        if (_lowercase)
        {
            strValue = strValue.ToLowerInvariant();
        }
        else
        {
            var characters = strValue.ToCharArray();
            characters[0] = char.ToLowerInvariant(characters[0]);
            strValue = new string(characters);
        }

        writer.WriteValue(strValue);
    }
}