using AdventUtils;

namespace AdventOfCode2024.Day11;

public class Day11 : AbstractDay
{
    // A stone + blink-count to stone count lookup
    private readonly Dictionary<(long, int), long> _lookup = [];

    protected override string[] GetLines() =>
        File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day11/input.txt");

    protected override object ProcessPartOne(string[] input) => StoneCount(input, 25);

    protected override object ProcessPartTwo(string[] input) => StoneCount(input, 75);

    private long StoneCount(string[] input, int blinks)
    {
        var result = input.Single().Split(' ').Select(long.Parse).ToList();
        return result.Sum(stone => StoneCountAfter(stone, blinks));
    }

    private long StoneCountAfter(long stone, int blinks)
    {
        if (blinks == 0) return 1; // 1 stone we're looking at now.

        if (_lookup.ContainsKey((stone, blinks))) return _lookup[(stone, blinks)];

        long newCount = EvaluateRules(stone).Sum(newStone => StoneCountAfter(newStone, blinks - 1));

        _lookup[(stone, blinks)] = newCount;

        return newCount;
    }

    private static List<long> EvaluateRules(long stone)
    {
        if (stone == 0) return [1];

        var stoneString = stone.ToString();
        if (stoneString.Length % 2 == 0)
        {
            var midpoint = stoneString.Length / 2;
            return [long.Parse(stoneString[..midpoint]), long.Parse(stoneString[midpoint..])];
        }

        return [stone * 2024];
    }
}