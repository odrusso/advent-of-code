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

    protected override object ProcessPartOne(string[] input)
    {
        Robot[] robots = ParseInput(input);
        List<Robot> movedRobots = [];

        foreach (Robot robot in robots)
        {
            movedRobots.Add(RobotAfter(robot, 100));
        }

        return DetermineScore(movedRobots.ToArray());
    }

    protected override object ProcessPartTwo(string[] input)
    {
        Robot[] robots = ParseInput(input);
        var i = 6390;

        while (true)
        {
            i++;
            List<Robot> movedRobots = [];

            foreach (Robot robot in robots)
            {
                movedRobots.Add(RobotAfter(robot, i));
            }

            var grid = Enumerable.Range(0, YMaxIndex + 1).Select(_ => Enumerable.Repeat(' ', XMaxIndex + 1).ToArray())
                .ToArray();

            foreach (var robot1 in movedRobots.ToArray())
            {
                grid[robot1.Y][robot1.X] = '\u2588';
            }

            foreach (var row in grid)
            {
                if (string.Join("", row).Contains(PartTwoTarget))
                {
                    return i;
                }
            }

            // Console.Clear();
            // Console.WriteLine(string.Join("\n", grid.Select(a => string.Join("", a))));
            // Console.WriteLine(i);
            // Console.ReadLine();
            // 
            // if some tree condition, exit.
        }
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