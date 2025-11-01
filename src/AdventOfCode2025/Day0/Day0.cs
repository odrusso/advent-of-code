using AdventUtils;

namespace AdventOfCode2025.Day0;

public class Day0 : AbstractDay
{
    public override object ProcessPartOne(string[] input) => input.Select(int.Parse).Sum();

    public override object ProcessPartTwo(string[] input) => input.Select(int.Parse).Sum() + 1;
}