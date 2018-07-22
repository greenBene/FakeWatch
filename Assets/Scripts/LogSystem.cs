using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LogSystem {

    static string fileName = "LOG\\" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "_EXE.txt";

    public static void LogOnFile(string message) {
        if (!GameManager.Instance.doLog)
            return;

        Directory.CreateDirectory("LOG");
        File.AppendAllText(fileName, System.DateTime.Now.ToString("HH:mm:ss") + " | GT: " + ((int)GameManager.Instance.timeLeft).ToString("D4") + " |=| Message: " + message + System.Environment.NewLine);
    }

    public static void LogOnNewFile(string message) {
        if (!GameManager.Instance.doLog)
            return;

        fileName = "LOG\\" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
        LogOnFile(message);
    }
}
