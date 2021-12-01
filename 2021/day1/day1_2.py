def day1_2():
    depths = [int(_) for _ in open("in").readlines()]

    num_of_increasing_depths = 0
    for depth_index in range(len(depths) - 3):
        base_slide = depths[depth_index:depth_index+3]
        next_slide = depths[depth_index+1:depth_index+4]
        if sum(base_slide) < sum(next_slide):
            num_of_increasing_depths += 1

    print(num_of_increasing_depths)


if __name__ == "__main__":
    day1_2()