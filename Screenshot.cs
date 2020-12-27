using System;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public void ScreenShot()
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int day = DateTime.Now.Day;

        int hour = DateTime.Now.Hour;
        int min = DateTime.Now.Minute;
        int sec = DateTime.Now.Second;

        string timestamp = string.Format("{0:D4}_{1:D2}_{2:D2} {3:D2}_{4:D2}_{5:D2}",
            year, month, day, hour, min, sec);

        string filename = timestamp + ".png";

        ScreenCapture.CaptureScreenshot(filename);
        Debug.Log("Screenshot stored to " + filename);
    }
}
