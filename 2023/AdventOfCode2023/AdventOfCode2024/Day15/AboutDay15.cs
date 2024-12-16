using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2024.Day15;

[TestClass]
public class AboutDay15_PartOne
{
    [TestMethod]
    public void TestMicro()
    {
        var input = File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day15/input_micro.txt");

        var result = new Day15().ProcessPartOne(input);
        
        Assert.AreEqual(2028, result);
    }

    [TestMethod]
    public void TestSmall()
    {
        var input = File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day15/input_small.txt");
        
        var result = new Day15().ProcessPartOne(input);
        
        Assert.AreEqual(10092, result);

    }

    [TestMethod]
    public void TestFull()
    {
        var input = File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day15/input.txt");
        
        var result = new Day15().ProcessPartOne(input);

        Assert.AreEqual(1495147, result);
    }
}

[TestClass]
public class AboutDay15_PartTwo
{
    [TestMethod]
    public void TestMicro()
    {
        var input = File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day15/input_micro.txt");

        var result = new Day15().ProcessPartTwo(input);
        
        Assert.AreEqual(2028, result);
    }

    [TestMethod]
    public void TestSmall()
    {
        var input = File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day15/input_small.txt");
        
        var result = new Day15().ProcessPartTwo(input);
        
        Assert.AreEqual(9021, result);
    }

    [TestMethod]
    public void TestFull()
    {
        var input = File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day15/input.txt");
        
        var result = new Day15().ProcessPartTwo(input);

        Assert.AreEqual(0, result);
    }
}