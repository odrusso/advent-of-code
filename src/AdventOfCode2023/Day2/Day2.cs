using System.Text.RegularExpressions;
using AdventUtils;
using static System.Int32;

namespace AdventOfCode2023.Day2;

public class Day2 : AbstractDay
{
    private const int MaxRed = 12;
    private const int MaxGreen = 13;
    private const int MaxBlue = 14;

    public override object ProcessPartOne(string[] input) =>
        input.Sum(line =>
        {
            var game = ParseLine(line);

            var gameValid = game.Rounds.All(round => round switch
            {
                { Red: > MaxRed } => false,
                { Green: > MaxGreen } => false,
                { Blue: > MaxBlue } => false,
                _ => true,
            });

            return gameValid ? game.Id : 0;
        });

    public override object ProcessPartTwo(string[] input) =>
        input.Sum(line =>
        {
            var game = ParseLine(line);

            var minRed = game.Rounds.Max(round => round.Red);
            var minGreen = game.Rounds.Max(round => round.Green);
            var minBlue = game.Rounds.Max(round => round.Blue);

            return minRed * minGreen * minBlue;
        });

    private static Game ParseLine(string line)
    {
        var prefix = Regex.Match(line, @"^Game (\d+): ");
        line = line.Replace(prefix.Value, string.Empty);

        var games = line.Split("; ").Select(gameString =>
        {
            var redCount = Regex.Match(gameString, @"(\d+) red").Groups[1].Value;
            var greenCount = Regex.Match(gameString, @"(\d+) green").Groups[1].Value;
            var blueCount = Regex.Match(gameString, @"(\d+) blue").Groups[1].Value;

            TryParse(redCount, out var red);
            TryParse(greenCount, out var green);
            TryParse(blueCount, out var blue);

            return new Round(red, green, blue);
        });

        return new Game(Parse(prefix.Groups[1].Value), games);
    }

    private record Game(int Id, IEnumerable<Round> Rounds);

    private record Round(int Red, int Green, int Blue);
}