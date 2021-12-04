def day3_1():
    input_values = [_ for _ in open("in").readlines()]

    bit_length = len(input_values[0])

    gamma_rate = ''
    epsilon_rate = ''

    for bit_pos in range(bit_length - 1):
        zeros = 0
        ones = 0
        for row in input_values:
            if row[bit_pos] == "1":
                ones += 1
            else:
                zeros += 1

        if ones == zeros:
            print(f"ohh fuck at row {row}")
            quit()

        if ones > zeros:
            gamma_rate += '1'
            epsilon_rate += '0'
        else:
            gamma_rate += '0'
            epsilon_rate += '1'

    print(f"gamma: {gamma_rate} eps: {epsilon_rate}")
    print(f"dec: gamma: {int(gamma_rate, 2)} eps: {int(epsilon_rate, 2)}")
    print(f"gamma * epsilon = {int(gamma_rate, 2) * int(epsilon_rate, 2)}")


day3_1()
