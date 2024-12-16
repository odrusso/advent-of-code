using System.Text.RegularExpressions;
using AdventUtils;

namespace AdventOfCode2024.Day3;

public class Day3 : AbstractDay
{
    protected override string[] GetLines() =>
        File.ReadAllLines(
            $"/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day3/input.txt");

    public override object ProcessPartOne(string[] input)
    {
        var matches = Regex.Matches(string.Join("", input), @"mul\(([0-9]+),([0-9]+)\)");
        return matches.Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
    }

    public override object ProcessPartTwo(string[] input)
    {
        var line = string.Join("", input);
        line = Regex.Replace(line, @"don't\(\).*?do\(\)", "");
        var matches = Regex.Matches(line, @"mul\(([0-9]+),([0-9]+)\)");
        return matches.Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
    }
}
