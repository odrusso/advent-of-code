using AdventUtils;

namespace AdventOfCode2024.Day5;

public class Day5 : AbstractDay
{
    public override object ProcessPartOne(string[] input)
    {
        var (ruleLines, updateLines) = ParseInput(input);
        var valueToAllLesser = BuildLesserMap(ruleLines);
        var validUpdates = updateLines.Where(ul => IsUpdateValid(ul, valueToAllLesser));
        return validUpdates.Sum(MiddleValue);
    }

    // 4366 TOO LOW!
    // 4603, still too low!
    public override object ProcessPartTwo(string[] input)
    {
        var (ruleLines, updateLines) = ParseInput(input);
        var valueToAllLesser = BuildLesserMap(ruleLines);
        var invalidUpdates = updateLines.Where(ul => !IsUpdateValid(ul, valueToAllLesser));
        var fixedUpdates = invalidUpdates.Select(invalid => RecursiveSort([], invalid, valueToAllLesser));

        return fixedUpdates.Sum(MiddleValue);
    }

    private static ((int Lesser, int Greater)[], int[][]) ParseInput(string[] input)
    {
        var breakIndex = Array.IndexOf(input, "");

        var ruleStrings = input[..breakIndex];
        var rules = ruleStrings
            .Select(rs => rs.Split('|').Select(int.Parse).ToArray())
            .Select(r => (r[0], r[1]))
            .ToArray();

        var pageStrings = input[(breakIndex + 1)..];
        var pages = pageStrings.Select(ps => ps.Split(',').Select(int.Parse).ToArray()).ToArray();

        return (rules, pages);
    }

    private static Dictionary<int, List<int>> BuildLesserMap((int Lesser, int Greater)[] ruleLines)
    {
        var valueToAllLesser = new Dictionary<int, List<int>>();

        foreach (var rule in ruleLines)
        {
            // TODO: is there a more terse way of writing this?
            if (valueToAllLesser.ContainsKey(rule.Greater))
            {
                valueToAllLesser[rule.Greater].Add(rule.Lesser);
            }
            else
            {
                valueToAllLesser[rule.Greater] = [rule.Lesser];
            }
        }

        return valueToAllLesser;
    }

    private static bool IsUpdateValid(int[] update, Dictionary<int, List<int>> valueToAllLesser)
    {
        for (int i = 0; i < update.Length - 1; i++)
        {
            var allOfTheseValuesCannotComeAfter = valueToAllLesser.GetValueOrDefault(update[i]);

            // If no rules for this, skip this index.
            if (allOfTheseValuesCannotComeAfter is null) continue;

            var valuesAfterCurrentIndex = update[(i + 1)..];

            // One of the future values should've come earlier, this whole update must be invalid
            if (valuesAfterCurrentIndex.Any(futureValue => allOfTheseValuesCannotComeAfter.Contains(futureValue)))
                return false;
        }

        return true;
    }

    private static int MiddleValue(int[] arg)
    {
        int middleIndex = arg.Length / 2;
        return arg[middleIndex];
    }

    private static int[] RecursiveSort(int[] sortedSoFar, int[] newValues, Dictionary<int, List<int>> lesserMap)
    {
        // Finished sorting
        if (newValues.Length == 0) return sortedSoFar;

        var newValue = newValues[0];

        // Just started sorting
        if (sortedSoFar.Length == 0) return RecursiveSort([newValue], newValues[1..], lesserMap);

        // Go through each index we could place it, and place it at the last valid position (by traversing backwards)
        for (int insertionIndex = sortedSoFar.Length; insertionIndex >= 0; insertionIndex--)
        {
            // Surely a nicer way to insert... TODO: get back to this. 
            int[] testArray = [..sortedSoFar[..insertionIndex], newValue, ..sortedSoFar[insertionIndex..]];
            if (IsUpdateValid(testArray, lesserMap))
            {
                return RecursiveSort(testArray, newValues[1..], lesserMap);
            }
        }

        // After each sort, we should check it's valid again lol
        throw new Exception("Un-sortable, apparently.");
    }
}