﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomExtensions;

public enum TimerState{
    Countdown = 0,
    TimeShort
}

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour {

    private Text text;
    private TimerState state = TimerState.TimeShort;
    

    void Start() {
        text = GetComponent<Text>();
        if (!text) {
            print("text not found. Timer");
        }
        GameManager.Instance.RegistTimer(this);
    }
    private void OnDestroy() {
        try {
            GameManager.Instance.RegistTimer(this);//meldet nur sich selbst ab; 
        } catch (System.Exception e) {
            print(e.Message);
        }
    }

    void Update() {
        switch (state) {
        case TimerState.Countdown:
            text.text = GameManager.Instance.timeLeft.ToTimeString();// ((int)(GameManager.Instance.timeLeft / 60)).ToString("D2") + ":" + ((int)(GameManager.Instance.timeLeft % 60)).ToString("D2");
            break;
        case TimerState.TimeShort:
            text.text = System.DateTime.Now.ToString("HH:mm");
            break;
        default:
            text.text = "kurtz nach drölf";
            break;
        }
    }

    public void ChangeStateToCountdown () {
        state = TimerState.Countdown;
    }

    public void ChangeStateToTimeShort() {
        state = TimerState.TimeShort;
    }
}
