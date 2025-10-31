using AdventUtils;

namespace AdventOfCode2024.Day2;

public class Day2 : AbstractDay
{
    private const int MaxDelta = 3;
    private const int MinDelta = 1;

    public override object ProcessPartOne(string[] input) => input
        .Select(ParseReports)
        .Count(LevelsSafe);

    public override object ProcessPartTwo(string[] input) => input
        .Select(ParseReports)
        .Select(GeneratePermutations)
        .Count(perm => perm.Any(LevelsSafe));

    private static IEnumerable<int[]> GeneratePermutations(int[] levels) =>
        levels
            .Select((l, i) => levels.Where((_, ii) => i != ii).ToArray());

    private static int[] ParseReports(string input) => input.Split(' ').Select(int.Parse).ToArray();

    private static bool LevelsSafe(int[] levels)
    {
        var deltas = new List<int>();

        for (var i = 1; i < levels.Length; i++)
        {
            deltas.Add(levels[i - 1] - levels[i]);
        }

        // are there any big jumps?
        if (deltas.Any(d => Math.Abs(d) > MaxDelta)) return false;
        
        // check if sufficiently monotonic
        if (!deltas.All(d => d >= MinDelta) && !deltas.All(d => d <= -MinDelta)) return false;

        return true;
    }
}