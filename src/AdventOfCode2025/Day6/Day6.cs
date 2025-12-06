using System.Text.RegularExpressions;
using AdventUtils;

namespace AdventOfCode2025.Day6;

public class Day6 : AbstractDay
{
    private readonly struct Line(long[] vals, char op)
    {
        public long Result => op == '+' ? vals.Sum() : vals.Aggregate(1, (long a, long b) => a * b);
    }

    public override object ProcessPartOne(string[] input)
    {
        Line[] lines = ParseInput(input);
        return lines.Sum(line => line.Result);
    }

    public override object ProcessPartTwo(string[] input)
    {
        Line[] lines = ParseInputPartTwo(input);
        return lines.Sum(line => line.Result);
    }

    private static Line[] ParseInput(string[] input)
    {
        List<Line> lines = [];

        var columnLength = Regex.Split(input[0].Trim(), @"\s+").Length;

        for (var columnIndex = 0; columnIndex < columnLength; columnIndex++)
        {
            List<long> lineValues = [];
            char lineOp = '\0';

            for (int rowIndex = 0; rowIndex < input.Length; rowIndex++)
            {
                var row = Regex.Split(input[rowIndex].Trim(), @"\s+");
                var field = row[columnIndex];

                if (Regex.IsMatch(field, "[0-9]+"))
                {
                    lineValues.Add(long.Parse(field));
                }
                else
                {
                    lineOp = field[0];
                }
            }

            lines.Add(new Line(lineValues.ToArray(), lineOp!));
        }

        return lines.ToArray();
    }

    private static Line[] ParseInputPartTwo(string[] input)
    {
        List<Line> lines = [];

        var columnLayoutMatches= Regex.Matches(input.Last(), @"([\+\*]|\s+)");

        (char op, int cols)[] layout = columnLayoutMatches
            .Chunk(2)
            .Select(matches => (matches[0].Value[0], matches[1].Length)).ToArray();

        for (var layoutIndex = 0; layoutIndex < layout.Length; layoutIndex++)
        {
            (char op, int cols) = layout[layoutIndex];

            // We need to CONSUME the next cols + 1 chars from each row, except the last.
            var alreadyConsumedRows = layout[..layoutIndex].Sum(l => l.cols + 1);
            var rowBites = input[..^1].Select(row => row[alreadyConsumedRows..(alreadyConsumedRows+cols)]).ToArray();

            var transposed = Transpose(rowBites
                .Select(s => s.ToCharArray())
                .ToArray());

            var values = transposed
                .Select(chars => new string(chars))
                .Select(long.Parse)
                .ToArray();

            lines.Add(new Line(values, op));
        }

        return lines.ToArray();
    }
    
    public static T[][] Transpose<T>(T[][] matrix)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;

        T[][] result = new T[cols][];

        for (int i = 0; i < cols; i++)
        {
            result[i] = new T[rows];
            for (int j = 0; j < rows; j++)
            {
                result[i][j] = matrix[j][i];
            }
        }

        return result;
    }
}