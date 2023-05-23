using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using brainflow;
using brainflow.math;

public class BoardData : MonoBehaviour
{
    private BoardShim board_shim = null;
    private int sampling_rate = 0;
    private double[,] data;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            BoardShim.set_log_file("brainflow_log.txt");
            BoardShim.enable_dev_board_logger();

            BrainFlowInputParams input_params = new BrainFlowInputParams();
            //input_params.ip_port = 6677;
            //input_params.ip_address = "225.1.1.1";
            int board_id = (int)BoardIds.SYNTHETIC_BOARD;
            board_shim = new BoardShim(board_id, input_params);
            board_shim.prepare_session();
            board_shim.start_stream(450000, "file://brainflow_data.csv:w");
            sampling_rate = BoardShim.get_sampling_rate(board_id);
            Debug.Log("Brainflow streaming was started");
            Debug.Log("HELLO WORLD");
        }
        catch (BrainFlowError e)
        {
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (board_shim == null)
        {
            return;
        }
        int number_of_data_points = sampling_rate * 4;
        //Debug.Log("Data poitns: " + number_of_data_points);
        double[,] data = board_shim.get_current_board_data(number_of_data_points);
        int[] eeg_channels = BoardShim.get_eeg_channels((int)BoardIds.SYNTHETIC_BOARD);
        // check https://brainflow.readthedocs.io/en/stable/index.html for api ref and more code samples
        //Debug.Log("Num elements: " + data.GetLength(1));
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                Debug.Log(data[i, j]);
            }
        }
    }

    // you need to call release_session and ensure that all resources correctly released
    private void OnDestroy()
    {
        if (board_shim != null)
        {
            try
            {
                board_shim.release_session();
            }
            catch (BrainFlowError e)
            {
                Debug.Log(e);
            }
            Debug.Log("Brainflow streaming was stopped");
        }
    }
}