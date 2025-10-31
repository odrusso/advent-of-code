using System.Text.RegularExpressions;
using AdventUtils;

namespace AdventOfCode2023.Day1;

public class Day1 : AbstractDay
{
    public override object ProcessPartOne(string[] input) =>
        input.Sum(line =>
        {
            var numbersInString = Regex.Matches(line, @"(\d)");
            return ValidTokens[numbersInString.First().Value] * 10 +
                   ValidTokens[numbersInString.Last().Value];
        });

    public override object ProcessPartTwo(string[] input) =>
        input.Sum(line =>
        {
            var numbersInString = Regex.Matches(line, $"(?=({string.Join("|", ValidTokens.Keys)}))");
            return ValidTokens[numbersInString.First().Groups[1].Value] * 10 +
                   ValidTokens[numbersInString.Last().Groups[1].Value];
        });

    private static readonly Dictionary<string, int> ValidTokens = new()
    {
        ["1"] = 1,
        ["2"] = 2,
        ["3"] = 3,
        ["4"] = 4,
        ["5"] = 5,
        ["6"] = 6,
        ["7"] = 7,
        ["8"] = 8,
        ["9"] = 9,
        ["one"] = 1,
        ["two"] = 2,
        ["three"] = 3,
        ["four"] = 4,
        ["five"] = 5,
        ["six"] = 6,
        ["seven"] = 7,
        ["eight"] = 8,
        ["nine"] = 9
    };
}