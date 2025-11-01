using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2024.Day15;

[TestClass]
public class AboutDay15_PartOne
{
    private static string[] ReadInput(string filename, [CallerFilePath] string? filePath = null)
    {
        var directory = Path.GetDirectoryName(filePath);
        var inputPath = Path.Combine(directory!, filename);
        return File.ReadAllLines(inputPath);
    }

    [TestMethod]
    public void TestMicro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day15().ProcessPartOne(input);
        
        Assert.AreEqual(2028, result);
    }

    [TestMethod]
    public void TestSmall()
    {
        var input = ReadInput("input_small.txt");
        
        var result = new Day15().ProcessPartOne(input);
        
        Assert.AreEqual(10092, result);

    }

    [TestMethod]
    public void TestFull()
    {
        var input = ReadInput("input.txt");
        
        var result = new Day15().ProcessPartOne(input);

        Assert.AreEqual(1495147, result);
    }
}

[TestClass]
public class AboutDay15_PartTwo
{
    private static string[] ReadInput(string filename, [CallerFilePath] string? filePath = null)
    {
        var directory = Path.GetDirectoryName(filePath);
        var inputPath = Path.Combine(directory!, filename);
        return File.ReadAllLines(inputPath);
    }

    [TestMethod]
    [Ignore]
    public void TestMicro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day15().ProcessPartTwo(input);
        
        Assert.AreEqual(2028, result);
    }

    [TestMethod]
    [Ignore]
    public void TestSmall()
    {
        var input = ReadInput("input_small.txt");
        
        var result = new Day15().ProcessPartTwo(input);
        
        Assert.AreEqual(9021, result);
    }

    [TestMethod]
    [Ignore]
    public void TestFull()
    {
        var input = ReadInput("input.txt");
        
        var result = new Day15().ProcessPartTwo(input);

        Assert.AreEqual(0, result);
    }
}