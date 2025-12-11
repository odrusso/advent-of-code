using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2025.Day11;

[TestClass]
public class AboutDay11
{
    private static string[] ReadInput(string filename, [CallerFilePath] string? filePath = null)
    {
        var directory = Path.GetDirectoryName(filePath);
        var inputPath = Path.Combine(directory!, filename);
        return File.ReadAllLines(inputPath);
    }

    [TestMethod]
    public void PartOne_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day11().ProcessPartOne(input);

        Assert.AreEqual(5L, result);
    }

    [TestMethod]
    public void PartOne()
    {
        var input = ReadInput("input.txt");

        var result = new Day11().ProcessPartOne(input);

        Assert.AreEqual(658L, result);
    }

    [TestMethod]
    public void PartTwo_Micro()
    {
        string[] input =
        [
            "svr: aaa bbb",
            "aaa: fft",
            "fft: ccc",
            "bbb: tty",
            "tty: ccc",
            "ccc: ddd eee",
            "ddd: hub",
            "hub: fff",
            "eee: dac",
            "dac: fff",
            "fff: ggg hhh",
            "ggg: out",
            "hhh: out"
        ];

        var result = new Day11().ProcessPartTwo(input);

        Assert.AreEqual(2L, result);
    }

    [TestMethod]
    public void PartTwo()
    {
        var input = ReadInput("input.txt");

        var result = new Day11().ProcessPartTwo(input);

        Assert.AreEqual(371113003846800L, result);
    }
}