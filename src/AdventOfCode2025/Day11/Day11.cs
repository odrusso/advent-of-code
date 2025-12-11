using AdventUtils;

namespace AdventOfCode2025.Day11;

public class Day11 : AbstractDay
{
    private readonly Dictionary<(string, bool, bool), long> _pathExtraMemo = [];

    public override object ProcessPartOne(string[] input)
    {
        Dictionary<string, string[]> paths = ParseInput(input);

        return PathsExtra(paths, "you", true, true);
    }

    public override object ProcessPartTwo(string[] input)
    {
        Dictionary<string, string[]> paths = ParseInput(input);

        return PathsExtra(paths, "svr", false, false);
    }

    private long PathsExtra(Dictionary<string, string[]> paths, string entry, bool seenDac, bool seenFft)
    {
        if (_pathExtraMemo.ContainsKey((entry, seenDac, seenFft))) return _pathExtraMemo[(entry, seenDac, seenFft)];

        seenDac |= entry == "dac";
        seenFft |= entry == "fft";

        if (entry == "out")
        {
            if (seenDac && seenFft) return 1;
            return 0;
        }

        var result = paths[entry].Sum(e => PathsExtra(paths, e, seenDac, seenFft));
        _pathExtraMemo[(entry, seenDac, seenFft)] = result;
        return result;
    }

    private Dictionary<string, string[]> ParseInput(string[] input)
    {
        Dictionary<string, string[]> paths = [];

        foreach (var line in input)
        {
            var name = line.Split(":")[0];
            var outputs = line.Split(":")[1][1..].Split(" ");
            paths[name] = outputs;
        }

        return paths;
    }
}