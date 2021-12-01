depths = [int(_) for _ in open("in").readlines()]

num_of_increasing_depths = 0
for depth_index in range(len(depths) - 1):
    if depths[depth_index] < depths[depth_index + 1]:
        num_of_increasing_depths += 1

print(num_of_increasing_depths)
