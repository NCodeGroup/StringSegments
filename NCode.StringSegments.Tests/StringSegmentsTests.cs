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
        Assert.Equal(count, segments.Count);
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

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(8)]
    [InlineData(13)]
    [InlineData(21)]
    public void Indexer_Valid(int count)
    {
        var index = 0;
        var first = new MemorySegment<char>(index.ToString().AsMemory());
        var last = first;
        while (++index < count)
        {
            last = last.Append(index.ToString().AsMemory());
        }

        var segments = new StringSegments("don't care".AsMemory(), count, first);

        Assert.Equal(count, segments.Count);
        for (index = 0; index < count; ++index)
        {
            Assert.Equal(index.ToString(), segments[index].Memory.ToString());
            if (index < count - 1)
            {
                var next = segments[index].Next;
                Assert.NotNull(next);
                Assert.Equal((index + 1).ToString(), next!.Memory.ToString());
            }
            else
            {
                Assert.Null(segments[index].Next);
            }
        }
    }

    [Fact]
    public void Split_SingleChar_Valid()
    {
        var segments = StringSegments.Split("1.2.3.4", '.');
        Assert.Equal("1.2.3.4", segments.Original.ToString());
        Assert.Equal(4, segments.Count);
        Assert.Equal("1", segments[0].Memory.ToString());
        Assert.Equal("2", segments[1].Memory.ToString());
        Assert.Equal("3", segments[2].Memory.ToString());
        Assert.Equal("4", segments[3].Memory.ToString());
    }

    [Fact]
    public void Split_MultiCharCaseInsensitive_Valid()
    {
        var segments = StringSegments.Split("1ab2Ab3aB4ab5", "ab", StringComparison.OrdinalIgnoreCase);
        Assert.Equal("1ab2Ab3aB4ab5", segments.Original.ToString());
        Assert.Equal(5, segments.Count);
        Assert.Equal("1", segments[0].Memory.ToString());
        Assert.Equal("2", segments[1].Memory.ToString());
        Assert.Equal("3", segments[2].Memory.ToString());
        Assert.Equal("4", segments[3].Memory.ToString());
        Assert.Equal("5", segments[4].Memory.ToString());
    }

    [Fact]
    public void Split_MultiCharCaseSensitive_Valid()
    {
        var segments = StringSegments.Split("1ab2Ab3aB4ab5", "ab", StringComparison.Ordinal);
        Assert.Equal("1ab2Ab3aB4ab5", segments.Original.ToString());
        Assert.Equal(3, segments.Count);
        Assert.Equal("1", segments[0].Memory.ToString());
        Assert.Equal("2Ab3aB4", segments[1].Memory.ToString());
        Assert.Equal("5", segments[2].Memory.ToString());
    }
}