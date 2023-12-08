using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Benchmarks;

[MemoryDiagnoser]
public class Day1Benchmarks
{
    const string Numbers = "0123456789";
    private readonly SearchValues<char> _searchValues = SearchValues.Create(Numbers);
    private readonly string[] _lines = File.ReadLines("C:\\Users\\panef\\Desktop\\Input_Day_1.txt").ToArray();

    [Benchmark(Baseline = true)]
    public int CalculateCalibrationValue_ForeachAdd()
    {
        int number = 0;
        foreach (var line in _lines)
        {
            number += Parse(line);
        }

        return number;
    }

    [Benchmark]
    public int CalculateCalibrationValue_ForeachSum()
    {
        var numbers = new List<int>();
        foreach (var line in _lines)
        {
            var number = Parse(line);
            numbers.Add(number);
        }
        return numbers.Sum();
    }

    [Benchmark]
    public int CalculateCalibrationValue_SumWithLambda()
    {
        return _lines.Sum(line => Parse(line));
    }

    [Benchmark]
    public int CalculateCalibrationValue_SumWithoutLambda()
    {
        return _lines.Sum(Parse);
    }

    [Benchmark]
    public int CalculateCalibrationValue_Unsafe()
    {
        var number = 0;
        ref var start = ref MemoryMarshal.GetArrayDataReference(_lines);
        ref var end = ref Unsafe.Add(ref start, _lines.Length);

        while (Unsafe.IsAddressLessThan(ref start, ref end))
        {
            number += Parse(start);
            start = ref Unsafe.Add(ref start, 1);
        }

        return number;
    }

    private int Parse(string input)
    {
        var inputAsSpan = input.AsSpan();
        var firstIndex = inputAsSpan.IndexOfAny(_searchValues);
        var lastIndex = inputAsSpan.LastIndexOfAny(_searchValues);

        if (lastIndex == -1)
        {
            lastIndex = firstIndex;
        }

        return Convert.ToInt32($"{inputAsSpan[firstIndex]}{inputAsSpan[lastIndex]}");
    }
}
