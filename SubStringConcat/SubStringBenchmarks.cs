using BenchmarkDotNet.Attributes;

namespace SubStringConcat;

[MemoryDiagnoser]
public class SubStringBenchmarks
{
    private const string AString = "This is something I wrote earlier";

    [Benchmark]
    public string UseSubstring()
    {
        return AString.Substring(0, 10) + "...";
    }

    [Benchmark]
    public string UseRange()
    {
        return AString[..10] + "...";
    }

    [Benchmark]
    public string UseSpanAndConcat()
    {
        return string.Concat(AString.AsSpan(0, 10), "...");
    } 
    
    [Benchmark]
    public string UseSubCat()
    {
        return AString.SubCat(..10, "...");
    }


    [Benchmark]
    public string UseSpanAndInterpolation()
    {
        return $"{AString.AsSpan(0, 10)}...";
    }

    [Benchmark]
    public string UseCreateAndInterpolation()
    {
        return string.Create(null, $"{AString.AsSpan(0, 10)}...");
    }

    [Benchmark]
    public string UseStringCreate()
    {
        string other = "...";
        return string.Create(13, (AString, other), static (span, s) =>
        {
            s.AString.AsSpan(0, 10).CopyTo(span);
            s.other.CopyTo(span[10..]);
        });
    }

    [Benchmark]
    public string UseStringCreateTryWrite()
    {
        string other = "...";
        return string.Create(13, (AString, other), static (span, s) =>
        {
            span.TryWrite($"{s.AString.AsSpan(0, 10)}{s.other}", out _);
        });
    }

}