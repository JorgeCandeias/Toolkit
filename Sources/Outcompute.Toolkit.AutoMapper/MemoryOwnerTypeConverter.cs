using CommunityToolkit.Diagnostics;

namespace Outcompute.Toolkit.AutoMapper;

internal class MemoryOwnerTypeConverter<TSource, TDestination> :
    ITypeConverter<IEnumerable<TSource>, IMemoryOwner<TDestination>>,
    ITypeConverter<IEnumerable<TSource>, MemoryOwner<TDestination>>,
    ITypeConverter<IEnumerable<TSource>, ArrayPoolBufferWriter<TDestination>>,
    ITypeConverter<IMemoryOwner<TSource>, IEnumerable<TDestination>>,
    ITypeConverter<IMemoryOwner<TSource>, MemoryOwner<TDestination>>,
    ITypeConverter<IMemoryOwner<TSource>, ArrayPoolBufferWriter<TDestination>>,
    ITypeConverter<IMemoryOwner<TSource>, IMemoryOwner<TDestination>>
{
    public IMemoryOwner<TDestination> Convert(IEnumerable<TSource> source, IMemoryOwner<TDestination> destination, ResolutionContext context)
    {
        if (source is null)
        {
            return new ArrayPoolBufferWriter<TDestination>(0);
        }

        var mapper = context.Mapper;

        return source.Select(mapper.Map<TSource, TDestination>).ToArrayPoolBufferWriter();
    }

    public MemoryOwner<TDestination> Convert(IEnumerable<TSource> source, MemoryOwner<TDestination> destination, ResolutionContext context)
    {
        if (source is null)
        {
            return MemoryOwner<TDestination>.Empty;
        }

        var mapper = context.Mapper;

        return source.Select(mapper.Map<TSource, TDestination>).ToMemoryOwner();
    }

    public ArrayPoolBufferWriter<TDestination> Convert(IEnumerable<TSource> source, ArrayPoolBufferWriter<TDestination> destination, ResolutionContext context)
    {
        if (source is null)
        {
            return new ArrayPoolBufferWriter<TDestination>(0);
        }

        var mapper = context.Mapper;

        return source.Select(mapper.Map<TSource, TDestination>).ToArrayPoolBufferWriter();
    }

    public IEnumerable<TDestination> Convert(IMemoryOwner<TSource> source, IEnumerable<TDestination> destination, ResolutionContext context)
    {
        if (source is null)
        {
            return Enumerable.Empty<TDestination>();
        }

        var mapper = context.Mapper;
        var span = source.Memory.Span;
        var length = span.Length;
        var array = new TDestination[length];

        for (var i = 0; i < length; i++)
        {
            array[i] = mapper.Map<TSource, TDestination>(span[i]);
        }

        return array;
    }

    public MemoryOwner<TDestination> Convert(IMemoryOwner<TSource> source, MemoryOwner<TDestination> destination, ResolutionContext context)
    {
        if (source is null)
        {
            return MemoryOwner<TDestination>.Empty;
        }

        var mapper = context.Mapper;
        var span = source.Memory.Span;
        var length = span.Length;
        var owner = MemoryOwner<TDestination>.Allocate(length);
        var result = owner.Span;

        for (var i = 0; i < length; i++)
        {
            result[i] = mapper.Map<TSource, TDestination>(span[i]);
        }

        return owner;
    }

    public ArrayPoolBufferWriter<TDestination> Convert(IMemoryOwner<TSource> source, ArrayPoolBufferWriter<TDestination> destination, ResolutionContext context)
    {
        if (source is null)
        {
            return new ArrayPoolBufferWriter<TDestination>(0);
        }

        var mapper = context.Mapper;
        var span = source.Memory.Span;
        var length = span.Length;
        var owner = new ArrayPoolBufferWriter<TDestination>(length);
        var result = owner.GetSpan(length);

        for (var i = 0; i < length; i++)
        {
            result[i] = mapper.Map<TSource, TDestination>(span[i]);
        }

        owner.Advance(length);

        return owner;
    }

    public IMemoryOwner<TDestination> Convert(IMemoryOwner<TSource> source, IMemoryOwner<TDestination> destination, ResolutionContext context)
    {
        if (source is null)
        {
            return MemoryOwner<TDestination>.Empty;
        }

        var mapper = context.Mapper;
        var span = source.Memory.Span;
        var length = span.Length;
        var owner = MemoryOwner<TDestination>.Allocate(length);
        var result = owner.Span;

        for (var i = 0; i < length; i++)
        {
            result[i] = mapper.Map<TSource, TDestination>(span[i]);
        }

        return owner;
    }
}

public static class MemoryOwnerTypeConverterMapperConfigurationExtensions
{
    public static void CreateMemoryOwnerTypeMaps(this IMapperConfigurationExpression options)
    {
        Guard.IsNotNull(options);

        options.CreateMap(typeof(IEnumerable<>), typeof(IMemoryOwner<>)).ConvertUsing(typeof(MemoryOwnerTypeConverter<,>));
        options.CreateMap(typeof(IEnumerable<>), typeof(MemoryOwner<>)).ConvertUsing(typeof(MemoryOwnerTypeConverter<,>));
        options.CreateMap(typeof(IEnumerable<>), typeof(ArrayPoolBufferWriter<>)).ConvertUsing(typeof(MemoryOwnerTypeConverter<,>));
        options.CreateMap(typeof(IMemoryOwner<>), typeof(IEnumerable<>)).ConvertUsing(typeof(MemoryOwnerTypeConverter<,>));
        options.CreateMap(typeof(IMemoryOwner<>), typeof(MemoryOwner<>)).ConvertUsing(typeof(MemoryOwnerTypeConverter<,>));
        options.CreateMap(typeof(IMemoryOwner<>), typeof(ArrayPoolBufferWriter<>)).ConvertUsing(typeof(MemoryOwnerTypeConverter<,>));
        options.CreateMap(typeof(IMemoryOwner<>), typeof(IMemoryOwner<>)).ConvertUsing(typeof(MemoryOwnerTypeConverter<,>));
    }
}