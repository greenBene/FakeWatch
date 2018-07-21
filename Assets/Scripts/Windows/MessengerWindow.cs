using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MessengerState { incoming, resting, movingUp, outgoing };

public class MessengerWindow : Window, IStateMachine<MessengerState> {

    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private float time;
    [SerializeField] private Text message;
    [SerializeField] private int SlideStop;

    private MessengerState state;
    private int targetHight;

    // Use this for initialization
    public override void Start () {
        
	}

    // Update is called once per frame
    public override void Update()
    {
        StateTransition();
        StateOnStay();
    }

    public override void Show()
    {
        Show("No text set!");
    }

    public void Show(string messageString)
    {
        SetPosition(Screen.width, Screen.height + 76); // maybe a bit more offset?
        targetHight = (int)transform.position.y;
        time += Time.time;
        message.text = messageString;
        state = MessengerState.incoming;
    }

    public void SlideUp(int distance) {
        targetHight += distance;
    }

    //===== ===== State Mashine ===== =====
    public void StateOnStay()
    {
        switch (state)
        {
            case MessengerState.incoming:
                MoveAbout(-movementSpeed/Time.deltaTime, 0);
                break;
            case MessengerState.resting:
                break;
            case MessengerState.movingUp:
                MoveAbout(0, movementSpeed/Time.deltaTime);
                break;
            case MessengerState.outgoing:
                MoveAbout(movementSpeed/Time.deltaTime, 0);
                break;
        }
    }

    public bool ChangeState(MessengerState newState)
    {
        if (state == newState)
            return false;

        switch (state) {
        case MessengerState.incoming:
            break;
        case MessengerState.movingUp:
            break;
        case MessengerState.outgoing:
            break;
        case MessengerState.resting:
            break;
        default:
            break;
        }

        state = newState;

        switch (state) {
        case MessengerState.incoming:
            break;
        case MessengerState.movingUp:
            break;
        case MessengerState.outgoing:
            break;
        case MessengerState.resting:
            SetPosition(SlideStop, (int)transform.position.y);
            break;
        default:
            break;
        }
        return true;
    }

    public bool RequestStateChange(MessengerState newState)
    {
        throw new System.NotImplementedException();
    }

    public void StateTransition()
    {
        //----- ----- Any State ----- -----
        if (Time.time >= time)
            ChangeState(MessengerState.outgoing);

        switch (state) {
        case MessengerState.incoming:
            if(transform.position.x <= SlideStop) {
                ChangeState(MessengerState.resting);
            }
            break;
        case MessengerState.movingUp:
            if (transform.position.y >= targetHight)
                ChangeState(MessengerState.resting);
            break;
        case MessengerState.outgoing:
            if (transform.position.y >= Screen.width)
                Destroy();
            break;
        case MessengerState.resting:
            if (transform.position.y < targetHight)
                ChangeState(MessengerState.movingUp);
            break;
        default:
            break;
        }
    }
}
