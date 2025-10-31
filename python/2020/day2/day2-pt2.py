import re

linesIn = open("in").readlines()
# linesIn = ["8-13 r: xhscjbqthpfkffjh\n"]
regex = "([0-9]+)-([0-9]+) ([a-zA-Z]): ([a-zA-Z0-9]*)\n"

totalValid = 0

for line in linesIn:
    print(f"Test line: {line}")
    min, max, char, wrd = re.fullmatch(regex, line).groups()

    pos1 = char == wrd[int(min)-1]
    pos2 = char == wrd[int(max)-1]

    if pos1 ^ pos2:
        print(f"Valid Password: {line}")
        totalValid += 1
    else:
        print(f"Invalid Password: {line}")

print(totalValid)
