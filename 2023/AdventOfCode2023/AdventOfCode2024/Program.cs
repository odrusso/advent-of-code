using AdventOfCode2024.Day1;
using AdventOfCode2024.Day10;
using AdventOfCode2024.Day11;
using AdventOfCode2024.Day12;
using AdventOfCode2024.Day13;
using AdventOfCode2024.Day2;
using AdventOfCode2024.Day3;
using AdventOfCode2024.Day4;
using AdventOfCode2024.Day5;
using AdventOfCode2024.Day6;
using AdventOfCode2024.Day7;
using AdventOfCode2024.Day8;
using AdventOfCode2024.Day9;
using AdventUtils;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

var days = new Dictionary<string, AbstractDay>();
days.Add("1", new Day1());
days.Add("2", new Day2());
days.Add("3", new Day3());
days.Add("4", new Day4());
days.Add("5", new Day5());
days.Add("6", new Day6());
days.Add("7", new Day7());
days.Add("8", new Day8());
days.Add("9", new Day9());
days.Add("10", new Day10());
days.Add("11", new Day11());
days.Add("12", new Day12());
days.Add("13", new Day13());

Console.WriteLine("Enter day numbers from these options: ");
Console.WriteLine(string.Join(", ", days.Keys));

var daySelection = Console.ReadLine()!;
var dayToRun = days[daySelection];

Console.WriteLine("Part 1 or 2");
var partSelection = Console.ReadLine()!;

switch (partSelection)
{
    case "1": dayToRun.RunPartOne(); break;
    case "2": dayToRun.RunPartTwo(); break;
}

// BenchmarkRunner.Run<Day13>(ManualConfig
//     .Create(DefaultConfig.Instance)
//     .AddDiagnoser(MemoryDiagnoser.Default));