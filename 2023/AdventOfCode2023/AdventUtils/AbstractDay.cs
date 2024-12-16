using BenchmarkDotNet.Attributes;

namespace AdventUtils;

// [MemoryDiagnoser]
public abstract class AbstractDay
{
    protected abstract string[] GetLines();

    public abstract object ProcessPartOne(string[] input);

    public abstract object ProcessPartTwo(string[] input);

    public void RunPartOne()
    {
        Console.WriteLine(ProcessPartOne(GetLines()).ToString());
    }

    public void RunPartTwo()
    {
        Console.WriteLine(ProcessPartTwo(GetLines()).ToString());
    }
    
    [Benchmark]
    public void BenchmarkPartOne() => ProcessPartOne(GetLines());

    [Benchmark]
    public void BenchmarkPartTwo() => ProcessPartTwo(GetLines());
}