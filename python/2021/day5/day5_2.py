class Coordinate:
    def __init__(self, x, y):
        self.x = int(x)
        self.y = int(y)

    def __repr__(self):
        return f"<x: {self.x}, y: {self.y}>"


class Line:
    def __init__(self, start, end):
        self.start: Coordinate = start
        self.end: Coordinate = end

    def __repr__(self):
        return f"<x0: {self.start.x}, y0: {self.start.y}, x1: {self.end.x}, y1: {self.end.y}>"


def day5_2():
    lines, coordinates = munge_data()
    max_x, max_y = get_max_x_and_y(coordinates)
    grid = build_grid(max_x, max_y)
    populate_grid(grid, lines)
    overlaps = get_number_of_overlaps(grid)
    print(f"overlaps: {overlaps}")


def munge_data():
    input_lines = [_.rstrip() for _ in open("in").readlines()]
    lines = []
    coordinates = []
    for line in input_lines:
        start_raw, end_raw = line.split(" -> ")

        start_coord = Coordinate(*start_raw.split(","))
        end_coord = Coordinate(*end_raw.split(","))

        coordinates.append(start_coord)
        coordinates.append(end_coord)
        lines.append(Line(start_coord, end_coord))

    return lines, coordinates


def get_max_x_and_y(coordinates):
    max_x = max([_.x for _ in coordinates])
    max_y = max([_.y for _ in coordinates])
    return max_x, max_y


def build_grid(x, y):
    return [[0 for _ in range(x + 1)] for a in range(y + 1)]


def populate_grid(grid, lines):
    for line in lines:
        points = get_all_points_for_line(line)
        for point in points:
            grid[point.x][point.y] += 1


def get_all_points_for_line(line: Line):
    start, end = sorted([line.start, line.end], key=lambda a: a.x)
    if start.x == end.x:
        # vert line
        y0, y1 = sorted([start.y, end.y])
        return [Coordinate(start.x, _) for _ in range(y0, y1 + 1)]

    elif start.y == end.y:
        # horiz line
        return [Coordinate(_, start.y) for _ in range(start.x, end.x + 1)]

    elif start.y > end.y:
        # slope-down
        points = [Coordinate(start.x + step, start.y - step) for step in range(end.x - start.x + 1)]
        return points

    elif start.y < end.y:
        # slope-up
        points = [Coordinate(start.x + step, start.y + step) for step in range(end.x - start.x + 1)]
        return points

    else:
        print("Not an easy line, ignoring it!")
        return []


def get_number_of_overlaps(grid):
    return len([_ for _ in flatten(grid) if _ > 1])


def flatten(t):
    return [item for sublist in t for item in sublist]


if __name__ == "__main__":
    day5_2()

# guess 1: 20177; too high
# guess 2: 19576; too high
# guess 3: 19164;
