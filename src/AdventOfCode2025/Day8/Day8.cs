using AdventUtils;

namespace AdventOfCode2025.Day8;

public class Day8 : AbstractDay
{
    private record Point(long X, long Y, long Z)
    {
        public long SquareDistance(Point other)
        {
            return (X - other.X) * (X - other.X) +
                   (Y - other.Y) * (Y - other.Y) +
                   (Z - other.Z) * (Z - other.Z);
        }
    }

    private class KNodeDistanceMap(int k)
    {
        // TODO, we can probably do this easier if we just keep the list sorted

        private readonly List<(long distance, Point a, Point b)> _map = [];
        private long _maximumDistance = -1;

        public void Add(long distance, Point a, Point b)
        {
            if (_map.Count < k)
            {
                _map.Add((distance, a, b));
                _maximumDistance = Math.Max(_maximumDistance, distance);
                return;
            }

            if (distance >= _maximumDistance) return;

            // We have something shorter, so we need to bump the current max
            var indexToRemove = _map.FindIndex(d => d.distance == _maximumDistance);
            _map.RemoveAt(indexToRemove);
            _map.Add((distance, a, b));
            _maximumDistance = _map.Select(c => c.distance).Max();
        }

        public (Point left, Point right)[] GetSorted()
        {
            return _map
                .OrderBy(a => a.distance)
                .Select(a => (a.a, a.b))
                .ToArray();
        }
    }

    public override object ProcessPartOne(string[] input)
    {
        var connectionCount = int.Parse(input[0]);

        var points = ParseInput(input[1..]);

        KNodeDistanceMap distanceMap = new KNodeDistanceMap(connectionCount);

        for (var leftIndex = 0; leftIndex < points.Length - 1; leftIndex++)
        {
            var leftPoint = points[leftIndex];

            for (var rightIndex = leftIndex + 1; rightIndex < points.Length; rightIndex++)
            {
                var rightPoint = points[rightIndex];

                // Might not need this check anymore tbh
                if (leftPoint == rightPoint) continue;

                var distance = leftPoint.SquareDistance(rightPoint);

                distanceMap.Add(distance, leftPoint, rightPoint);
            }
        }

        var connectionsToMake = distanceMap.GetSorted();

        List<List<Point>> circuits = [];

        foreach (var connection in connectionsToMake)
        {
            // See if either of the points in this connection are already in a circuit
            var leftExistingIndex = circuits.FindIndex(c => c.Contains(connection.left));
            var rightExistingIndex = circuits.FindIndex(c => c.Contains(connection.right));

            if (leftExistingIndex == -1 && rightExistingIndex == -1)
            {
                // No existing connections
                circuits.Add([connection.left, connection.right]);
            }
            else if (leftExistingIndex != -1 && rightExistingIndex == -1)
            {
                // Left in a circuit, right isn't, just add right
                circuits[leftExistingIndex].Add(connection.right);
            }
            else if (leftExistingIndex == -1 && rightExistingIndex != -1)
            {
                // Right in a circuit, left isn't, just add left
                circuits[rightExistingIndex].Add(connection.left);
            }
            else if (leftExistingIndex != -1 && rightExistingIndex != -1)
            {
                // Don't do anything if they're already together
                if (leftExistingIndex == rightExistingIndex) continue;

                // Both are in different circuits, join them together (to the left)
                circuits[leftExistingIndex].AddRange(circuits[rightExistingIndex]);
                circuits.RemoveAt(rightExistingIndex);
            }
        }

        return circuits
            .OrderBy(c => c.Count)
            .TakeLast(3)
            .Aggregate(1, (running, list) => running * list.Count);
    }

    public override object ProcessPartTwo(string[] input)
    {
        var points = ParseInput(input[1..]);

        // Going big because I don't know what this should be. 10 works for both micro and full sets. Shrug.
        var maxConnectionCount = input.Length * 10;

        KNodeDistanceMap distanceMap = new KNodeDistanceMap(maxConnectionCount);

        for (var leftIndex = 0; leftIndex < points.Length - 1; leftIndex++)
        {
            var leftPoint = points[leftIndex];

            for (var rightIndex = leftIndex + 1; rightIndex < points.Length; rightIndex++)
            {
                var rightPoint = points[rightIndex];

                // Might not need this check anymore tbh
                if (leftPoint == rightPoint) continue;

                var distance = leftPoint.SquareDistance(rightPoint);

                distanceMap.Add(distance, leftPoint, rightPoint);
            }
        }

        var connectionsToMake = distanceMap.GetSorted();

        List<List<Point>> circuits = [];

        foreach (var connection in connectionsToMake)
        {
            // See if either of the points in this connection are already in a circuit
            var leftExistingIndex = circuits.FindIndex(c => c.Contains(connection.left));
            var rightExistingIndex = circuits.FindIndex(c => c.Contains(connection.right));

            if (leftExistingIndex == -1 && rightExistingIndex == -1)
            {
                // No existing connections
                circuits.Add([connection.left, connection.right]);
            }
            else if (leftExistingIndex != -1 && rightExistingIndex == -1)
            {
                // Left in a circuit, right isn't, just add right
                circuits[leftExistingIndex].Add(connection.right);
            }
            else if (leftExistingIndex == -1 && rightExistingIndex != -1)
            {
                // Right in a circuit, left isn't, just add left
                circuits[rightExistingIndex].Add(connection.left);
            }
            else if (leftExistingIndex != -1 && rightExistingIndex != -1)
            {
                // Don't do anything if they're already together
                if (leftExistingIndex != rightExistingIndex)
                {
                    // Both are in different circuits, join them together (to the left)
                    circuits[leftExistingIndex].AddRange(circuits[rightExistingIndex]);
                    circuits.RemoveAt(rightExistingIndex);
                }
            }

            if (circuits.Count == 1 && circuits.First().Count == points.Length)
            {
                return connection.left.X * connection.right.X;
            }
        }

        return -1;
    }

    private static Point[] ParseInput(string[] input) =>
        input
            .Select(pointsString =>
            {
                var pointsArr = pointsString.Split(",").Select(long.Parse).ToArray();
                return new Point(pointsArr[0], pointsArr[1], pointsArr[2]);
            }).ToArray();
}