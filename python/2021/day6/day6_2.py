def day6_1():
    fish_list = [int(_) for _ in open("in").readlines()[0].rstrip().split(",")]
    fish = gen_empty_fish()

    for dude in fish_list:
        fish[dude] += 1

    for _ in range(256):
        todays_fish = gen_empty_fish()
        for age, count in fish.items():
            if age == 0:
                todays_fish[8] += count  # new lads
                todays_fish[6] += count  # old lads
            else:
                todays_fish[age - 1] += count  # getting ready lads

        fish = todays_fish

    print(f"fish: {fish}")
    print(f"fish count: {sum(fish.values())}")


def gen_empty_fish():
    return {0: 0, 1: 0, 2: 0, 3: 0, 4: 0, 5: 0, 6: 0, 7: 0, 8: 0}


if __name__ == "__main__":
    day6_1()
