using AdventUtils;

namespace AdventOfCode2025.Day3;

public class Day3 : AbstractDay
{
    public override object ProcessPartOne(string[] input)
    {
        long acc = 0;

        foreach (var bankString in input)
        {
            var bank = bankString.Select(c => long.Parse(new string(c, 1))).ToArray();
            long solved = SolveBank(bank, 2);
            acc += solved;
        }

        return acc;
    }

    public override object ProcessPartTwo(string[] input)
    {
        long acc = 0;

        foreach (var bankString in input)
        {
            var bank = bankString.Select(c => long.Parse(new string(c, 1))).ToArray();
            long solved = SolveBank(bank, 12);
            acc += solved;
        }

        return acc;
    }

    private long SolveBank(long[] bank, int size)
    {
        // Base cases
        if (bank.Length == 0) return -1; // We probably shouldn't get here, but it happens because I'm lazy
        if (bank.Length == 1 && size != 1) return -1; // We probably shouldn't get here, but it happens because I'm lazy

        if (size == 1) return bank.Max();

        long subMax = 0;
        for (var i = 0; i < bank.Length - size + 1; i++)
        {
            var thisLeft = bank[i] * (long)Math.Pow(10, size - 1);
            var solved = thisLeft + SolveBank(bank[(i + 1)..], size - 1);
            if (solved > subMax) subMax = solved;
        }

        return subMax;
    }
}