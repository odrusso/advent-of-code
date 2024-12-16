using AdventUtils;
using Point = (int Y, int X);
using Plot = (char plant, int plotIndex);

namespace AdventOfCode2024.Day12;

public class Day12 : AbstractDay
{
    private string[] _map;

    // Plot to all points within it
    private readonly Dictionary<Plot, List<Point>> _areaMap = [];

    // Plot to its perimeter
    private readonly Dictionary<Plot, int> _perimeterMap = [];

    // Plot to its number of corners
    private readonly Dictionary<Plot, int> _cornerMap = [];

    protected override string[] GetLines() =>
        File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day12/input.txt");

    public override object ProcessPartOne(string[] input)
    {
        _map = input;

        BuildAreaMap();

        foreach (var (plot, points) in _areaMap)
        {
            _perimeterMap[plot] = 0;

            foreach (var point in points)
            {
                FenceCount(point.Y, point.X, plot);
            }
        }

        return _perimeterMap.Sum(p => p.Value * _areaMap[p.Key].Count);
    }

    private void BuildAreaMap()
    {
        for (int y = 0; y < _map.Length; y++)
        {
            for (int x = 0; x < _map.Length; x++)
            {
                // Check is this point is categorised already
                if (_areaMap.Values.Any(pt => pt.Contains((y, x)))) continue;

                // We're somewhere that we haven't mapped out
                // First we need ot find what area we're about to map
                char plant = _map[y][x];
                int plotIndex = _areaMap.Keys.Count(k => k.plant == plant);
                _areaMap[(plant, plotIndex)] = []; // Create the new area
                TraverseArea(y, x, (plant, plotIndex));
            }
        }
    }

    private void TraverseArea(int y, int x, Plot plot)
    {
        // This position doesn't match the plant, exit.
        if (!Match(y, x, plot.plant)) return;

        // We've already been here, exit.
        if (_areaMap[plot].Contains((y, x))) return;

        // It does match! Add to the area.
        _areaMap[plot].Add((y, x));

        TraverseArea(y - 1, x, plot);
        TraverseArea(y + 1, x, plot);
        TraverseArea(y, x - 1, plot);
        TraverseArea(y, x + 1, plot);
    }

    private void FenceCount(int y, int x, Plot plot)
    {
        int count = 4;

        char plant = _map[y][x];
        if (Match(y - 1, x, plant)) count--; // Check south position
        if (Match(y + 1, x, plant)) count--; // Check north position
        if (Match(y, x - 1, plant)) count--; // Check left position
        if (Match(y, x + 1, plant)) count--; // Check right position

        _perimeterMap[plot] += count;
    }

    private void BuildCornerMap()
    {
        foreach (var (plot, points) in _areaMap)
        {
            _cornerMap[plot] = 0;

            foreach (var point in points)
            {
                // wtf am i doing rn fr
                var northMatch = Match(point.Y + 1, point.X, plot.plant);
                var northRightMatch = Match(point.Y + 1, point.X + 1, plot.plant);
                var rightMatch = Match(point.Y, point.X + 1, plot.plant);
                var rightSouthMatch = Match(point.Y - 1, point.X + 1, plot.plant);
                var southMatch = Match(point.Y - 1, point.X, plot.plant);
                var southLeftMatch = Match(point.Y - 1, point.X + 1, plot.plant);
                var leftMatch = Match(point.Y, point.X - 1, plot.plant);
                var leftNorthMatch = Match(point.Y, point.X - 1, plot.plant);

                bool[] matches = [northMatch, leftMatch, southMatch, rightMatch];
                int matchCount = matches.Count(a => a);

                if (matchCount == 0) // Entirely external
                {
                    _cornerMap[plot] = 4;
                    return;
                }

                // From here... I'm not sure.
            }
        }
    }

    private bool Match(int y, int x, char m)
    {
        if (y < 0 || y > _map.Length - 1) return false;
        if (x < 0 || x > _map[y].Length - 1) return false;
        return _map[y][x] == m;
    }

    public override object ProcessPartTwo(string[] input)
    {
        _map = input;

        BuildAreaMap();

        BuildCornerMap();

        return _perimeterMap.Sum(p => p.Value * _cornerMap[p.Key]);
    }
}