from collections import namedtuple


class BingoValue:
    def __init__(self, value):
        self.value = value
        self.marked = False

    def mark(self):
        self.marked = True

    def __repr__(self):
        return f"<{self.value}: {'T' if self.marked else 'F'}>"


def chunks(lst, n):
    """Yield successive n-sized chunks from lst."""
    for i in range(0, len(lst), n):
        yield lst[i:i + n]


def day4_1():
    # start with some data munging
    numbers, boards = munge_data()

    for number in numbers:
        update_boards(boards, number)

        winner_index = find_winner(boards)

        if winner_index is not None:
            print(f"winning board index: {winner_index}")
            [print(_) for _ in boards[winner_index]]
            winning_score = get_score_of_board(boards[winner_index], number)
            print(f"winning score is {winning_score}")
            return


def munge_data():
    """returns the list of input numbers, and a list of boards"""
    input_lines = [_.rstrip() for _ in open("in").readlines()]
    bingo_numbers = [int(number) for number in input_lines.pop(0).split(",")]
    bingo_board_blocks = list(chunks([_.split() for idx, _ in enumerate(input_lines) if idx % 6 != 0], 5))

    bingo_boards = []
    for numeric_board in bingo_board_blocks:
        this_board = []
        for row in numeric_board:
            this_board.append([BingoValue(value) for value in row])
        bingo_boards.append(this_board)

    return bingo_numbers, bingo_boards


def update_boards(bingo_boards, number):
    for board in bingo_boards:
        update_board(board, number)


def update_board(bingo_board, number):
    for row in bingo_board:
        for value in row:
            if value.value == str(number):
                value.marked = True


def find_winner(bingo_boards):
    # return index of first winner, or None if no winners
    for index, board in enumerate(bingo_boards):
        if is_board_winner(board):
            return index
    return None


def is_board_winner(bingo_board):
    for row in bingo_board:
        if is_row_winner(row):
            return True

    # Transpose the rows into cols and reuse the row logic
    transposed_board = list(map(list, zip(*bingo_board)))
    for row in transposed_board:
        if is_row_winner(row):
            return True

    return False


def is_row_winner(row):
    return len([_ for _ in row if _.marked]) == 5


def get_score_of_board(bingo_board, current_number):
    flat_board = [item for sublist in bingo_board for item in sublist]
    unmarked_values = [int(item.value) for item in flat_board if not item.marked]
    return sum(unmarked_values) * current_number


if __name__ == "__main__":
    day4_1()
