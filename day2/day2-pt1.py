import re

linesIn = open("in").readlines()
# linesIn = ["8-13 r: xhscjbqthpfkffjh\n"]
regex = "([0-9]+)-([0-9]+) ([a-zA-Z]): ([a-zA-Z0-9]*)\n"

totalValid = 0

for line in linesIn:
    print(f"Test line: {line}")
    min, max, char, wrd = re.fullmatch(regex, line).groups()
    srtWrd = "".join(sorted(wrd))
    group = re.match(f".*?([{char}]+).*?", srtWrd)

    if group is None:
        continue

    letters = len(group.group(1))

    if (int(min) <= letters) and (letters <= int(max)):
        print(f"Valid Password: {line}")
        totalValid += 1
    else:
        print(f"Invalid Password: {line}")

print(totalValid)
