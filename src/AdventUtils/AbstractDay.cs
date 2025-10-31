using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace AdventUtils;

// [MemoryDiagnoser]
public abstract class AbstractDay
{
    protected virtual string[] GetLines([CallerFilePath] string? filePath = null)
    {
        if (filePath == null)
            throw new InvalidOperationException("Could not determine file path");
            
        var directory = Path.GetDirectoryName(filePath);
        var inputPath = Path.Combine(directory!, "input.txt");
        return File.ReadAllLines(inputPath);
    }

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