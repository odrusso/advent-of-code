#try 1: 3052
from functools import reduce

groups = [ _.split("\n") for _ in open("in").read().split("\n\n") ]

sums = []

for group in groups:
    groupSet = [set(_) for _ in group]
    sums.append( len(groupSet[0].intersection(*groupSet[1:])) )

print(sum(sums))
