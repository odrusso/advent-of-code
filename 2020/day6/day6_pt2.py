#try 1: 3052

groups = [ _.split("\n") for _ in open("in").read().split("\n\n") ]

sums = []

for group in groups:
    groupSet = [set(_) for _ in group]
    sums.append( len(set.intersection(*groupSet)) )

print(sum(sums))
