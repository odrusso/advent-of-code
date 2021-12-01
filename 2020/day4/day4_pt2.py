# try 1: 224

import re

class Passport:
    def __init__(self, listIn):
        self.listIn = listIn
        self.attr = dict( [tuple(_.split(":")) for _ in listIn] )

    def isValid(self):
        # verify all attributes
        reqKeys = ("byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid")
        for reqKey in reqKeys:
            if reqKey not in self.attr:
                return False

            val = self.attr[reqKey]

            if reqKey == "byr":
                valid = int(val) >= 1920 and int(val) <= 2002
                if not valid:
                    return False

            if reqKey == "iyr":
                valid = int(val) >= 2010 and int(val) <= 2020
                if not valid:
                    return False

            if reqKey == "eyr":
                valid = int(val) >= 2020 and int(val) <= 2030
                if not valid:
                    return False

            if reqKey == "hgt":
                if val[-2:] == "cm":
                    valid = int(val[:-2]) >= 150 and int(val[:-2]) <= 193
                    if not valid:
                        return False
                elif val[-2:] == "in":
                    valid = int(val[:-2]) >= 59 and int(val[:-2]) <= 76
                    if not valid:
                        return False
                else:
                    return False

            if reqKey == "hcl":
                match = re.fullmatch("#[0-9a-f]{6}", val)
                if match is None:
                    return False

            if reqKey == "ecl":
                valid = val in ("amb", "blu", "brn", "gry", "grn", "hzl", "oth")
                if not valid:
                    return False

            if reqKey == "pid":
                match = re.fullmatch("[0-9]{9}", val)
                if match is None:
                    return False

        return True


passports = [_.replace("\n", " ").split(" ") for _ in open("in").read().split("\n\n")]

valid = 0
for passport in passports:
    if Passport(passport).isValid():
        valid += 1

print(valid)
