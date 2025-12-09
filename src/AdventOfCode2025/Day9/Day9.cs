using AdventUtils;

namespace AdventOfCode2025.Day9;

public class Day9 : AbstractDay
{
    private record Point(long X, long Y)
    {
        public long SquareSize(Point other)
        {
            return Math.Abs(X - other.X + 1) * Math.Abs(Y - other.Y + 1);
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
        // These points describe the perminiter of a shape. This shape is filled. 
        // When we are looking for the largest rectangle (same alg as above), we also need
        // to validate that it is entirely contained within this shape. 

        // How?

        // To build the whole thing into a map would take a 1.25gb matrix (maybe with more overhead)
        // Then for each square, iterate through every point within it (the biggest is 4741848414 lol)
        // and make sure every point is mapped within that bitmap. Can maybe do it 'easier' using i64 bitmasks
        // instead of arrays or whatever... but still. Not fast.

        // I'll have a look at good data structures for describing strange 2D shapes...

        // The above might make sense, because we only have to check along the edges of the rectangle.
        // We follow along the edges of our testtangle, and see if the points intersect with a line segment...?
        // What does that mean, it will 100% intersect with the lines created by the points that define them ...
        // Grr no maybe this doesn't work :/

        // Maybe the bitmask idea is the best idea?
        // We can just try to build it for now...
        // But then we'll have to do a flood fill lmfao gross

        // Okay here's the plan.
        // 'shrink' the rectangle by 0.5 unit(s) on each each edge. Then for each point (at half units)
        // We run a ray check. This will check against a ton of line segments. If the ray count is even
        // Then we've got a bit of edge outside of the shape. Therefore, it's invalid.
        // If we get to the end of the edges, and all rays are odd, then it's entirely enclosed.
        // Bosh.

        var points = ParseInput(input);

        long maxSquareSize = -1;

        for (var leftIndex = 0; leftIndex < points.Length - 1; leftIndex++)
        {
            var leftPoint = points[leftIndex];

            for (var rightIndex = leftIndex + 1; rightIndex < points.Length; rightIndex++)
            {
                var rightPoint = points[rightIndex];

                var size = leftPoint.SquareSize(rightPoint);

                // if (size < 1000000000L) continue;

                //          230259758L
                // if (size < 1500000000L) continue;
                // if (size > 1000L) continue;

                // maxSquareSize++;

                if (size <= maxSquareSize) continue;

                if (!Enclosed(points, leftPoint, rightPoint)) continue;

                maxSquareSize = size;
            }
        }

        return maxSquareSize;
    }

    private static bool Enclosed(Point[] points, Point leftPoint, Point rightPoint)
    {
        // Top-line check. Horiz ray from top-left
        if (NewEdgeDetect(points, Math.Min(leftPoint.X, rightPoint.X), Math.Min(leftPoint.Y, rightPoint.Y), horizontalRay: true)) return false;

        // Bottom-line check. Horiz ray from bottom-left
        if (NewEdgeDetect(points, Math.Min(leftPoint.X, rightPoint.X), Math.Max(leftPoint.Y, rightPoint.Y) - 1, horizontalRay: true)) return false;

        // Left-line check. Vert ray from top-left
        if (NewEdgeDetect(points, Math.Min(leftPoint.X, rightPoint.X), Math.Min(leftPoint.Y, rightPoint.Y), horizontalRay: false)) return false;

        // Right-line check. Vert ray from top-right
        if (NewEdgeDetect(points, Math.Max(leftPoint.X, rightPoint.X) - 1, Math.Min(leftPoint.Y, rightPoint.Y), horizontalRay: false)) return false;

        // x1y2 -> x1y1 or left-line check
        // for (long y = Math.Min(leftPoint.Y, rightPoint.Y); y < Math.Max(leftPoint.Y, rightPoint.Y - 1); y++)
        // {
        //     if (EdgeDetect(points, Math.Min(leftPoint.X, rightPoint.X), y)) return false;
        //     if (EdgeDetect(points, Math.Max(leftPoint.X, rightPoint.X) - 1, y)) return false;
        // }

        // for (long x = Math.Min(leftPoint.X, rightPoint.X); x < Math.Max(leftPoint.X, rightPoint.X - 1); x++)
        // {
        //     if (EdgeDetect(points, x, Math.Min(leftPoint.Y, rightPoint.Y))) return false;
        //     if (EdgeDetect(points, x, Math.Max(leftPoint.Y, rightPoint.Y) - 1)) return false;
        // }

        // x2y1 -> x2y2 or right-line check and
        // x1y2 -> x1y1 or left-line check
        // for (long y = Math.Min(leftPoint.Y, rightPoint.Y); y < Math.Max(leftPoint.Y, rightPoint.Y - 1); y++)
        // {
        //     if (EdgeDetect(points, Math.Min(leftPoint.X, rightPoint.X), y)) return false;
        //     if (EdgeDetect(points, Math.Max(leftPoint.X, rightPoint.X) - 1, y)) return false;
        // }

        return true;
    }

    private static bool NewEdgeDetect(Point[] points, long x, long y, bool horizontalRay)
    {
        var intersections = 0;

        for (int i = 0; i < points.Length; i++)
        {
            // These two points define a line segment. 
            var thisPoint = points[i];
            var nextPoint = points[(i + 1) % points.Length]; // For wrapping

            // Eliminite co-linear comparisons
            if (horizontalRay && thisPoint.Y == nextPoint.Y) continue;
            if (!horizontalRay && thisPoint.X == nextPoint.X) continue;

            var testX = x + 0.5;
            var testY = y + 0.5;

            if (horizontalRay)
            {
                var segmentLowY = Math.Min(thisPoint.Y, nextPoint.Y);
                var segmentHighY = Math.Max(thisPoint.Y, nextPoint.Y);

                // hx1 <= vx <= hx2 (intersection point's x is within horizontal segment)
                // vy1 <= hy <= vy2 (intersection point's y is within vertical segment)
                if ((testX <= thisPoint.X) && (segmentLowY <= testY) && (testY <= segmentHighY)) intersections++;
            }
            else // Vertical ray
            {
                var segmentLowX = Math.Min(thisPoint.X, nextPoint.X);
                var segmentHighX = Math.Max(thisPoint.X, nextPoint.X);

                // hx1 <= vx <= hx2 (intersection point's x is within horizontal segment)
                // vy1 <= hy <= vy2 (intersection point's y is within vertical segment)
                if ((segmentLowX <= testX) && (testX <= segmentHighX) && (testY <= thisPoint.Y)) intersections++;
            }
        }

        return intersections % 2 != 0;
    }

    private static bool EdgeDetect(Point[] points, long x, long y)
    {
        var testX = x + 0.5;
        var testY = y + 0.5;

        var intersections = 0;

        for (int i = 0; i < points.Length; i++)
        {
            // These two points define a line segment. 
            var thisPoint = points[i];
            var nextPoint = points[(i + 1) % points.Length]; // For wrapping

            // We can safely ignore horizontal line segments, yay! Or christ I hope we can.
            // Probably need to TODO this harder...
            if (thisPoint.Y == nextPoint.Y) continue;

            var pointLowY = Math.Min(thisPoint.Y, nextPoint.Y);
            var pointHighY = Math.Max(thisPoint.Y, nextPoint.Y);

            // hx1 <= vx <= hx2 (intersection point's x is within horizontal segment)
            // vy1 <= hy <= vy2 (intersection point's y is within vertical segment)
            // man I hope this is right...
            if ((testX <= thisPoint.X) && (pointLowY <= testY) && (testY <= pointHighY)) intersections++;
        }

        return intersections % 2 != 0;
    }

    private static Point[] ParseInput(string[] input) =>
        input
            .Select(pointsString =>
            {
                var pointsArr = pointsString.Split(",").Select(long.Parse).ToArray();
                return new Point(pointsArr[0], pointsArr[1]);
            }).ToArray();
}