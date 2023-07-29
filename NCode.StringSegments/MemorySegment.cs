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

namespace NCode.Buffers;

/// <summary>
/// Provides an implementation of <see cref="ReadOnlySequenceSegment{T}"/> using a linked list of <see cref="ReadOnlyMemory{T}"/> nodes.
/// </summary>
public class MemorySegment<T> : ReadOnlySequenceSegment<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemorySegment{T}"/> class.
    /// </summary>
    /// <param name="memory">The block of memory for this node.</param>
    public MemorySegment(ReadOnlyMemory<T> memory)
    {
        Memory = memory;
    }

    /// <summary>
    /// Appends a block of memory to the end of the current node.
    /// </summary>
    /// <param name="memory">The block of memory to append to the current node.</param>
    /// <returns>The newly added node in the linked list.</returns>
    public MemorySegment<T> Append(ReadOnlyMemory<T> memory)
    {
        var next = new MemorySegment<T>(memory)
        {
            RunningIndex = RunningIndex + Memory.Length
        };

        Next = next;

        return next;
    }
}
