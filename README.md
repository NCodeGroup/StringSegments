[![ci](https://github.com/NCodeGroup/StringSegments/actions/workflows/main.yml/badge.svg)](https://github.com/NCodeGroup/StringSegments/actions)

# NCode.StringSegments

Provides the ability to split a string into substrings based on a delimiter without any additional heap allocations.

## API

```csharp
namespace NCode.Buffers;

/// <summary>
/// Provides extensions methods to split a string into substrings based on a
/// delimiter without any additional heap allocations.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Splits a string into substrings based on a delimiter without any
    /// additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">A character that delimits the substrings in the
    /// original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the
    /// substrings from the string that are delimited by the separator.</returns>
    public static StringSegments SplitSegments(
        this string value,
        char separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any
    /// additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the
    /// original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the
    /// substrings from the string that are delimited by the separator.</returns>
    public static StringSegments SplitSegments(
        this string value,
        ReadOnlySpan<char> separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any
    /// additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the
    /// original string.</param>
    /// <param name="comparisonType">An enumeration that specifies the rules for
    /// the substring search.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the
    /// substrings from the string that are delimited by the separator.</returns>
    public static StringSegments SplitSegments(
        this string value,
        ReadOnlySpan<char> separator,
        StringComparison comparisonType);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any
    /// additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">A character that delimits the substrings in the
    /// original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the
    /// substrings from the string that are delimited by the separator.</returns>
    public static StringSegments SplitSegments(
        this ReadOnlyMemory<char> value,
        char separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any
    /// additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the
    /// original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the
    /// substrings from the string that are delimited by the separator.</returns>
    public static StringSegments SplitSegments(
        this ReadOnlyMemory<char> value,
        ReadOnlySpan<char> separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any
    /// additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the
    /// original string.</param>
    /// <param name="comparisonType">An enumeration that specifies the rules for
    /// the substring search.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the
    /// substrings from the string that are delimited by the separator.</returns>
    public static StringSegments SplitSegments(
        this ReadOnlyMemory<char> value,
        ReadOnlySpan<char> separator,
        StringComparison comparisonType);
}

/// <summary>
/// Provides the ability to split a string into substrings based on a delimiter
/// without any additional heap allocations.
/// </summary>
public class StringSegments : IReadOnlyCollection<ReadOnlySequenceSegment<char>>
{
    /// <summary>
    /// Gets the original string value.
    /// </summary>
    public ReadOnlyMemory<char> Original { get; }

    /// <summary>
    /// Gets the number of substrings.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Gets the first substring.
    /// </summary>
    public ReadOnlySequenceSegment<char> First { get; }

    /// <summary>
    /// Returns an enumerator that iterates over the collection of substrings. 
    /// </summary> 
    public IEnumerator<ReadOnlySequenceSegment<char>> GetEnumerator();
}
```

## Release Notes

* v1.0.0 - Initial release
* v1.0.1 - Added IReadOnlyList<> to StringSegments
* v2.0.0 - Revert IReadOnlyList<> to IReadOnlyCollection<>
* v2.0.1 - Updated readme
* v2.0.2 - Exposing MemorySegment as public
