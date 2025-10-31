def day6_1():
    fish = [int(_) for _ in open("in").readlines()[0].rstrip().split(",")]
    for _ in range(80):
        todays_fish = []
        for dude in fish:

            if dude == 0:
                new_dude = 6
                todays_fish.append(8)
            else:
                new_dude = dude - 1

            todays_fish.append(new_dude)

        fish = todays_fish

    print(f"fish: {len(fish)}")


if __name__ == "__main__":
    day6_1()

# guess 1: 393019, correct!
