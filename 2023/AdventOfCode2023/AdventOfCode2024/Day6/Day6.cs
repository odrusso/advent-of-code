using AdventUtils;

namespace AdventOfCode2024.Day6;

public class Day6 : AbstractDay
{
    private const char FreeSpace = '.';
    private const char Obstacle = '#';

    private enum Cardinal
    {
        Up = '^',
        Down = 'v',
        Left = '<',
        Right = '>'
    }

    private record Position(int Y, int X);

    private readonly record struct GuardState(Position Position, Cardinal Direction)
    {
        public Position GetNextPosition() => Direction switch
        {
            Cardinal.Up => Position with { Y = Position.Y - 1 },
            Cardinal.Down => Position with { Y = Position.Y + 1 },
            Cardinal.Left => Position with { X = Position.X - 1 },
            Cardinal.Right => Position with { X = Position.X + 1 },
            _ => throw new ArgumentOutOfRangeException() // Get my my algebraic types please!
        };
    }

    protected override string[] GetLines() =>
        File.ReadAllLines(
            $"/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day6/input.txt");

    // 4883
    public override object ProcessPartOne(string[] input)
    {
        var (map, state) = GetInitialGuardState(input);

        HashSet<Position> uniquePositions = [];
        while (InMapArea(state.Position, map))
        {
            state = UpdateGuardPosition(state, map);
            uniquePositions.Add(state.Position);
        }

        return uniquePositions.Count;
    }

    // 1656 TOO HIGH
    // 1655 i s2g
    public override object ProcessPartTwo(string[] input)
    {
        var (map, state) = GetInitialGuardState(input);

        // We'll need this again later.
        var startingState = state with { };

        HashSet<Position> uniquePositions = [];
        while (InMapArea(state.Position, map))
        {
            state = UpdateGuardPosition(state, map);
            uniquePositions.Add(state.Position);
        }

        // Try and put a obstacle everywhere along the OG path, then run through again and see what happens.

        HashSet<Position> cyclicObstacleLocations = [];

        foreach (var obstaclePosition in uniquePositions)
        {
            // Console.WriteLine($"Obstacle position: {obstaclePosition}");

            // Put the guard back in his starting spot
            state = startingState with { };

            HashSet<GuardState> newUniquePositions = [];

            // Create a new map, with the new obstacle
            var mapWithObstacle = MapWithObstacle(map, obstaclePosition);

            while (InMapArea(state.Position, mapWithObstacle))
            {
                state = UpdateGuardPosition(state, mapWithObstacle);

                if (newUniquePositions.Add(state)) continue;

                // Didn't add any new unique position, so we've been here before.
                cyclicObstacleLocations.Add(obstaclePosition);
                break;
            }
            // If we're here, it means the guard left the map, and therefore the route isn't cyclic
        }

        // Have to remove the starting position, if it exists
        return cyclicObstacleLocations.Count(p => p != startingState.Position);
    }

    private static string[] MapWithObstacle(string[] map, Position obstaclePosition)
    {
        var cloneMap = (string[])map.Clone();

        cloneMap[obstaclePosition.Y] =
            map[obstaclePosition.Y].Remove(obstaclePosition.X, 1).Insert(obstaclePosition.X, Obstacle.ToString());

        return cloneMap;
    }

    private static (string[], GuardState) GetInitialGuardState(string[] map)
    {
        GuardState state = default!;

        // look throw all the rows and cols until we find out guard.
        for (var y = 0; y < map.Length; y++)
        {
            var mapRow = map[y];

            if (!mapRow.Contains('^')) continue;

            state = new GuardState(new Position(y, mapRow.IndexOf('^')), Cardinal.Up);
        }

        map[state.Position.Y] = map[state.Position.Y].Replace('^', FreeSpace);

        return (map, state);
    }

    private static bool InMapArea(Position pos, string[] map)
    {
        return pos.Y > 0 && pos.Y < map.Length - 1 && pos.X > 0 && pos.X < map[0].Length - 1;
    }

    private static GuardState UpdateGuardPosition(GuardState state, string[] map)
    {
        var nextPosition = state.GetNextPosition();
        var nextChar = GetMapChar(nextPosition, map);

        return (nextChar, state) switch
        {
            (FreeSpace, _) => state with { Position = nextPosition },
            (Obstacle, { Direction: Cardinal.Up }) => state with { Direction = Cardinal.Right },
            (Obstacle, { Direction: Cardinal.Right }) => state with { Direction = Cardinal.Down },
            (Obstacle, { Direction: Cardinal.Down }) => state with { Direction = Cardinal.Left },
            (Obstacle, { Direction: Cardinal.Left }) => state with { Direction = Cardinal.Up },
            _ => throw new ArgumentOutOfRangeException() // again with the types, smh
        };
    }

    private static char GetMapChar(Position pos, string[] map)
    {
        return map[pos.Y][pos.X];
    }
}