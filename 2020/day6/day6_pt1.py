#try 1: 6291

groups = [ set(_.replace("\n", "")) for _ in open("in").read().split("\n\n") ]

sums = []
for group in groups:
    sums.append(len(group))

print(sum(sums))
