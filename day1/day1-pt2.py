# nothing here

listIn = [int(_) for _ in open("in").readlines()]

for num1, num2, num3 in [(x, y, z) for x in listIn for y in listIn for z in listIn]:
    # print(f"testing {num1} and {num2}")
    if num1 + num2 + num3 == 2020:
        print(f"{num1} {num2} {num3} {num1 * num2 * num3}")