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

namespace NCode.Buffers.Tests;

public class StringSegmentsTests
{
    [Fact]
    public void Original_Valid()
    {
        const int count = 1;
        var original = "original".AsMemory();
        var first = new MemorySegment<char>(original);
        var segments = new StringSegments(original, count, first);
        Assert.Equal(original, segments.Original);
    }

    [Fact]
    public void Count_Valid()
    {
        const int count = 1;
        var original = "original".AsMemory();
        var first = new MemorySegment<char>(original);
        var segments = new StringSegments(original, count, first);
        Assert.Equal(new[] { first }, segments);
    }

    [Fact]
    public void First_Valid()
    {
        const int count = 1;
        var original = "original".AsMemory();
        var first = new MemorySegment<char>(original);
        var segments = new StringSegments(original, count, first);
        Assert.Same(first, segments.First);
    }

    [Fact]
    public void GetEnumerator_Valid()
    {
        var segments = StringSegments.Split("1.2.3.4", '.');
        Assert.Equal(segments.Select(segment => segment.Memory.ToString()), new[] { "1", "2", "3", "4" });
    }

    [Fact]
    public void Split_SingleChar_Valid()
    {
        var segments = StringSegments.Split("1.2.3.4", '.');
        Assert.Equal("1.2.3.4", segments.Original.ToString());
        Assert.Equal(4, segments.Count);

        var list = segments.ToList();
        Assert.Equal("1", list[0].Memory.ToString());
        Assert.Equal("2", list[1].Memory.ToString());
        Assert.Equal("3", list[2].Memory.ToString());
        Assert.Equal("4", list[3].Memory.ToString());
    }

    [Fact]
    public void Split_MultiCharCaseInsensitive_Valid()
    {
        var segments = StringSegments.Split("1ab2Ab3aB4ab5", "ab", StringComparison.OrdinalIgnoreCase);
        Assert.Equal("1ab2Ab3aB4ab5", segments.Original.ToString());
        Assert.Equal(5, segments.Count);

        var list = segments.ToList();
        Assert.Equal("1", list[0].Memory.ToString());
        Assert.Equal("2", list[1].Memory.ToString());
        Assert.Equal("3", list[2].Memory.ToString());
        Assert.Equal("4", list[3].Memory.ToString());
        Assert.Equal("5", list[4].Memory.ToString());
    }

    [Fact]
    public void Split_MultiCharCaseSensitive_Valid()
    {
        var segments = StringSegments.Split("1ab2Ab3aB4ab5", "ab", StringComparison.Ordinal);
        Assert.Equal("1ab2Ab3aB4ab5", segments.Original.ToString());
        Assert.Equal(3, segments.Count);

        var list = segments.ToList();
        Assert.Equal("1", list[0].Memory.ToString());
        Assert.Equal("2Ab3aB4", list[1].Memory.ToString());
        Assert.Equal("5", list[2].Memory.ToString());
    }
}