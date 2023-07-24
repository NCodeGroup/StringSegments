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

namespace NCode.Buffers;

/// <summary>
/// Provides extensions methods to split a string into substrings based on a delimiter without any additional heap allocations.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">A character that delimits the substrings in the original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments SplitSegments(
        this string value,
        char separator) =>
        StringSegments.Split(value, separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments SplitSegments(
        this string value,
        ReadOnlySpan<char> separator) =>
        StringSegments.Split(value, separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the original string.</param>
    /// <param name="comparisonType">An enumeration that specifies the rules for the substring search.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments SplitSegments(
        this string value,
        ReadOnlySpan<char> separator,
        StringComparison comparisonType) =>
        StringSegments.Split(value, separator, comparisonType);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">A character that delimits the substrings in the original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments SplitSegments(
        this ReadOnlyMemory<char> value,
        char separator) =>
        StringSegments.Split(value, separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the original string.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments SplitSegments(
        this ReadOnlyMemory<char> value,
        ReadOnlySpan<char> separator) =>
        StringSegments.Split(value, separator);

    /// <summary>
    /// Splits a string into substrings based on a delimiter without any additional heap allocations.
    /// </summary>
    /// <param name="value">The string to split into substrings.</param>
    /// <param name="separator">The string that delimits the substrings in the original string.</param>
    /// <param name="comparisonType">An enumeration that specifies the rules for the substring search.</param>
    /// <returns>A <see cref="StringSegments"/> instance that contains the substrings from the string that are delimited
    /// by the separator.</returns>
    public static StringSegments SplitSegments(
        this ReadOnlyMemory<char> value,
        ReadOnlySpan<char> separator,
        StringComparison comparisonType) =>
        StringSegments.Split(value, separator, comparisonType);
}
