import brainflow
import time

from brainflow import BoardIds
from brainflow.board_shim import BoardShim, BrainFlowInputParams
from brainflow.data_filter import DataFilter, FilterTypes, AggOperations

def main ():
    board_id = BoardIds.CROWN_BOARD.value # or BoardIds.NOTION_2_BOARD.value or BoardIds.NOTION_1_BOARD.value
    params = BrainFlowInputParams ()
    params.board_id = board_id
    params.serial_number = "9bb2e2fad5668286a4b4f407002b4359"
    BoardShim.enable_dev_board_logger ()
    board = BoardShim (board_id, params)
    board.prepare_session ()
    board.start_stream ()
    data = board.get_board_data ()
    print("First\n",data)
    data = board.get_board_data ()
    time.sleep(5)
    board.stop_stream ()
    board.release_session ()

    print ("2nd\n",data)

if __name__ == "__main__":
    main ()