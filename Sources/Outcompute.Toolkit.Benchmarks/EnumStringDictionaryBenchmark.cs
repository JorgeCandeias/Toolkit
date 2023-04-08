using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Outcompute.Toolkit.Core.Extensions;

namespace Outcompute.Toolkit.Benchmarks;

[MemoryDiagnoser]
public class EnumStringDictionaryBenchmark
{
    private readonly Consumer _consumer = new();

    private readonly SomeValue[] _data = Enum.GetValues<SomeValue>();

    private readonly Dictionary<SomeValue, string> _lookup = new();

    [GlobalSetup]
    public void GlobalSetup()
    {
        var values = Enum.GetValues<SomeValue>();
        var names = Enum.GetNames<SomeValue>();

        for (var i = 0; i < names.Length; i++)
        {
            _lookup[values[i]] = names[i];
        }
    }

    [Benchmark(Baseline = true, OperationsPerInvoke = 10)]
    public void WithToString()
    {
        for (var i = 0; i < _data.Length; i++)
        {
            _consumer.Consume(_data[i].ToString());
        }
    }

    [Benchmark(OperationsPerInvoke = 10)]
    public void WithAsString()
    {
        for (var i = 0; i < _data.Length; i++)
        {
            _consumer.Consume(_data[i].AsString());
        }
    }
}

public enum SomeValue
{
    V0,
    V1,
    V2,
    V3,
    V4,
    V5,
    V6,
    V7,
    V8,
    V9,
}