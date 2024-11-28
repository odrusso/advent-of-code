// See https://aka.ms/new-console-template for more information

using AdventOfCode2023.Day1;
using AdventOfCode2023.Utils;

var days = new Dictionary<string, AbstractDay>();
days.Add("1", new Day1());

Console.WriteLine("Enter day numbers from these options: ");
Console.WriteLine(String.Join(", ", days.Keys));

var daySelection = Console.ReadLine()!;
var dayToRun = days[daySelection];

Console.WriteLine("Part 1 or 2");
var partSelection = Console.ReadLine()!;

switch (partSelection)
{
    case "1": dayToRun.RunPartOne(); break;
    case "2": dayToRun.RunPartTwo(); break;
}