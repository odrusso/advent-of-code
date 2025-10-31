# try 1 =

import math

linesIn = [_.rstrip() for _ in open("in").readlines()]

x = 0
y = 0
impacts = 0
impactsList = []

lineLen = len(linesIn[0])

for (dx, dy) in [(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)]:
    while y < len(linesIn):
        if linesIn[y][x % lineLen] == "#":
            impacts += 1

        x += dx
        y += dy

    impactsList.append(impacts)
    x, y = (0, 0)
    impacts = 0


print(impactsList)
print(math.prod(impactsList))
