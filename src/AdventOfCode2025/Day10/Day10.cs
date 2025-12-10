using AdventUtils;

namespace AdventOfCode2025.Day10;

public class Day10 : AbstractDay
{
    private record Machine(uint Target, uint[] Buttons);

    public override object ProcessPartOne(string[] input)
    {
        Machine[] machines = ParseInput(input);

        long count = 0;

        foreach (var machine in machines)
        {
            long bestOption = long.MaxValue;

            foreach (var subsetA in SubSetsOf(machine.Buttons))
            {
                var subset = subsetA.ToArray();

                var testTarget = 0u;

                foreach (var subsetButton in subset)
                {
                    testTarget ^= subsetButton;
                }

                if (testTarget != machine.Target) continue;

                if (bestOption > subset.Count())
                {
                    bestOption = subset.Count();
                }

            }

            count += bestOption;
        }

        return count;
    }

    public override object ProcessPartTwo(string[] input)
    {
        return 0L;
    }

    private static Machine[] ParseInput(string[] input)
    {
        List<Machine> machines = [];

        foreach (var line in input)
        {
            var targetString = new string(line.Split("]")[0][1..].Replace(".", "0").Replace("#", "1").Reverse().ToArray());
            var target = Convert.ToUInt16(targetString, 2);

            var buttonStrings = line.Split(" ")[1..^1].Select(s => s.Replace("(", "").Replace(")", "").Split(","));

            List<uint> buttons = [];

            foreach (var buttonString in buttonStrings)
            {
                uint button = 0;
                foreach (var bs in buttonString)
                {
                    button ^= 1U << int.Parse(bs);
                }
                buttons.Add(button);
            }

            machines.Add(new Machine(target, buttons.ToArray()));
        }

        return machines.ToArray();
    }

    // I stole this because cbf
    public static IEnumerable<IEnumerable<T>> SubSetsOf<T>(IEnumerable<T> source)
    {
        if (!source.Any())
            return Enumerable.Repeat(Enumerable.Empty<T>(), 1);

        var element = source.Take(1);

        var haveNots = SubSetsOf(source.Skip(1));
        var haves = haveNots.Select(set => element.Concat(set));

        return haves.Concat(haveNots);
    }
}