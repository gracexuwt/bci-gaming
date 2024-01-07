import time
import brainflow
import numpy as np
from midiutil import MIDIFile

import pandas as pd
import matplotlib.pyplot as plt

from brainflow.board_shim import BoardShim, BrainFlowInputParams, LogLevels, BoardIds
from brainflow.data_filter import DataFilter, FilterTypes, AggOperations, WindowOperations, DetrendOperations, NoiseTypes

def main():
    params = BrainFlowInputParams()
    params.board_id = BoardIds.CROWN_BOARD.value 
    # params.serial_port = 'COM5'
    params.serial_number = "9bb2e2fad5668286a4b4f407002b4359"
    board_id = params.board_id
    sampling_rate = BoardShim.get_sampling_rate(board_id)

    board = BoardShim(board_id, params)
    board.prepare_session()
    board.start_stream()
    BoardShim.log_message(LogLevels.LEVEL_INFO.value, 'start sleeping in the main thread')

    time.sleep(10)
    data = board.get_board_data()
    board.stop_stream()
    board.release_session()

    eeg_channels = BoardShim.get_eeg_channels(board_id)
    timestamp_channel = BoardShim.get_timestamp_channel(board_id)
    
    # PSD calculation
    nfft = DataFilter.get_nearest_power_of_two(sampling_rate)
    eeg_channel = eeg_channels[0]
    psd = DataFilter.get_psd_welch(data[eeg_channel], nfft, nfft // 2, sampling_rate, WindowOperations.NO_WINDOW.value)
    
    # Plotting before processing
    df = pd.DataFrame(np.transpose(data))
    plt.figure()
    df[eeg_channels[:3]].plot(subplots=True)
    plt.savefig("before_processing.png")

    # Data filtering
    for channel in eeg_channels:
        DataFilter.perform_bandpass(data[channel], sampling_rate, 1.0, 30.0, 4, FilterTypes.BUTTERWORTH.value, 0)
        DataFilter.remove_environmental_noise(data[channel], sampling_rate, NoiseTypes.FIFTY.value)
    
    # Plotting after processing
    df = pd.DataFrame(np.transpose(data))
    plt.figure()
    df[eeg_channels[:3]].plot(subplots=True)
    plt.savefig("after_processing.png")

    # Cycling through data and converting to musical notes
    cycled_data = data[eeg_channel][::50]  # Cycle through every 50th data point
    print(cycled_data)
    c_major_notes = [60, 62, 64, 65, 67, 69, 71]
    musical_notes = [c_major_notes[3 + round(max(-3000, min(3000, datum)) / 1000)] for datum in cycled_data] 
    print(musical_notes)

    # MIDI file creation
    track = 0
    channel = 0
    time_beat = 0
    duration = 1
    tempo = 120
    volume = 100

    MyMIDI = MIDIFile(1)
    MyMIDI.addTempo(track, time_beat, tempo)

    for pitch in musical_notes:
        MyMIDI.addNote(track, channel, pitch, time_beat, duration, volume)
        time_beat += 1

    with open("le-music.mid", "wb") as output_file:
        MyMIDI.writeFile(output_file)

    print("Conversion completed, play file le-music.mid")


if __name__ == "__main__":
    main()
