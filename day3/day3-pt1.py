# try 1 = 20
# try 2 = 48
# try 3 = 225 right!


linesIn = [_.rstrip() for _ in open("in").readlines()]

x = 0
y = 0
impacts = 0
lineLen = len(linesIn[0])

while y < len(linesIn):
    if linesIn[y][x % lineLen] == "#":
        impacts += 1

    lineList = list(linesIn[y])
    lineList[x % lineLen] = "X"
    linesIn[y] = "".join(lineList)

    if y > 320:
        print("="*200)
        print("\n".join(linesIn))
        print("="*200)

    x += 3
    y += 1

print(impacts)
