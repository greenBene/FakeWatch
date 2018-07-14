using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TimerState{
    Countdown = 0,
    TimeShort
}

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour {

    private Text text;
    private NewsGeneration news;
    public TimerState state = TimerState.TimeShort;

    // Use this for initialization
    void Start() {
        news = FindObjectOfType<NewsGeneration>();
        text = GetComponent<Text>();
        if (!text) {
            print("text not found. Timer");
        }
        state = TimerState.Countdown;
    }
    // Update is called once per frame
    void Update() {
        switch (state) {
        case TimerState.Countdown:
            text.text = ((int)(news.timeLeft / 60)).ToString("D2") + ":" + ((int)(news.timeLeft % 60)).ToString("D2");
            break;
        case TimerState.TimeShort:
            text.text = System.DateTime.Now.ToString("HH:mm");
            break;
        default:
            text.text = "kurtz nach drölf";
            break;
        }
        
    }
}
