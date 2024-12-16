using System.Text.RegularExpressions;
using AdventUtils;

namespace AdventOfCode2024.Day14;

public class Day14 : AbstractDay
{
    protected override string[] GetLines() =>
        File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day14/input.txt");

    private const int YMaxIndex = 102;
    private const int XMaxIndex = 100;

    private const int YMiddleIndex = YMaxIndex / 2;
    private const int XMiddleIndex = XMaxIndex / 2;

    const string PartTwoTarget =
        "\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588";


    private record Robot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Dx { get; init; }
        public int Dy { get; init; }
    };

    public override object ProcessPartOne(string[] input)
    {
        Robot[] robots = ParseInput(input);
        List<Robot> movedRobots = [];

        foreach (Robot robot in robots)
        {
            movedRobots.Add(RobotAfter(robot, 100));
        }

        return DetermineScore(movedRobots.ToArray());
    }

    public override object ProcessPartTwo(string[] input)
    {
        Robot[] robots = ParseInput(input);

        Dictionary<int, int> energyMap = [];

        for (int i = 0; i < (XMaxIndex + 1) * (YMaxIndex + 1); i++)
        {
            List<Robot> movedRobots = [];
            foreach (Robot robot in robots)
            {
                movedRobots.Add(RobotAfter(robot, i));
            }

            int energy = GetEnergy(movedRobots.ToArray());
            energyMap[i] = energy;
        }

        // Return the most energetic index
        return energyMap.OrderByDescending(kvp => kvp.Value).First().Key;
    }

    private int GetEnergy(Robot[] robots)
    {
        var energy = 0;

        var grid = Enumerable.Range(0, YMaxIndex + 1).Select(_ => Enumerable.Repeat(false, XMaxIndex + 1).ToArray())
            .ToArray();

        foreach (var robot in robots.ToArray())
        {
            grid[robot.Y][robot.X] = true;
        }

        for (int y = 1; y < YMaxIndex - 1; y++)
        {
            for (int x = 1; x < XMaxIndex - 1; x++)
            {
                if (!grid[y][x]) continue;

                // Check all 8 position around the current pixel, add 1 to energy for value that's true
                if (grid[y - 1][x - 1]) energy++;
                if (grid[y - 1][x]) energy++;
                if (grid[y - 1][x + 1]) energy++;

                if (grid[y][x - 1]) energy++;
                if (grid[y][x + 1]) energy++;

                if (grid[y + 1][x - 1]) energy++;
                if (grid[y + 1][x]) energy++;
                if (grid[y + 1][x + 1]) energy++;
            }
        }

        return energy;
    }

    private static Robot[] ParseInput(string[] input)
    {
        return input.Select(line =>
        {
            var robotMatches = Regex.Matches(line, @"\-?\d+");
            return new Robot
            {
                X = int.Parse(robotMatches[0].Value),
                Y = int.Parse(robotMatches[1].Value),
                Dx = int.Parse(robotMatches[2].Value),
                Dy = int.Parse(robotMatches[3].Value)
            };
        }).ToArray();
    }

    private static Robot RobotAfter(Robot robot, int seconds)
    {
        Robot movedRobot = new();

        var newX = (robot.X + robot.Dx * seconds) % (XMaxIndex + 1);
        newX = newX >= 0 ? newX : XMaxIndex + newX + 1;
        movedRobot.X = newX;

        var newY = (robot.Y + robot.Dy * seconds) % (YMaxIndex + 1);
        newY = newY >= 0 ? newY : YMaxIndex + newY + 1;
        movedRobot.Y = newY;

        return movedRobot;
    }

    private static int DetermineScore(Robot[] robots)
    {
        int count00 = 0;
        int count01 = 0;
        int count10 = 0;
        int count11 = 0;

        foreach (var robot in robots)
        {
            switch (robot)
            {
                case { X: < XMiddleIndex, Y: < YMiddleIndex }:
                    count00++;
                    break;
                case { X: > XMiddleIndex, Y: < YMiddleIndex }:
                    count01++;
                    break;
                case { X: < XMiddleIndex, Y: > YMiddleIndex }:
                    count10++;
                    break;
                case { X: > XMiddleIndex, Y: > YMiddleIndex }:
                    count11++;
                    break;
            }
        }

        return count00 * count01 * count10 * count11;
    }
}