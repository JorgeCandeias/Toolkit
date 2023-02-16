using CommunityToolkit.Diagnostics;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Outcompute.Toolkit.Core.Tests.InteropServices;

public class MemoryMarshalExTests
{
    [Fact]
    public void GetArrayReturnsArray()
    {
        // arrange
        var array = new int[10];
        var memory = array.AsMemory();

        // act
        var result = MemoryMarshalEx.GetArray<int>(memory);

        // assert
        Assert.Same(array, result.Array);
    }

    [Fact]
    public unsafe void GetArrayThrowsOnNativeMemory()
    {
        // arrange
        var length = 10;
        using var buffer = NativeMemoryManager<int>.Allocate(length);

        // act
        var ex = Assert.Throws<InvalidOperationException>(() => MemoryMarshalEx.GetArray<int>(buffer.Memory));

        // assert
        Assert.Equal("Could not get an array segment from the specified memory block", ex.Message);
    }

    private sealed unsafe class NativeMemoryManager<T> : MemoryManager<T> where T : unmanaged
    {
        private readonly void* _pointer;
        private readonly int _length;

        private NativeMemoryManager(void* pointer, int length)
        {
            _pointer = pointer;
            _length = length;
        }

        private int _disposed;

        public override Span<T> GetSpan() => new(_pointer, _length);

        public override MemoryHandle Pin(int elementIndex = 0) => new(_pointer);

        public override void Unpin()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 0)
            {
                NativeMemory.Free(_pointer);
            }
        }

        public static NativeMemoryManager<T> Allocate(int length)
        {
            Guard.IsGreaterThanOrEqualTo(length, 0);

            var bytes = (nuint)(sizeof(T) * length);

            var pointer = NativeMemory.Alloc(bytes);

            return new NativeMemoryManager<T>(pointer, length);
        }
    }
}