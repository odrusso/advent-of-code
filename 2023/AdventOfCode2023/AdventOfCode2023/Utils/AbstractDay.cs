namespace AdventOfCode2023.Utils;

public abstract class AbstractDay
{
    private string[] GetLines() => File.ReadAllLines($"../../../{GetType().Name}/input.txt");

    protected abstract object ProcessPartOne(string[] input);

    protected abstract object ProcessPartTwo(string[] input);

    public void RunPartOne()
    {
        Console.WriteLine(ProcessPartOne(GetLines()).ToString());
    }

    public void RunPartTwo()
    {
        Console.WriteLine(ProcessPartTwo(GetLines()).ToString());
    }
}