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

public class MemorySegmentTests
{
    [Fact]
    public void Append_Valid()
    {
        var firstSource = "first".AsMemory();
        var firstSegment = new MemorySegment<char>(firstSource);
        Assert.Null(firstSegment.Next);
        Assert.Equal(firstSource, firstSegment.Memory);
        Assert.Equal(0, firstSegment.RunningIndex);

        var secondSource = "source".AsMemory();
        var secondSegment = firstSegment.Append(secondSource);
        Assert.Equal(secondSegment, firstSegment.Next);
        Assert.Null(secondSegment.Next);
        Assert.Equal(secondSource, secondSegment.Memory);
        Assert.Equal(firstSource.Length, secondSegment.RunningIndex);
    }
}