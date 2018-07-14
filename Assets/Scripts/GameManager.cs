using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Desktop = 0,
    Tutorial,
    Playing,
    EndScreen
}

public class GameManager : MonoBehaviour {

    //===== ===== Variables ===== =====
    private static GameManager s_instance;
    public static GameManager Instance
    {
        get
        {
            if (!s_instance)
            {
                s_instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return s_instance;
        }
    }

    private NewsGeneration s_newsSource;
    public static NewsGeneration NewsSource
    {
        get
        {
            return Instance.s_newsSource;
        }
    }



    GameState state = GameState.Desktop;
    public bool TutorialFinished = false; 

    //===== ===== Start/Update ===== =====
    void Start () {
        s_newsSource = this.GetComponent<NewsGeneration>();
	}
	
	void Update () {
        StateTransition();
        //StateOnStay(); //for stuff that has to be done every frame in an specifik state
	}

    //===== ===== Registration ===== =====
    public Timer timer = null;
    public void RegistUI(Timer handle) {
        if (timer == handle)
            timer = null;
        else
            timer = handle;
    }

    //===== ===== State Mashine ===== =====
    void StateTransition() {

        //----- Any State -----
        //Quit button

        switch (state) {
        case GameState.Desktop:
            //TODO change to Tutorial by klicking an icon
            break;
        case GameState.Tutorial:
            //TODO change to Desktiop by abort
            if (TutorialFinished)
                ChangeState(GameState.Playing);
            break;
        case GameState.Playing:
            //TODO change to Desktop by pressing esc
            if (Input.GetButtonUp(StringCollection.CANCEL))
                ChangeState(GameState.Desktop);
            break;
        case GameState.EndScreen:
            break;
        default:
            break;
        }
    }

    void StateOnStay() {
        switch (state) {
        case GameState.Desktop:
            break;
        case GameState.Tutorial:
            break;
        case GameState.Playing:
            break;
        case GameState.EndScreen:
            break;
        default:
            break;
        }
    }

    void ChangeState(GameState newState) {
        if (state == newState)
            return;

        switch (state) { //by exiting this state
        case GameState.Desktop:
            break;
        case GameState.Tutorial:
            break;
        case GameState.Playing:
            if (timer)
                timer.state = TimerState.TimeShort;
            else
                Debug.LogError("timer not found. GameManager");
            break;
        case GameState.EndScreen:
            break;
        default:
            break;
        }

        state = newState;

        switch (state) { //by entering this state
        case GameState.Desktop:
            break;
        case GameState.Tutorial:
            break;
        case GameState.Playing:
            if (timer)
                timer.state = TimerState.Countdown;
            else
                Debug.LogError("timer not found. GameManager");
            break;
        case GameState.EndScreen:
            break;
        default:
            break;
        }
    }

    public bool RequestStateChange(GameState newState) {
        if ((state == GameState.Playing && newState == GameState.Tutorial) ||
            (state == GameState.Tutorial && newState == GameState.EndScreen) ||
            (state == GameState.Desktop && newState == GameState.EndScreen))
            return false;

        ChangeState(newState);
        return true;
    }
}
