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

    public static void SaveHighScore(string alias) {
        if (!File.Exists("Highscore.csv"))
            File.AppendAllText("Highscore.csv", "SCORE" + ";" + "ID" + ";" + "NAME" + ";" + "CORECKT" + ";" + "FALSE +" + ";" + "FALSE -" + System.Environment.NewLine);

        float score = ((GameManager.Instance.correctMarkedArticles * 10000) /(GameManager.Instance.correctMarkedArticles + GameManager.Instance.wronglyMarkedArticlesAsTrue + GameManager.Instance.wronglyMarkedArticlesAsFalse));
        
        string[] lines = File.ReadAllLines("Highscore.csv");
        lines[0] = score + ";" + GameManager.Instance.PlayerID + ";" + alias + ";" + GameManager.Instance.correctMarkedArticles + ";" + GameManager.Instance.wronglyMarkedArticlesAsTrue + ";" + GameManager.Instance.wronglyMarkedArticlesAsFalse;
        System.Array.Sort(lines, (string y, string x) => int.Parse(x.Split(';')[0]) - int.Parse(y.Split(';')[0]));
        
        File.WriteAllText("Highscore.csv", "SCORE" + ";" + "ID" + ";" + "NAME" + ";" + "CORECKT" + ";" + "FALSE +" + ";" + "FALSE -" + System.Environment.NewLine);
        foreach(var line in lines) {
            File.AppendAllText("Highscore.csv", line + System.Environment.NewLine);
            Debug.Log(line);
        }
    }
}