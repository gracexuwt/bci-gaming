using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Notion.Unity.Example
{
    public class Calibration : MonoBehaviour
    {
        BlinkDetector blinkDetector = new BlinkDetector(); 
        public GameObject CalibrationModal;
        public GameObject CalibrationDoneModal;

        bool cont = true;

        void isCalibrationComplete() 
        {
            while(cont) 
            {
                if (BlinkDetector.calibrationComplete) 
                {
                    cont = false;
                    CalibrationModal.SetActive(false);
                    CalibrationDoneModal.SetActive(true);

                }
            }

        }
    }
}
