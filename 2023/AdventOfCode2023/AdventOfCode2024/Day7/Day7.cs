using AdventUtils;

namespace AdventOfCode2024.Day7;

public class Day7 : AbstractDay
{
    protected override string[] GetLines() =>
        File.ReadAllLines(
            $"/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day7/input.txt");

    // 2501605301465
    public override object ProcessPartOne(string[] input)
    {
        var parsedLines = ParseLines(input);

        var calculableLines = parsedLines.Where(line => RecurseCalculate(line.result, line.operands));

        return calculableLines.Sum(line => line.result);
    }

    // 44841372855953
    public override object ProcessPartTwo(string[] input)
    {
        var parsedLines = ParseLines(input);

        var calculableLines = parsedLines.Where(line => RecurseCalculateWithConcat(line.result, line.operands));

        return calculableLines.Sum(line => line.result);
    }

    private static bool RecurseCalculate(long lineResult, long[] ops)
    {
        if (ops.Length == 1) return lineResult == ops[0];
        if (lineResult < 0) return false;

        if (lineResult % ops[^1] == 0)
            return
                RecurseCalculate(lineResult - ops[^1], ops[..^1]) || // Subtraction branch
                RecurseCalculate(lineResult / ops[^1], ops[..^1]); // Multiplication branch

        return RecurseCalculate(lineResult - ops[^1], ops[..^1]);
    }

    private static bool RecurseCalculateWithConcat(long lineResult, long[] ops)
    {
        if (ops.Length == 1) return lineResult == ops[0];
        if (lineResult < 0) return false;

        var valid = false;
        var lastOp = ops[^1];

        if (lineResult % lastOp == 0)
            valid = valid || RecurseCalculateWithConcat(lineResult / lastOp, ops[..^1]); // Multiplication branch

        var lrs = lineResult.ToString();
        var fops = lastOp.ToString();

        if (lrs.EndsWith(fops) && lrs.Length > fops.Length)
            valid = valid || RecurseCalculateWithConcat(long.Parse(lrs[..^fops.Length]), ops[..^1]); // Concat branch

        return valid || RecurseCalculateWithConcat(lineResult - lastOp, ops[..^1]); // Addition branch
    }

    private static (long result, long[] operands)[] ParseLines(string[] lines) =>
        lines.Select(arg =>
        {
            var splits = arg.Split(": ");
            var result = long.Parse(splits[0]);
            var operands = splits[1].Split(" ").Select(long.Parse).ToArray();

            return (result, operands);
        }).ToArray();
}