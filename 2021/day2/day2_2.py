def day2_2():
    input_values = [_.rstrip().split(" ") for _ in open("in").readlines()]

    depth = 0
    hoz = 0
    aim = 0

    for action, amount_str in input_values:
        amount = int(amount_str)

        match action:
            case "forward":
                hoz += amount
                depth += amount * aim
            case "up":
                aim -= amount
            case "down":
                aim += amount

    print(f"hoz: {hoz} depth: {depth} depth*hoz {hoz*depth}")


day2_2()
