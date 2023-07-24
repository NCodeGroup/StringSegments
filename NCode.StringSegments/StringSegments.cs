#region Copyright Preamble

//
//    Copyright @ 2023 NCode Group
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

#endregion

using System.Buffers;
using System.Collections;

namespace NCode.Buffers;

/// <summary>
/// Provides the ability to split a string into substrings based on a delimiter without any additional heap allocations.
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
    /// Initializes a new instance of the <see cref="StringSegments"/> class.
    /// </summary>
    /// <param name="original">The original string value.</param>
    /// <param name="count">The number of segments.</param>
    /// <param name="first">The first segment in the string value.</param>
    public StringSegments(ReadOnlyMemory<char> original, int count, ReadOnlySequenceSegment<char> first)
    {
        Original = original;
        Count = count;
        First = first;
    }

    /// <inheritdoc/>
    public IEnumerator<ReadOnlySequenceSegment<char>> GetEnumerator()
    {
        for (var iter = First; iter != null; iter = iter.Next)
        {
            yield return iter;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">A character that delimits the substrings in the original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments Split(string value, char separator)
        => Split(value.AsMemory(), separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">A character that delimits the substrings in the original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments Split(ReadOnlyMemory<char> value, char separator)
    {
        var count = 1;
        var index = value.Span.IndexOf(separator);
        if (index == -1)
        {
            return new StringSegments(value, count, new MemorySegment<char>(value));
        }

        var first = new MemorySegment<char>(value[..index]);
        var last = first;
        var offset = index + 1;

        while (true)
        {
            ++count;

            index = value.Span[offset..].IndexOf(separator);
            if (index == -1)
            {
                last.Append(value[offset..]);
                return new StringSegments(value, count, first);
            }

            index += offset;
            last = last.Append(value.Slice(offset, index - offset));
            offset = index + 1;
        }
    }

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments Split(string value, ReadOnlySpan<char> separator)
        => Split(value.AsMemory(), separator, StringComparison.Ordinal);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the original string.</param>
    /// <param name="comparisonType">An enumeration that specifies the rules for the substring search.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments Split(string value, ReadOnlySpan<char> separator, StringComparison comparisonType)
        => Split(value.AsMemory(), separator, comparisonType);


    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments Split(ReadOnlyMemory<char> value, ReadOnlySpan<char> separator) =>
        Split(value, separator, StringComparison.Ordinal);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the original string.</param>
    /// <param name="comparisonType">An enumeration that specifies the rules for the substring search.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments Split(
        ReadOnlyMemory<char> value,
        ReadOnlySpan<char> separator,
        StringComparison comparisonType)
    {
        var count = 1;
        var index = value.Span.IndexOf(separator, comparisonType);
        if (index == -1)
        {
            return new StringSegments(value, count, new MemorySegment<char>(value));
        }

        var remaining = value[(index + separator.Length)..];
        var first = new MemorySegment<char>(value[..index]);
        var last = first;

        while (true)
        {
            ++count;

            index = remaining.Span.IndexOf(separator, comparisonType);
            if (index == -1)
            {
                last.Append(remaining);
                return new StringSegments(value, count, first);
            }

            last = last.Append(remaining[..index]);
            remaining = remaining[(index + separator.Length)..];
        }
    }
}