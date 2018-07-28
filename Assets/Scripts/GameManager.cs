using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Sucsess,
    Scip
}

[RequireComponent(typeof(NewsGeneration))]
[RequireComponent(typeof(MessengerHandler))]
[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour,IStateMachine<GameState> {

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

    private Canvas s_mainScreen;
    public static Canvas MainScreen {
        get {
            return Instance.s_mainScreen;
        }
    }

    private MessengerHandler s_messengerHandler;
    public static MessengerHandler MessengerHandler {
        get {
            return Instance.s_messengerHandler;
        }
    }

    //===== ===== Variables ===== =====
    [SerializeField] AudioClip DesktopStartAudio;
    [SerializeField] AudioClip LogOutAudio;
    [SerializeField]
    GameObject endScreen = null;

    [SerializeField]
    float timeToPlayInSeconds = 600f;

    public float timeLeft {get;  private set; }
    
    public int correctMarkedArticles { get; private set; }
    public int wronglyMarkedArticlesAsTrue { get; private set; }
    public int wronglyMarkedArticlesAsFalse { get; private set; }

    public int PlayerID = 0;
    AudioSource audio;

    [SerializeField] public bool doLog = false;

    [Header("Debug")]
    public int DebugCorrect1 = 0;
    public int DebugWrong1 = 0;
    public int DebugWrong2 = 0;

    public GameState state = GameState.Desktop;

    //===== ===== MonoBehaviourStuff ===== =====
    
    // Use this for initialization
    void Start () {
        correctMarkedArticles = 0;
        wronglyMarkedArticlesAsFalse = 0;
        wronglyMarkedArticlesAsTrue = 0;
        s_newsSource = GetComponent<NewsGeneration2>();
        s_mainScreen = FindObjectOfType<Canvas>();
        s_messengerHandler = GetComponent<MessengerHandler>();
        audio = GetComponent<AudioSource>();
        audio.clip = DesktopStartAudio;
        audio.Play();
	}
	
	void Update () {
        StateTransition();
        StateOnStay(); //for stuff that has to be done every frame in an specifik state
        DebugCorrect1 = correctMarkedArticles;
        DebugWrong1 = wronglyMarkedArticlesAsFalse;
        DebugWrong2 = wronglyMarkedArticlesAsTrue;
	}

    //===== ===== Registration ===== =====
    Timer timer = null;
    System.Action timerFlag;
    public void RegistTimer(Timer handle) {
        if (timer == handle)
            timer = null;
        else {
            timer = handle;
            if(timerFlag != null) {
                timerFlag();
                tutorialFlag = null;
            }
        }
    }

    TutorialHandler tutorial = null;
    System.Action tutorialFlag;
    public void RegistTutorial(TutorialHandler handle) {
        if (tutorial == handle)
            tutorial = null;
        else {
            tutorial = handle;
            if(tutorialFlag != null) {
                tutorialFlag();
                tutorialFlag = null;
            }
        }
    }

    EndScreenWindow end = null;
    public void RegistEnd(EndScreenWindow handle) {
        if (end == handle)
            end = null;
        else {
            end = handle;
        }
    }

    /*NewsGeneration2 NewsGen = null;
    public void RegistNewsGen(NewsGeneration2 handle) {
        if (NewsGen == handle)
            NewsGen = null;
        else
            NewsGen = handle;
    }*///wir haben einen NewsGenerator2 Singelton im GameManager

    //===== ===== Comunicator ===== =====
    public bool RequestStateChange(GameState newState) {
        if ((state == GameState.Playing && newState == GameState.Tutorial) ||
            (state == GameState.Tutorial && newState == GameState.EndScreen) ||
            (state == GameState.Desktop && newState == GameState.EndScreen))
            return false;

        return ChangeState(newState);
    }

    public void RequestStateChange(EventTrigger trigger, EventMessage message) {
        switch (trigger) {
        case EventTrigger.Tutorial:
            switch (message) {
            case EventMessage.Scip:
                goto case EventMessage.Sucsess;
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

    public void LockScreen() {
        audio.clip = LogOutAudio;
        audio.Play();
        //StartCoroutine("SceneManager.LoadScene", StringCollection.INTRO);
        Invoke("LoadIntro", LogOutAudio.length);
    }

    void LoadIntro() {
        SceneManager.LoadScene(StringCollection.INTRO);
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
            timeLeft -= Time.deltaTime;//eventuel erst nach der ersten news machen
            break;
        case GameState.EndScreen:
            break;
        default:
            break;
        }
    }

    bool ChangeState(GameState newState) {
        if (state == newState)
            return false;

        switch (state) { //by exiting this state
        case GameState.Desktop:
            break;
        case GameState.Tutorial:
            if (tutorial)
                tutorial.AbortTutorial();
            //else
                //tutorialFlag += tutorial.AbortTutorial;//TODO: flag setzten
            break;
        case GameState.Playing:
            LogSystem.LogOnFile("===== ===== Game has Ended ===== =====");
            NewsSource.StopGeneration();
            if (timer)
                timer.ChangeStateToTimeShort();
            //else
            //timerFlag += timer.ChangeStateToTimeShort;//TODO: flag setzten
            break;
        case GameState.EndScreen:
            if (end)
                end.Close();
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
            PlayerID = Random.Range(0, 9999999);
            LogSystem.LogOnNewFile("===== ===== Started Tutorial ===== =====");
            LogSystem.LogOnFile("PlayerID = " + PlayerID);
            if (tutorial)
                tutorial.StartTutorial();
            //else
                //tutorialFlag += tutorial.StartTutorial;//TODO: flag setzten
            break;
        case GameState.Playing:
            timeLeft = timeToPlayInSeconds;
            LogSystem.LogOnFile("===== ===== New Game Started ===== =====");
            NewsSource.StartGeneration();
            if (timer)
                timer.ChangeStateToCountdown();
            //else
                //timerFlag += timer.ChangeStateToCountdown;//TODO: flag setzten
            ProgressionManager.RestartCounter();
            break;
        case GameState.EndScreen:
            LogSystem.LogOnFile("Correct: " + correctMarkedArticles + ". False positive: " + wronglyMarkedArticlesAsTrue + ". False negative: " + wronglyMarkedArticlesAsFalse);
            if (end)
                end.Show();
            break;
        default:
            break;
        }
        return true;
    }
}
