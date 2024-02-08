// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
using System.Diagnostics.CodeAnalysis;

// ReSharper disable ConvertConstructorToMemberInitializers

// ReSharper disable UnusedType.Global

// ReSharper disable UnusedMember.Local

// ReSharper disable UnusedMember.Global

// ReSharper disable UnusedParameter.Global

namespace LineApiWrapper.Helpers;

[SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
internal class AsyncEvent<T>
    where T : class
{
    private readonly object _subLock = new();
    internal ImmutableArray<T> _subscriptions;

    public bool HasSubscribers => _subscriptions.Length != 0;
    public IReadOnlyList<T> Subscriptions => _subscriptions;

    public AsyncEvent()
    {
        _subscriptions = ImmutableArray.Create<T>();
    }

    public void Add(T subscriber)
    {
        NotNull(subscriber, nameof(subscriber));
        lock (_subLock)
            _subscriptions = _subscriptions.Add(subscriber);
    }

    public void Remove(T subscriber)
    {
        NotNull(subscriber, nameof(subscriber));
        lock (_subLock)
            _subscriptions = _subscriptions.Remove(subscriber);
    }

    /// <exception cref="ArgumentNullException"><paramref name="obj" /> must not be <see langword="null" />.</exception>
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
    public static void NotNull<T>(T obj, string name, string? msg = null) where T : class
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
    {
        if (obj == null) throw new NullReferenceException(msg);
    }
}

//Based on https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Nullable.cs
public readonly struct Optional<T>
{
    public static Optional<T> Unspecified => default;
    private readonly T _value;

    /// <summary>
    ///     Gets the value for this parameter.
    /// </summary>
    /// <exception cref="InvalidOperationException" accessor="get">
    ///     This property has no value set.
    /// </exception>
    public T Value
    {
        get
        {
            if (!IsSpecified)
                throw new InvalidOperationException("This property has no value set.");
            return _value;
        }
    }

    /// <summary>
    ///     Returns true if this value has been specified.
    /// </summary>
    public bool IsSpecified { get; }

    /// <summary>
    ///     Creates a new Parameter with the provided value.
    /// </summary>
    public Optional(T value)
    {
        _value = value;
        IsSpecified = true;
    }

    public T GetValueOrDefault() => _value;

    public T GetValueOrDefault(T defaultValue) => IsSpecified ? _value : defaultValue;

    public override bool Equals(object? other)
    {
        if (!IsSpecified) return other == null;
        if (other == null) return false;
        return _value != null && _value.Equals(other);
    }

    public override int GetHashCode() => IsSpecified ? _value!.GetHashCode() : 0;

    public override string? ToString() => IsSpecified ? _value?.ToString() : null;

    private string DebuggerDisplay => IsSpecified ? _value?.ToString() ?? "<null>" : "<unspecified>";

    public static implicit operator Optional<T>(T value) => new(value);

    public static explicit operator T(Optional<T> value) => value.Value;
}

public static class Optional
{
    public static Optional<T> Create<T>() => Optional<T>.Unspecified;

    public static Optional<T> Create<T>(T value) => new(value);

    public static T? ToNullable<T>(this Optional<T> val)
        where T : struct
        => val.IsSpecified ? val.Value : null;
}