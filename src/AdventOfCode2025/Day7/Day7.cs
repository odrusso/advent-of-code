using AdventUtils;

namespace AdventOfCode2025.Day7;

public class Day7 : AbstractDay
{
    private readonly Dictionary<(int y, int x), long> _memo = [];

    public override object ProcessPartOne(string[] input)
    {
        var grid = ToGrid(input);

        int splits = 0;

        for (int y = 1; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                var cell = grid[y, x];
                var cellAbove = grid[y - 1, x];

                if (cellAbove == 'S')
                {
                    grid[y, x] = '|';
                }

                if (cellAbove == '|')
                {
                    // We either need to continue it down (if we're blank), or split (if we're a splitter)
                    if (cell == '.')
                    {
                        grid[y, x] = '|';
                    } else if (cell == '^')
                    {
                        grid[y, x - 1] = '|';
                        grid[y, x + 1] = '|';
                        splits++;
                    }
                }
            }
        }

        return splits;
    }

    public override object ProcessPartTwo(string[] input)
    {
        var grid = ToGrid(input);

        (int y, int x) = GetStartingPosition(grid);

        return AllPaths(grid, y, x);
    }

    private long AllPaths(char[,] grid, int y, int x)
    {
        // Base case, end of the grid.
        if (y == grid.GetLength(0) - 1) return 1;

        // Check the memo
        if (_memo.ContainsKey((y, x))) return _memo[(y, x)];

        long result;
        var cellBelow = grid[y + 1, x];

        if (cellBelow == '.')
        {
            result = AllPaths(grid, y + 1, x);
        }
        else
        {
            // Must be at a ^ if not a .
            result = AllPaths(grid, y + 1, x - 1) + AllPaths(grid, y - 1, x + 1);
        }

        _memo[(y, x)] = result;
        return result;
    }

    private static (int y, int x) GetStartingPosition(char[,] grid)
    {
        for (int x = 0; x < grid.GetLength(1); x++)
        {
            if (grid[0, x] == 'S') return (1, x);
        }

        return (-1, -1);
    }

    private static char[,] ToGrid(string[] input)
    {
        char[,] grid = new char[input.Length, input[0].Length];

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                grid[i, j] = input[i][j];
            }
        }

        return grid;
    }
}