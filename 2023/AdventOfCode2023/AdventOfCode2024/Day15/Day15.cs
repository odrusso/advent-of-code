using AdventUtils;

namespace AdventOfCode2024.Day15;

using Map = List<List<char>>;
using Vector = (int Y, int X);

public class Day15 : AbstractDay
{
    private const char Wall = '#';
    private const char Robot = '@';
    private const char Box = 'O';
    private const char FreeSpace = '.';

    private const char BoxLeft = '[';
    private const char BoxRight = ']';

    private static readonly Vector LeftMovement = new(0, -1);
    private static readonly Vector RightMovement = new(0, 1);
    private static readonly Vector UpMovement = new(-1, 0);
    private static readonly Vector DownMovement = new(1, 0);

    private static readonly Dictionary<char, Vector> Movements = new()
    {
        ['<'] = LeftMovement,
        ['>'] = RightMovement,
        ['^'] = UpMovement,
        ['v'] = DownMovement,
    };

    protected override string[] GetLines() =>
        File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day15/input_small.txt");

    public override object ProcessPartOne(string[] input)
    {
        var midPoint = Array.IndexOf(input, "");

        Map map = input[..midPoint].Select(l => l.ToList()).ToList();

        var movements = input[midPoint..].SelectMany(a => a);

        foreach (var movement in movements)
        {
            UpdateMap(ref map, movement);
        }

        int result = 0;
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == Box) result += 100 * y + x;
            }
        }

        return result;
    }

    private static void UpdateMap(ref Map map, char movement)
    {
        var move = Movements[movement];
        var robotPosition = GetRobotPosition(ref map);

        AttemptMove(ref map, move, robotPosition);
    }

    private static bool AttemptMove(ref Map map, Vector movement, Vector position)
    {
        Vector nextPosition = new Vector(position.Y + movement.Y, position.X + movement.X);

        char nextObject = map[nextPosition.Y][nextPosition.X];

        // Facing a wall, can't do anything.
        if (nextObject == Wall) return false;

        // Free to move, whatever this is.
        if (nextObject == FreeSpace)
        {
            map[nextPosition.Y][nextPosition.X] = map[position.Y][position.X];
            map[position.Y][position.X] = FreeSpace;
            return true;
        }

        if (nextObject == Box)
        {
            var moved = AttemptMove(ref map, movement, nextPosition);

            if (!moved) return false;

            map[nextPosition.Y][nextPosition.X] = map[position.Y][position.X];
            map[position.Y][position.X] = FreeSpace;
            return true;
        }

        throw new ApplicationException("In-exhaustive move setup.");
    }

    private static Vector GetRobotPosition(ref Map map)
    {
        var row = map.Single(l => l.Contains(Robot));

        var y = Array.IndexOf(map.ToArray(), row);
        var x = Array.IndexOf(row.ToArray(), Robot);

        if (y == -1 || x == -1) throw new ApplicationException("Couldn't find the robot");

        return new Vector(y, x);
    }

    public override object ProcessPartTwo(string[] input)
    {
        var midPoint = Array.IndexOf(input, "");

        Map map = CreateDoubleMap(input[..midPoint]);

        var movements = input[midPoint..].SelectMany(a => a);

        foreach (var movement in movements)
        {
            UpdateMapTwo(ref map, movement);
        }

        // int result = 0;
        // for (int y = 0; y < map.Count; y++)
        // {
        //     for (int x = 0; x < map[y].Count; x++)
        //     {
        //         if (map[y][x] == Box) result += 100 * y + x;
        //     }
        // }
        //
        // return result;

        return 0;
    }

    private static void UpdateMapTwo(ref Map map, char movement)
    {
        var move = Movements[movement];
        var robotPosition = GetRobotPosition(ref map);

        AttemptMoveTwo(ref map, move, robotPosition);
    }

    private static bool AttemptMoveTwo(ref Map map, Vector movement, Vector position)
    {
        Vector nextPosition = new Vector(position.Y + movement.Y, position.X + movement.X);

        char nextObject = map[nextPosition.Y][nextPosition.X];

        // Facing a wall, can't do anything.
        if (nextObject == Wall) return false;

        // Free to move, whatever this is.
        if (nextObject == FreeSpace)
        {
            map[nextPosition.Y][nextPosition.X] = map[position.Y][position.X];
            map[position.Y][position.X] = FreeSpace;
            return true;
        }

        // >> Thing is box <<
        if (movement == LeftMovement || movement == RightMovement)
        {
            if (nextObject == BoxLeft || nextObject == BoxRight)
            {
            }
        }

        throw new ApplicationException("In-exhaustive move setup.");
    }

    private static Map CreateDoubleMap(string[] a)
    {
        var baseMap = a.Select(l => l.ToList()).ToList();

        Map newMap = [];

        foreach (var row in baseMap)
        {
            List<char> newRow = [];
            foreach (var inChar in row)
            {
                if (inChar == Wall) newRow.AddRange([Wall, Wall]);
                if (inChar == Robot) newRow.AddRange([Robot, FreeSpace]);
                if (inChar == FreeSpace) newRow.AddRange([FreeSpace, FreeSpace]);
                if (inChar == Box) newRow.AddRange([BoxLeft, BoxRight]);
            }

            newMap.Add(newRow);
        }

        return newMap;
    }
}