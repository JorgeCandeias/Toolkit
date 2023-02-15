using AutoMapper;
using CommunityToolkit.HighPerformance.Buffers;
using Outcompute.Toolkit.HighPerformance.Extensions;

namespace Outcompute.Toolkit.AutoMapper.Tests;

public class MemoryOwnerTypeConverterTests
{
    [Fact]
    public void ConvertsIEnumerableToIMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        var source = Enumerable.Range(1, 1000);

        // act
        var result = mapper.Map<IEnumerable<int>, IMemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ArrayPoolBufferWriter<string>>(result);
        Assert.Equal(source.Select(x => x.ToString()), result.Memory.Span.ToArray());
    }

    [Fact]
    public void ConvertsIEnumerableToMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        var source = Enumerable.Range(1, 1000);

        // act
        var result = mapper.Map<IEnumerable<int>, MemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MemoryOwner<string>>(result);
        Assert.Equal(source.Select(x => x.ToString()), result.Span.ToArray());
    }

    [Fact]
    public void ConvertsNullIEnumerableToMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        var source = (IEnumerable<int>)null!;

        // act
        var result = mapper.Map<IEnumerable<int>, MemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MemoryOwner<string>>(result);
        Assert.Equal(0, result.Length);
    }

    [Fact]
    public void ConvertsIEnumerableToArrayPoolBufferWriter()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        var source = Enumerable.Range(1, 1000);

        // act
        var result = mapper.Map<IEnumerable<int>, ArrayPoolBufferWriter<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ArrayPoolBufferWriter<string>>(result);
        Assert.Equal(source.Select(x => x.ToString()), result.WrittenSpan.ToArray());
    }

    [Fact]
    public void ConvertsNullIEnumerableToArrayPoolBufferWriter()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        var source = (IEnumerable<int>)null!;

        // act
        var result = mapper.Map<IEnumerable<int>, ArrayPoolBufferWriter<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ArrayPoolBufferWriter<string>>(result);
        Assert.Equal(0, result.WrittenCount);
    }

    [Fact]
    public void ConvertsIMemoryOwnerToIEnumerable()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToMemoryOwner();

        // act
        var result = mapper.Map<IMemoryOwner<int>, IEnumerable<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<string[]>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result);
    }

    [Fact]
    public void ConvertsNullIMemoryOwnerToIEnumerable()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = (IMemoryOwner<int>)null!;

        // act
        var result = mapper.Map<IMemoryOwner<int>, IEnumerable<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ConvertsIMemoryOwnerToMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToMemoryOwner();

        // act
        using var result = mapper.Map<IMemoryOwner<int>, MemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MemoryOwner<string>>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result.AsEnumerable());
    }

    [Fact]
    public void ConvertsNullIMemoryOwnerToMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = (IMemoryOwner<int>)null!;

        // act
        using var result = mapper.Map<IMemoryOwner<int>, MemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MemoryOwner<string>>(result);
        Assert.Equal(0, result.Length);
    }

    [Fact]
    public void ConvertsIMemoryOwnerToArrayPoolBufferWriter()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToMemoryOwner();

        // act
        using var result = mapper.Map<IMemoryOwner<int>, ArrayPoolBufferWriter<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ArrayPoolBufferWriter<string>>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result.AsEnumerable());
    }

    [Fact]
    public void ConvertsNullIMemoryOwnerToArrayPoolBufferWriter()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = (IMemoryOwner<int>)null!;

        // act
        using var result = mapper.Map<IMemoryOwner<int>, ArrayPoolBufferWriter<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ArrayPoolBufferWriter<string>>(result);
        Assert.Equal(0, result.WrittenCount);
    }

    [Fact]
    public void ConvertsMemoryOwnerToIEnumerable()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToMemoryOwner();

        // act
        var result = mapper.Map<MemoryOwner<int>, IEnumerable<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<string[]>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result);
    }

    [Fact]
    public void ConvertsMemoryOwnerToIMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToMemoryOwner();

        // act
        var result = mapper.Map<MemoryOwner<int>, IMemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MemoryOwner<string>>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result.AsEnumerable());
    }

    [Fact]
    public void ConvertsMemoryOwnerToMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToMemoryOwner();

        // act
        var result = mapper.Map<MemoryOwner<int>, MemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MemoryOwner<string>>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result.AsEnumerable());
    }

    [Fact]
    public void ConvertsMemoryOwnerToArrayPoolBufferWriter()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToMemoryOwner();

        // act
        var result = mapper.Map<MemoryOwner<int>, ArrayPoolBufferWriter<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ArrayPoolBufferWriter<string>>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result.AsEnumerable());
    }

    [Fact]
    public void ConvertsArrayPoolBufferWriterToIEnumerable()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToArrayPoolBufferWriter();

        // act
        var result = mapper.Map<ArrayPoolBufferWriter<int>, IEnumerable<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<string[]>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result);
    }

    [Fact]
    public void ConvertsArrayPoolBufferWriterToIMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToArrayPoolBufferWriter();

        // act
        using var result = mapper.Map<IMemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MemoryOwner<string>>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result.AsEnumerable());
    }

    [Fact]
    public void ConvertsArrayPoolBufferWriterToArrayPoolBufferWriter()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToArrayPoolBufferWriter();

        // act
        using var result = mapper.Map<ArrayPoolBufferWriter<int>, ArrayPoolBufferWriter<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ArrayPoolBufferWriter<string>>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result.AsEnumerable());
    }

    [Fact]
    public void ConvertsArrayPoolBufferWriterToMemoryOwner()
    {
        // arrange
        var config = new MapperConfiguration(config =>
        {
            config.CreateMemoryOwnerTypeMaps();
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        using var source = Enumerable.Range(1, 1000).ToArrayPoolBufferWriter();

        // act
        using var result = mapper.Map<ArrayPoolBufferWriter<int>, MemoryOwner<string>>(source);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MemoryOwner<string>>(result);
        Assert.Equal(source.AsEnumerable().Select(x => x.ToString()), result.AsEnumerable());
    }
}