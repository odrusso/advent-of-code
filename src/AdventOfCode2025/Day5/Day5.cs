using AdventUtils;

namespace AdventOfCode2025.Day5;

public class Day5 : AbstractDay
{
    public override object ProcessPartOne(string[] input)
    {
        var (rangeStrings, items) = PraseInput(input);
        var rangeMap = BuildRangeMap(rangeStrings);

        return items
            .Count(item => rangeMap
                .Select(r => item >= r.start && item <= r.end)
                .Any(a => a));
    }

    public override object ProcessPartTwo(string[] input)
    {
        var (rangeStrings, _) = PraseInput(input);
        var rangeMap = BuildRangeMap(rangeStrings);

        return rangeMap.Sum(range => range.end - range.start + 1);
    }

    private static (string[] ranges, long[] items) PraseInput(string[] input)
    {
        var blankLineIndex = Array.IndexOf(input, string.Empty);
        var rangeStrings = input[..blankLineIndex];
        var itemStrings = input[(blankLineIndex + 1)..].Select(long.Parse).ToArray();
        return (rangeStrings, itemStrings);
    }

    private static List<(long start, long end)> BuildRangeMap(string[] rangeStrings)
    {
        // Build and populate a range map, keyed on the lower bound, valued to the upper bound
        var rangeMap = new List<(long start, long end)>();
        foreach (var rangeString in rangeStrings)
        {
            var parts = rangeString.Split('-');
            var lower = long.Parse(parts[0]);
            var upper = long.Parse(parts[1]);
            rangeMap.Add((lower, upper));
        }

        // We've got an overlapping range map, now we need to merge the ranges
        while (!RangeMapNonOverlapping(rangeMap))
        {
            rangeMap = DeduplicateRange(rangeMap);
        }

        return rangeMap;
    }

    private static List<(long start, long end)> DeduplicateRange(List<(long start, long end)> rangeMap)
    {
        var uniqueRangeMap = new List<(long start, long end)>();

        foreach (var newRange in rangeMap)
        {
            // We either need to:

            // Do nothing, because the range is already covered. The min >= this && max <= this.
            var isRedundant =
                uniqueRangeMap.Any(existing => newRange.start >= existing.start && newRange.end <= existing.end);
            if (isRedundant) continue;

            // Add it in, because it's not overlapping with All of the other values in the unique map.
            // Either start & end are both below start or start and end are above end, for all. 0 overlaps.
            var isNovel =
                uniqueRangeMap.All(existing => newRange.end < existing.start || newRange.start > existing.end);
            if (isNovel)
            {
                uniqueRangeMap.Add(newRange);
                continue;
            }

            // Update exactly one value in the unique map. This could be either or both of the starts and mins.
            // At this point we know it's not redundant, and we know it's not novel. We need to find the index of the first range it overlaps with.
            var overlappingIndex = uniqueRangeMap.FindIndex(existing =>
                newRange.start <= existing.end && existing.start <= newRange.end);
            var overlappingRange = uniqueRangeMap[overlappingIndex];

            var replacementRange = (
                Math.Min(overlappingRange.start, newRange.start),
                Math.Max(overlappingRange.end, newRange.end));

            uniqueRangeMap[overlappingIndex] = replacementRange;
        }

        return uniqueRangeMap;
    }

    private static bool RangeMapNonOverlapping(List<(long start, long end)> rangeMap)
    {
        foreach (var range in rangeMap)
        {
            var isNovel = rangeMap.Except([range])
                .All(existing => range.end < existing.start || range.start > existing.end);
            if (!isNovel) return false;
        }

        return true;
    }
}