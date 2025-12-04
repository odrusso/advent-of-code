using AdventUtils;

namespace AdventOfCode2025.Day4;

public class Day4 : AbstractDay
{
    public override object ProcessPartOne(string[] input)
    {
        // Just do a single round
        return ClearRolls(input);
    }

    public override object ProcessPartTwo(string[] input)
    {
        long rollsRemoved = 0;
        long prevRollsRemoved = -1;

        while (prevRollsRemoved != rollsRemoved)
        {
            prevRollsRemoved = rollsRemoved;
            rollsRemoved += ClearRolls(input);
        }

        return rollsRemoved;
    }

    private static long ClearRolls(string[] input)
    {
        long accCount = 0;
        List<(int y, int x)> cleared = [];

        // Y is row index, 0 is top row
        // X is column index, 0 is leftmost column

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (IsAccessible(y, x, input))
                {
                    accCount++;
                    cleared.Add((y, x));
                }
            }
        }

        // Update input
        foreach (var (y, x) in cleared)
        {
            var rowChars = input[y].ToCharArray();
            rowChars[x] = '.';
            input[y] = new string(rowChars);
        }

        return accCount;
    }

    private static bool IsAccessible(int y, int x, string[] map)
    {
        // Ignore the empty spaces
        if (map[y][x] == '.') return false;

        // Check each adjacent position
        var directions = new (int dy, int dx)[]
        {
            (-1, 0), // North
            (-1, 1), // North-east
            (0, 1), // East
            (1, 1), // South-east
            (1, 0), // South
            (1, -1), // South-west
            (0, -1), // West
            (-1, -1) // North-west
        };

        var bundleCount = directions.Count(direction => IsRoll(y + direction.dy, x + direction.dx, map));

        return bundleCount < 4;
    }

    private static bool IsRoll(int y, int x, string[] map)
    {
        // Range checks
        if (y < 0 || y >= map.Length || x < 0 || x >= map[0].Length) return false;

        return map[y][x] == '@';
    }
}