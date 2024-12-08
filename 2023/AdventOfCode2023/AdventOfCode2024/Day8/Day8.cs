using System.Numerics;
using AdventUtils;

namespace AdventOfCode2024.Day8;

public class Day8 : AbstractDay
{
    protected override string[] GetLines() =>
        File.ReadAllLines(
            $"/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day8/input.txt");

    protected override object ProcessPartOne(string[] input)
    {
        HashSet<Vector<int>> antinodes = [];

        var antennas = ParseBoard(input);

        foreach (var positions in antennas.Values)
        {
            var positionPairs =
                from position1 in positions
                from position2 in positions
                where position1 != position2
                select new { PositionOne = position1, PositionTwo = position2 };

            foreach (var pos in positionPairs)
            {
                var positionDelta = pos.PositionTwo - pos.PositionOne;
                var negativePositionDelta = Vector<int>.Zero - positionDelta;
                var antinodePosition = pos.PositionOne + negativePositionDelta;

                antinodes.Add(antinodePosition);
            }
        }

        var filteredAntinodes = FilterAntinodes(input, antinodes);

        return filteredAntinodes.Count();
    }

    protected override object ProcessPartTwo(string[] input)
    {
        HashSet<Vector<int>> antinodes = [];

        var antennas = ParseBoard(input);

        foreach (var positions in antennas.Values)
        {
            var positionPairs =
                from position1 in positions
                from position2 in positions
                where position1 != position2
                select new { PositionOne = position1, PositionTwo = position2 };

            foreach (var pos in positionPairs)
            {
                var positionDelta = pos.PositionTwo - pos.PositionOne;

                for (var i = 1; i < 50 / Min(positionDelta); i++)
                {
                    antinodes.Add(pos.PositionOne + i * positionDelta);
                }
            }
        }

        var filteredAntinodes = FilterAntinodes(input, antinodes);

        return filteredAntinodes.Count();
    }

    private static IEnumerable<Vector<int>> FilterAntinodes(string[] input, HashSet<Vector<int>> antinodes)
    {
        var filteredAntinodes = antinodes.Where(node =>
        {
            if (node[0] < 0) return false;
            if (node[1] < 0) return false;

            if (node[1] >= input.First().Length) return false;
            if (node[0] >= input.Length) return false;

            return true;
        });
        return filteredAntinodes;
    }

    private static Dictionary<char, Vector<int>[]> ParseBoard(string[] input)
    {
        Dictionary<char, Vector<int>[]> antennas = [];
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                var c = input[y][x];
                if (c == '.') continue;

                if (antennas.ContainsKey(c))
                {
                    antennas[c] = [..antennas[c], new Vector<int>([y, x, 0, 0])];
                }
                else
                {
                    antennas[c] = [new Vector<int>([y, x, 0, 0])];
                }
            }
        }

        return antennas;
    }

    private static int Min(Vector<int> p) => Math.Min(Math.Abs(p[0]), Math.Abs(p[1]));
}