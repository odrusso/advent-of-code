# try 1: 135
# try 2: 264

print(len(list(filter(lambda x: x, [all(attribute in _.replace("\n", " ") for attribute in ("byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid")) for _ in open("in").read().split("\n\n")]))))
