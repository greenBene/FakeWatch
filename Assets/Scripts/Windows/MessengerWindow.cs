using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MessengerState { incoming, resting, movingUp, outgoing };

public class MessengerWindow : Window, IStateMachine<MessengerState> {

    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private Text message;

    private MessengerState state;

    // Use this for initialization
    public override void Start () {
        
	}

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Show()
    {
        Show("No text set!");
    }

    public void Show(string messageString)
    {
        SetPosition(Screen.width, Screen.height + 76); // maybe a bit more offset?
        message.text = messageString;
        state = MessengerState.incoming;
    }

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
        throw new System.NotImplementedException();
    }

    public bool RequestStateChange(MessengerState newState)
    {
        throw new System.NotImplementedException();
    }

    public void StateTransition()
    {
        throw new System.NotImplementedException();
    }
}
