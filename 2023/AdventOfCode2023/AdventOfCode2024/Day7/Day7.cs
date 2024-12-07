using AdventUtils;

namespace AdventOfCode2024.Day7;

public class Day7 : AbstractDay
{
    protected override string[] GetLines() =>
        File.ReadAllLines(
            $"/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day7/input.txt");

    protected override object ProcessPartOne(string[] input)
    {
        var parsedLines = ParseLines(input);

        var calculableLines = parsedLines.Where(line => RecurseCalculate(line.result, line.operands));

        return calculableLines.Sum(line => line.result);
    }

    protected override object ProcessPartTwo(string[] input)
    {
        var parsedLines = ParseLines(input);

        var calculableLines = parsedLines.Where(line => RecurseCalculateWithConcat(line.result, line.operands));

        return calculableLines.Sum(line => line.result);
    }

    private static bool RecurseCalculate(long lineResult, long[] ops)
    {
        if (ops.Length == 1) return lineResult == ops[0];

        return
            RecurseCalculate(lineResult, [ops[0] + ops[1], ..ops[2..]]) || // Addition branch
            RecurseCalculate(lineResult, [ops[0] * ops[1], ..ops[2..]]); // Multiplication branch
    }

    private static bool RecurseCalculateWithConcat(long lineResult, long[] ops)
    {
        if (ops.Length == 1) return lineResult == ops[0];

        return
            RecurseCalculateWithConcat(lineResult, [ops[0] + ops[1], ..ops[2..]]) || // Addition branch
            RecurseCalculateWithConcat(lineResult, [ops[0] * ops[1], ..ops[2..]]) || // Multiplication branch
            RecurseCalculateWithConcat(lineResult, [Concat(ops[0], ops[1]), ..ops[2..]]); // Concat branch
    }

    private static (long result, long[] operands)[] ParseLines(string[] lines) =>
        lines.Select(arg =>
        {
            var splits = arg.Split(": ");
            var result = long.Parse(splits[0]);
            var operands = splits[1].Split(" ").Select(long.Parse).ToArray();

            return (result, operands);
        }).ToArray();

    private static long Concat(long a, long b)
    {
        int exp = (int)Math.Ceiling(Math.Log10(b));
        return a * (long)Math.Pow(10, exp) + b;
    }
}