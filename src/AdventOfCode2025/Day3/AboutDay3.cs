using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2025.Day3;

[TestClass]
public class AboutDay3
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

        var result = new Day3().ProcessPartOne(input);

        Assert.AreEqual(357L, result);
    }

    [DataTestMethod]
    [DataRow("987654321111111", 98L)]
    [DataRow("811111111111119", 89L)]
    [DataRow("234234234234278", 78L)]
    [DataRow("818181911112111", 92L)]
    public void PartOne_IndividualCases(string bank, long expected)
    {
        var input = new[] { bank };

        var result = new Day3().ProcessPartOne(input);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void PartOne()
    {
        var input = ReadInput("input.txt");

        var result = new Day3().ProcessPartOne(input);

        Assert.AreEqual(17694L, result);
    }

    [TestMethod]
    public void PartTwo_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day3().ProcessPartTwo(input);

        Assert.AreEqual(3121910778619L, result);
    }

    [TestMethod]
    public void PartTwo()
    {
        var input = ReadInput("input.txt");

        var result = new Day3().ProcessPartTwo(input);

        Assert.AreEqual(175659236361660L, result);
    }
}