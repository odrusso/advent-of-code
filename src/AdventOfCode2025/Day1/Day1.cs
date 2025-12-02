using AdventUtils;

namespace AdventOfCode2025.Day1;

public class Day1 : AbstractDay
{
    public override object ProcessPartOne(string[] input)
    {
        int zeroCount = 0;
        int currentPosition = 50;

        foreach (var line in input)
        {
            currentPosition = Mod(currentPosition + BuildSigned(line), 100);

            if (currentPosition == 0)
            {
                zeroCount++;
            }
        }

        return zeroCount;
    }

    public override object ProcessPartTwo(string[] input)
    {
        int zeroCount = 0;
        int currentPosition = 50;

        foreach (var line in input)
        {
            var movement = BuildSigned(line);
            var sign = movement >= 0 ? 1 : -1;
            var ticks = Math.Abs(movement);

            for (int i = 0; i < ticks; i++)
            {
                currentPosition = Mod(currentPosition + sign, 100);
                
                if (currentPosition == 0)
                {
                    zeroCount++;
                }
            }
        }

        return zeroCount;
    }

    private int BuildSigned(string line)
    {
        var sign = line[0];
        var number = int.Parse(line[1..]);
        return sign == 'R' ? number : -number;
    }

    private int Mod(int value, int mod) => (Math.Abs(value * mod) + value) % mod;
}