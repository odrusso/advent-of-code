using AdventUtils;

namespace AdventOfCode2024.Day1;

public class Day1 : AbstractDay
{
    // 2192892 is right!
    protected override string[] GetLines() => File.ReadAllLines($"/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day1/input.txt");

    public override object ProcessPartOne(string[] input)
    {
        var (listOne, listTwo) = GetLists(input);

        return listOne.Zip(listTwo, (x, y) => Math.Abs(x - y)).Sum();
    }

    // 22962826 is right!
    public override object ProcessPartTwo(string[] input)
    {
        var (listOne, listTwo) = GetLists(input);

        // var listTwoLookup = listTwo.Distinct().ToDictionary(val => val, val => listTwo.Count(v => v == val));
        var listTwoLookup = listTwo.GroupBy(v => v).ToDictionary(g => g.Key, g => g.Count());
        
        return listOne.Sum(leftNumber => leftNumber * listTwoLookup.GetValueOrDefault(leftNumber));
    }

    private static (IOrderedEnumerable<int> listOne, IOrderedEnumerable<int> listTwo) GetLists(string[] input)
    {
        var parsedLines = input.Select(line => line.Split("   ").Select(int.Parse).ToArray()).ToArray();

        var listOne = parsedLines.Select(line => line[0]).Order();
        var listTwo = parsedLines.Select(line => line[1]).Order();
        return (listOne, listTwo);
    }
}