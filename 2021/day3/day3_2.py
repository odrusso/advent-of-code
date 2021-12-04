from collections import namedtuple

Row = namedtuple("Row", ["index", "value"])


def day3_2():
    input_values = [_.rstrip() for _ in open("in").readlines()]
    input_rows = []

    for index, row in enumerate(input_values):
        input_rows.append(Row(index, row))

    oxygen_rating_index = rating(input_rows, keep_common=True, fallback=1)
    co2_rating_index = rating(input_rows, keep_common=False, fallback=0)

    print(f"oxygen rating: {input_rows[oxygen_rating_index]}")
    print(f"co2 rating: {input_rows[co2_rating_index]}")

    print(f"oxygen decimal: {int(input_rows[oxygen_rating_index].value, 2)}")
    print(f"co2 decimal: {int(input_rows[co2_rating_index].value, 2)}")

    print(f"result: {int(input_rows[oxygen_rating_index].value, 2) * int(input_rows[co2_rating_index].value, 2)}")


def rating(rows, keep_common, fallback):
    # base case
    if len(rows) == 1:
        return rows[0].index

    common_value, fellback = most_common_value_at_position_0(rows, fallback)

    # We invert the filter value if we want to keep the less common values
    if not keep_common and not fellback: common_value = int(not common_value)

    filtered_rows = [row for row in rows if row.value[0] == str(common_value)]

    # drill down with the 1st value on all
    return rating([Row(row.index, row.value[1:]) for row in filtered_rows], keep_common, fallback)


def most_common_value_at_position_0(rows, fallback):
    zeros = 0
    ones = 0

    for row in rows:
        if row.value[0] == "1": ones += 1
        if row.value[0] == "0": zeros += 1

    if ones == zeros: return fallback, True
    return int(ones > zeros), False


if __name__ == "__main__":
    day3_2()

# first guess; 4448418; too high
# second guess; 4406844; correct!
