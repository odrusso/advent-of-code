using AdventUtils;

namespace AdventOfCode2024.Day4;

public class Day4 : AbstractDay
{
    private const string SearchString = "XMAS";
    private const string PartTwoSearchString = "MAS";

    protected override string[] GetLines() =>
        File.ReadAllLines(
            $"/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day4/input.txt");

    private record struct Cardinal(string Name, int YStep, int XStep)
    {
        public string Name = Name;
        public int YStep = YStep;
        public int XStep = XStep;
    }

    private static readonly Cardinal[] SquareCardinals =
    [
        new("E", 0, 1),
        new("N", -1, 0),
        new("W", 0, -1),
        new("S", 1, 0),
    ];

    private static readonly Cardinal[] AngleCardinals =
    [
        new("NE", -1, 1),
        new("NW", -1, -1),
        new("SW", 1, -1),
        new("SE", 1, 1),
    ];

    private static readonly Cardinal[] AllCardinals = [..SquareCardinals, ..AngleCardinals];

    protected override object ProcessPartOne(string[] input)
    {
        char[][] matrix = input.Select(l => l.ToCharArray()).ToArray();

        int xmasCount = 0;

        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                xmasCount += AllCardinals.Count(cardinal => MatchesCardinal(y, x, cardinal, matrix, SearchString));
            }
        }

        return xmasCount;
    }

    protected override object ProcessPartTwo(string[] input)
    {
        char[][] matrix = input.Select(l => l.ToCharArray()).ToArray();

        int xmasCount = 0;

        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                var numMatchingCardinals = AngleCardinals.Count(cardinal =>
                {
                    var adjustY = -1 * cardinal.YStep;
                    var adjustX = -1 * cardinal.XStep;
                    return MatchesCardinal(y + adjustY, x + adjustX, cardinal, matrix, PartTwoSearchString);
                });

                if (numMatchingCardinals > 0)
                {
                    xmasCount--;
                    xmasCount++;
                }

                if (numMatchingCardinals == 2) xmasCount++;
            }
        }

        return xmasCount;
    }

    private static bool MatchesCardinal(int y, int x, Cardinal cardinal, char[][] matrix, string searchString)
    {
        for (int cardinalStep = 0; cardinalStep < searchString.Length; cardinalStep++)
        {
            int stepY = cardinalStep * cardinal.YStep;
            int stepX = cardinalStep * cardinal.XStep;
            char character = searchString[cardinalStep];

            var matrixCharacter = GetValue(y + stepY, x + stepX, matrix);

            var charactersMatch = CharMatch(matrixCharacter, character);

            if (!charactersMatch)
            {
                return false;
            }
        }

        return true;
    }

    private static char? GetValue(int y, int x, char[][] m)
    {
        if (x < 0 || y < 0) return null;
        return m.ElementAtOrDefault(y)?.ElementAtOrDefault(x);
    }

    private static bool CharMatch(char? m, char? k)
    {
        if (m == null || k == null) return false;
        return m == k;
    }
}