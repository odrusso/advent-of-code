using AdventUtils;

namespace AdventOfCode2024.Day10;

public class Day10 : AbstractDay
{
    private HashSet<(int, int)> _visited;
    private int[][] _map;

    public override object ProcessPartOne(string[] input)
    {
        _map = ParseInput(input);

        int routes = 0;

        for (var y = 0; y < _map.Length; y++)
        {
            for (var x = 0; x < _map[y].Length; x++)
            {
                int value = _map[y][x];
                if (value != 0) continue;

                _visited = [];

                // Ugh, not very DRY 
                TraverseMapRecursive(value, y - 1, x); // North
                TraverseMapRecursive(value, y + 1, x); // South
                TraverseMapRecursive(value, y, x - 1); // Left
                TraverseMapRecursive(value, y, x + 1); // Right

                routes += +_visited.Count;
            }
        }

        return routes;
    }

    private void TraverseMapRecursive(int last, int y, int x)
    {
        if (y < 0 || y >= _map.Length) return; // Out of bounds
        if (x < 0 || x >= _map[y].Length) return; // Out of bounds

        var next = _map[y][x];

        if (next != last + 1) return; // Not a valid path, exit
        if (next == 9)
        {
            _visited.Add((x, y));
            return; // Successful Leaf node
        }

        TraverseMapRecursive(next, y - 1, x); // North
        TraverseMapRecursive(next, y + 1, x); // South
        TraverseMapRecursive(next, y, x - 1); // Left
        TraverseMapRecursive(next, y, x + 1); // Right
    }

    public override object ProcessPartTwo(string[] input)
    {
        _map = ParseInput(input);

        int routes = 0;

        for (var y = 0; y < _map.Length; y++)
        {
            for (var x = 0; x < _map[y].Length; x++)
            {
                int value = _map[y][x];
                if (value != 0) continue;

                routes +=
                    TraverseMapRecursivePartTwo(value, y - 1, x) + // North
                    TraverseMapRecursivePartTwo(value, y + 1, x) + // South
                    TraverseMapRecursivePartTwo(value, y, x - 1) + // Left
                    TraverseMapRecursivePartTwo(value, y, x + 1); // Right
            }
        }

        return routes;
    }

    private int TraverseMapRecursivePartTwo(int last, int y, int x)
    {
        if (y < 0 || y >= _map.Length) return 0; // Out of bounds
        if (x < 0 || x >= _map[y].Length) return 0; // Out of bounds

        var next = _map[y][x];

        if (next != last + 1) return 0; // Not a valid path, exit
        if (next == 9) return 1;

        return
            TraverseMapRecursivePartTwo(next, y - 1, x) + // North
            TraverseMapRecursivePartTwo(next, y + 1, x) + // South
            TraverseMapRecursivePartTwo(next, y, x - 1) + // Left
            TraverseMapRecursivePartTwo(next, y, x + 1); // Right
    }

    private static int[][] ParseInput(string[] input) =>
        input.Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
}