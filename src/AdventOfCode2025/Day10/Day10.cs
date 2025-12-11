using AdventUtils;

namespace AdventOfCode2025.Day10;

public class Day10 : AbstractDay
{
    private record Machine(uint Target, uint[] Buttons);

    private record VectorMachine(uint[] Target, uint[][] Buttons);

    public override object ProcessPartOne(string[] input)
    {
        Machine[] machines = ParseInput(input);

        long count = 0;

        foreach (var machine in machines)
        {
            long bestOption = long.MaxValue;

            foreach (var subsetA in SubSetsOf(machine.Buttons))
            {
                var subset = subsetA.ToArray();

                var testTarget = 0u;

                foreach (var subsetButton in subset)
                {
                    testTarget ^= subsetButton;
                }

                if (testTarget != machine.Target) continue;

                if (bestOption > subset.Count())
                {
                    bestOption = subset.Count();
                }
            }

            count += bestOption;
        }

        return count;
    }

    public override object ProcessPartTwo(string[] input)
    {
        var machines = ParseInputVectors(input);

        long presses = 0;

        foreach (var machine in machines)
        {
            int[][] matrix = BuildAugmentedMatrix(machine.Buttons, machine.Target);
            presses += SolveMachine(matrix);
        }

        return presses;
    }

    private int SolveMachine(int[][] matrix)
    {
        int[][] reduced = Reduce(matrix);

        // Identify free variables
        List<int> freeVars = FindFreeVariables(reduced);

        if (freeVars.Count == 0)
        {
            // No free variables - direct back substitution
            int[] solution = BackSub(reduced);
            return solution[0] == -1 ? -1 : solution.Sum();
        }

        // Search over free variable values
        return SearchFreeVariables(reduced, freeVars);
    }

    private static List<int> FindFreeVariables(int[][] matrix)
    {
        int numVars = matrix[0].Length - 1;
        bool[] hasPivot = new bool[numVars];

        foreach (var r in matrix)
        {
            int pivotCol = Array.FindIndex(r, 0, numVars, x => x != 0);
            if (pivotCol != -1)
            {
                hasPivot[pivotCol] = true;
            }
        }

        List<int> freeVars = [];
        for (int i = 0; i < numVars; i++)
        {
            if (!hasPivot[i])
            {
                freeVars.Add(i);
            }
        }

        return freeVars;
    }

    private static int[] BackSub(int[][] matrix, int[]? freeVars = null, int[]? freeVarValues = null)
    {
        var coefficients = new int[matrix[0].Length - 1];

        if (freeVars != null)
        {
            for (int i = 0; i < freeVars.Length; i++)
            {
                coefficients[freeVars[i]] = freeVarValues![i];
            }
        }

        // Go through each row, starting at the end
        for (int row = matrix.Length - 1; row >= 0; row--)
        {
            int pivotCol = matrix[row]
                .Select((_, i) => i)
                .FirstOr(i => matrix[row][i] != 0, -1);

            // This is a zero row, skip it
            if (pivotCol == -1) continue;

            int value = matrix[row][^1];

            for (int col = pivotCol + 1; col < matrix[0].Length - 1; col++)
            {
                value -= matrix[row][col] * coefficients[col];
            }

            if (value % matrix[row][pivotCol] != 0 || value / matrix[row][pivotCol] < 0)
            {
                return [-1];
            }

            coefficients[pivotCol] = value / matrix[row][pivotCol];
        }

        return coefficients;
    }

    private static Machine[] ParseInput(string[] input)
    {
        List<Machine> machines = [];

        foreach (var line in input)
        {
            var targetString =
                new string(line.Split("]")[0][1..].Replace(".", "0").Replace("#", "1").Reverse().ToArray());
            var target = Convert.ToUInt16(targetString, 2);

            var buttonStrings = line.Split(" ")[1..^1].Select(s => s.Replace("(", "").Replace(")", "").Split(","));

            List<uint> buttons = [];

            foreach (var buttonString in buttonStrings)
            {
                uint button = 0;
                foreach (var bs in buttonString)
                {
                    button ^= 1U << int.Parse(bs);
                }

                buttons.Add(button);
            }

            machines.Add(new Machine(target, buttons.ToArray()));
        }

        return machines.ToArray();
    }

    private static VectorMachine[] ParseInputVectors(string[] input)
    {
        List<VectorMachine> machines = [];

        foreach (var line in input)
        {
            var target = line.Split("{")[1][..^1].Split(",").Select(uint.Parse).ToArray();

            var buttonStrings = line.Split(" ")[1..^1].Select(s => s.Replace("(", "").Replace(")", "").Split(","));

            List<uint[]> buttons = [];
            foreach (var buttonString in buttonStrings)
            {
                string[] button = Enumerable.Repeat("0", target.Length).ToArray();
                foreach (var bs in buttonString)
                {
                    button[int.Parse(bs)] = "1";
                }

                uint[] vectorButton = button.Select(uint.Parse).ToArray();

                buttons.Add(vectorButton);
            }

            machines.Add(new VectorMachine(target, buttons.ToArray()));
        }

        return machines.ToArray();
    }

    // I stole this because cbf
    private static IEnumerable<IEnumerable<T>> SubSetsOf<T>(IEnumerable<T> source)
    {
        if (!source.Any())
            return Enumerable.Repeat(Enumerable.Empty<T>(), 1);

        var element = source.Take(1);

        var haveNots = SubSetsOf(source.Skip(1));
        var haves = haveNots.Select(set => element.Concat(set));

        return haves.Concat(haveNots);
    }

    private static int[][] Reduce(int[][] matrix)
    {
        int currentRow = 0;
        for (int c = 0; c < matrix[0].Length; c++)
        {
            // Find a row at or below current_row with a non-zero value in column c
            var pivotRow = matrix[currentRow..]
                .Select((_, i) => i + currentRow)
                .FirstOr(i => matrix[i][c] != 0, -1);

            // This column has no pivot, move to next column
            if (pivotRow == -1) continue;

            // Swap the pivot row to current_row
            (matrix[currentRow], matrix[pivotRow]) = (matrix[pivotRow], matrix[currentRow]);

            // Ensure pivot is positive
            if (matrix[currentRow][c] < 0)
            {
                for (int col = 0; col < matrix[0].Length; col++)
                {
                    matrix[currentRow][col] *= -1;
                }
            }

            // # Eliminate all rows BELOW current_row
            for (int r = currentRow + 1; r < matrix.Length; r++)
            {
                if (matrix[r][c] != 0)
                {
                    int pivotValue = matrix[currentRow][c];
                    int targetValue = matrix[r][c];

                    for (int col = 0; col < matrix[0].Length; col++)
                    {
                        matrix[r][col] = matrix[r][col] * pivotValue - matrix[currentRow][col] * targetValue;
                    }
                }
            }

            currentRow++;
        }

        return matrix;
    }

    private static int[][] BuildAugmentedMatrix(uint[][] buttons, uint[] target)
    {
        int numEquations = target.Length;
        int numVariables = buttons.Length;

        // Create augmented matrix: numEquations rows x (numVariables + 1) columns
        int[][] matrix = new int[numEquations][];

        for (int row = 0; row < numEquations; row++)
        {
            matrix[row] = new int[numVariables + 1];

            // Fill in button values as columns
            for (int col = 0; col < numVariables; col++)
            {
                matrix[row][col] = (int)buttons[col][row];
            }

            // Fill in target as the last column
            matrix[row][numVariables] = (int)target[row];
        }

        return matrix;
    }

    private int SearchFreeVariables(int[][] matrix, List<int> freeVars)
    {
        int minPresses = int.MaxValue;

        // Determine upper bounds for each free variable
        int maxBound = 200; // Conservative upper bound

        // Generate all combinations of free variable values
        foreach (var freeVarValues in GenerateCombinations(freeVars.Count, maxBound))
        {
            // Try back substitution with these free variable values
            int[] solution = BackSub(matrix, freeVars.ToArray(), freeVarValues);

            if (solution[0] != -1) // Valid solution
            {
                int totalPresses = solution.Sum();
                minPresses = Math.Min(minPresses, totalPresses);
            }
        }

        return minPresses == int.MaxValue ? -1 : minPresses;
    }

    private IEnumerable<int[]> GenerateCombinations(int numFreeVars, int maxBound)
    {
        if (numFreeVars == 1)
        {
            for (int i = 0; i <= maxBound; i++)
                yield return [i];
        }
        else if (numFreeVars == 2)
        {
            for (int i = 0; i <= maxBound; i++)
            for (int j = 0; j <= maxBound; j++)
                yield return [i, j];
        }
        else if (numFreeVars == 3)
        {
            for (int i = 0; i <= maxBound; i++)
            for (int j = 0; j <= maxBound; j++)
            for (int k = 0; k <= maxBound; k++)
                yield return [i, j, k];
        }
    }
}

static class ExtensionsThatWillAppearOnIEnumerables
{
    public static T FirstOr<T>(this IEnumerable<T> source, Func<T, bool> pred, T alternate)
    {
        foreach (var t in source)
            if (pred(t))
                return t;
        return alternate;
    }
}