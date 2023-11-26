using System;
using brainflow;
using brainflow.math;

class BCIApplication
{
    static void Main(string[] args)
    {
        BoardShim.enable_dev_board_logger();

        BrainFlowInputParams inputParams = new BrainFlowInputParams();
        int boardId = (int)BoardIds.CROWN_BOARD; // Or other BoardIds as needed
        inputParams.serial_number = "9bb2e2fad5668286a4b4f407002b4359";
        int samplingRate = BoardShim.get_sampling_rate(boardId);
        int timeThres = 100;
        double maxVal = -100000000000;
        double valsMean = 0;
        int numSamples = 5000;
        int samples = 0;

        BoardShim board = new BoardShim(boardId, inputParams);
        board.prepare_session();
        board.start_stream(450000);
        Console.WriteLine("Starting calibration");
        System.Threading.Thread.Sleep(5000);
        double[,] data = board.get_board_data();
        Console.WriteLine("Start blinking");

        // Calibration
        while (samples < numSamples)
        {
            data = board.get_board_data();
            int channelLength = data.GetLength(1); // Assuming channel 1 is the EEG data
            if (channelLength > 0)
            {
                double[] channelData = data.GetRow(1);
                DataFilter.perform_rolling_filter(channelData, 2, (int)AggOperations.MEAN);
                for (int i = 0; i < channelLength; i++)
                {
                    valsMean += channelData[i] / numSamples;
                }
                samples += channelLength;
                maxVal = Math.Max(maxVal, channelData.Max());
            }
        }

        double blinkThres = 0.5 * Math.Pow((maxVal - valsMean), 2);
        Console.WriteLine($"Mean value: {valsMean}");
        Console.WriteLine($"Max value: {maxVal}");
        Console.WriteLine($"Threshold: {blinkThres}");
        Console.WriteLine("CALIBRATION DONE START PLAYING");

        long prevTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        // Main loop
        while (true)
        {
            data = board.get_board_data();
            int channelLength = data.GetLength(1);
            if (channelLength > 0)
            {
                double[] channelData = data.GetRow(1);
                DataFilter.perform_rolling_filter(channelData, 2, (int)AggOperations.MEAN);
                if ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - timeThres) > prevTime)
                {
                    prevTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    foreach (double element in channelData)
                    {
                        if (Math.Pow((element - valsMean), 2) >= blinkThres)
                        {
                            Console.WriteLine("WOOO BLINK DETECTED");
                            break;
                        }
                    }
                }
            }
        }

        // Cleanup
        board.stop_stream();
        board.release_session();
    }
}
