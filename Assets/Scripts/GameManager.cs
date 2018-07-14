using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Desktop = 0,
    Tutorial,
    Playing,
    EndScreen
}

public enum EventTrigger {
    Tutorial = 0
}

public enum EventMessage {
    Failed = 0,
    Sucsess
}

[RequireComponent(typeof(NewsGeneration))]
public class GameManager : MonoBehaviour {

    //===== ===== Singelton ===== =====
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

    private NewsGeneration2 s_newsSource;
    public static NewsGeneration2 NewsSource
    {
        get
        {
            return Instance.s_newsSource;
        }
    }

    //===== ===== Variables ===== =====
    [SerializeField]
    GameObject endScreen = null;

    [SerializeField]
    float timeToPlayInSeconds = 600f;

    public float timeLeft { get; private set; }

    private int correctMarkedArticles = 0;
    private int wronglyMarkedArticlesAsTrue = 0;
    private int wronglyMarkedArticlesAsFalse = 0;

    GameState state = GameState.Desktop;

    //===== ===== MonoBehaviourStuff ===== =====
    private Canvas s_mainScreen;
    public static Canvas MainScreen
    {
        get
        {
            return Instance.s_mainScreen;
        }
    }

    // Use this for initialization
    void Start () {
        s_newsSource = GetComponent<NewsGeneration2>();
        s_mainScreen = FindObjectOfType<Canvas>();
	}
	
	void Update () {
        StateTransition();
        StateOnStay(); //for stuff that has to be done every frame in an specifik state
	}

    //===== ===== Registration ===== =====
    Timer timer = null;
    public void RegistTimer(Timer handle) {
        if (timer == handle)
            timer = null;
        else
            timer = handle;
    }

    TutorialHandler tutorial = null;
    public void RegistTutorial(TutorialHandler handle) {
        if (tutorial == handle)
            tutorial = null;
        else
            tutorial = handle;
    }

    //===== ===== Comunicator ===== =====
    public bool RequestStateChange(GameState newState) {
        if ((state == GameState.Playing && newState == GameState.Tutorial) ||
            (state == GameState.Tutorial && newState == GameState.EndScreen) ||
            (state == GameState.Desktop && newState == GameState.EndScreen))
            return false;

        ChangeState(newState);
        return true;
    }

    public void RequestStateChange(EventTrigger trigger, EventMessage message) {
        switch (trigger) {
        case EventTrigger.Tutorial:
            switch (message) {
            case EventMessage.Sucsess:
                ChangeState(GameState.Playing);
                break;
            case EventMessage.Failed:
                ChangeState(GameState.Desktop);
                break;
            default:
                break;
            }
            break;
        default:
            break;
        }
    }

    public void Score(bool news, bool input) {//real/fake news, correct/fake as input
        if (news == input)
            correctMarkedArticles++;
        else if (input)//zu diesem zeitpunkt muss der spieler sich falsch entschieden haben.
            wronglyMarkedArticlesAsTrue++;
        else
            wronglyMarkedArticlesAsFalse++;
    }

    //===== ===== State Mashine ===== =====
    void StateTransition() {

        //----- Any State -----
        if (Input.GetButtonUp(StringCollection.CANCEL))
            ChangeState(GameState.Desktop);

        //----- State Specific -----
        switch (state) {
        case GameState.Desktop:
            break;
        case GameState.Tutorial:
            break;
        case GameState.Playing:
            if (timeLeft <= 0)
                ChangeState(GameState.EndScreen);
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
            timeLeft -= Time.deltaTime;
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
            tutorial.AbortTutorial();
            break;
        case GameState.Playing:
            if (timer)
                timer.state = TimerState.TimeShort;
            else
                Debug.LogError("timer not found. GameManager");
            break;
        case GameState.EndScreen:
            endScreen.SetActive(false);
            correctMarkedArticles = 0;
            wronglyMarkedArticlesAsFalse = 0;
            wronglyMarkedArticlesAsTrue = 0;
            break;
        default:
            break;
        }

        state = newState;

        switch (state) { //by entering this state
        case GameState.Desktop:
            break;
        case GameState.Tutorial:
            //TODO: tutorial starten
            //TODO: tutorial aktiv setzten
            break;
        case GameState.Playing:
            timeLeft = timeToPlayInSeconds;
            if (timer)
                timer.state = TimerState.Countdown;
            else
                Debug.LogError("timer not found. GameManager");
            break;
        case GameState.EndScreen:
            endScreen.SetActive(true);
            break;
        default:
            break;
        }
    }

    
}
