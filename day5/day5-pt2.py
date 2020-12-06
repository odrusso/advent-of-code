# try 1: 527

boardingPasses = [_.rstrip() for _ in open("in").readlines()]

ids = []

for boardingPass in boardingPasses:
    rowSection = boardingPass[:7]
    columnSection = boardingPass[7:]

    rowNum = int(rowSection.replace("F", "0").replace("B", "1"), 2)
    colNum = int(columnSection.replace("L", "0").replace("R", "1"), 2)

    ids.append(rowNum * 8 + colNum)

for i in range(min(ids), max(ids)):
    if i not in ids:
        print(i)

print(max(ids))
