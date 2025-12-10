using AdventUtils;

namespace AdventOfCode2025.Day9;

public class Day9 : AbstractDay
{
    private record Point(long X, long Y)
    {
        public long SquareSize(Point other)
        {
            return (Math.Abs(X - other.X) + 1) * (Math.Abs(Y - other.Y) + 1);
        }
    }

    public override object ProcessPartOne(string[] input)
    {
        var points = ParseInput(input);

        long maxSquareSize = -1;

        for (var leftIndex = 0; leftIndex < points.Length - 1; leftIndex++)
        {
            var leftPoint = points[leftIndex];

            for (var rightIndex = leftIndex + 1; rightIndex < points.Length; rightIndex++)
            {
                var rightPoint = points[rightIndex];

                var size = leftPoint.SquareSize(rightPoint);

                if (size > maxSquareSize) maxSquareSize = size;
            }
        }

        return maxSquareSize;
    }

    public override object ProcessPartTwo(string[] input)
    {
        var points = ParseInput(input);

        long maxSquareSize = -1;

        for (var leftIndex = 0; leftIndex < points.Length - 1; leftIndex++)
        {
            var leftPoint = points[leftIndex];

            for (var rightIndex = leftIndex + 1; rightIndex < points.Length; rightIndex++)
            {
                var rightPoint = points[rightIndex];

                var size = leftPoint.SquareSize(rightPoint);

                if (size <= maxSquareSize) continue;

                if (!EnclosedSimple(points, leftPoint, rightPoint)) continue;

                maxSquareSize = size;
            }
        }

        return maxSquareSize;
    }

    private bool EnclosedSimple(Point[] points, Point rectA, Point rectB)
    {
        var rectMinX = Math.Min(rectA.X, rectB.X);
        var rectMaxX = Math.Max(rectA.X, rectB.X);
        var rectMinY = Math.Min(rectA.Y, rectB.Y);
        var rectMaxY = Math.Max(rectA.Y, rectB.Y);

        for (var index = 0; index < points.Length; index++)
        {
            var point = points[index];
            var nextPoint = points[(index + 1) % points.Length];

            if (point.Y == nextPoint.Y)
            {
                // Horizontal segment
                var segMinX = Math.Min(point.X, nextPoint.X);
                var segMaxX = Math.Max(point.X, nextPoint.X);

                // rectMinY < Y < rectMaxY - The horizontal line is lines within the Y-range of the rectangle.
                // Seems legit.
                bool horizontalIntersects = rectMinY < point.Y && point.Y < rectMaxY;

                // rectMinX < segMaxX - The low-point of the segment sits before the high-point of the rectangle X-range
                // segMinX < rectMaxX - The high-point of the segment sits after the low-point of the rectangle X-range
                // So the low-point could either be inside the square or below it.
                // So the high-point could either be inside the square or below it.
                bool verticallyInRange = segMinX < rectMaxX && segMaxX > rectMinX;

                if (horizontalIntersects && verticallyInRange) return false;
            }
            else
            {
                // Vertical segment
                var segMinY = Math.Min(point.Y, nextPoint.Y);
                var segMaxY = Math.Max(point.Y, nextPoint.Y);

                // rectMinX < X < rectMaxX - The vertical line is lines within the X-range of the rectangle.
                // Seems legit.
                bool verticallyIntersects = rectMinX < point.X && point.X < rectMaxX;

                // rectMinY < segMaxY - The low-point of the segment sits before the high-point of the rectangle Y-range
                // segMinY < rectMaxY - The high-point of the segment sits after the low-point of the rectangle Y-range
                // So the low-point could either be inside the square or to the left it.
                // So the high-point could either be inside the square or to the right it.
                bool horizontallyInRange = segMinY < rectMaxY && segMaxY > rectMinY;

                if (verticallyIntersects && horizontallyInRange) return false;
            }
        }

        return true;
    }

    private static Point[] ParseInput(string[] input) =>
        input
            .Select(pointsString =>
            {
                var pointsArr = pointsString.Split(",").Select(long.Parse).ToArray();
                return new Point(pointsArr[0], pointsArr[1]);
            }).ToArray();
}