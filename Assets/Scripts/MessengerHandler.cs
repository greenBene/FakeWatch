﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerHandler : MonoBehaviour {
    
    [SerializeField] GameObject MessengerPrefab;
    [SerializeField] private string pathToErrorMessages;

    private ErrorMessageGenerator generator;
    List<MessengerWindow> WindowList;
    Dictionary<Inconsistency, string[]> Messages;

    private void Start() {
        WindowList = new List<MessengerWindow>();
        generator = new ErrorMessageGenerator(pathToErrorMessages);
    }

    public void NewMessage(Inconsistency handle) {
        MessengerWindow newMessenger = Instantiate(MessengerPrefab, GameManager.MainScreen.transform).GetComponent<MessengerWindow>();
        string message = generator.GetMessage(handle);
        float moveHight = newMessenger.Show(message);

        foreach (MessengerWindow it in WindowList) {
            it.SlideUp(moveHight);
        }

        WindowList.Add(newMessenger);
        LogSystem.LogOnFile("New Message: " + message + " (based on inconsitency " + handle.info1.value + " <-> " + handle.info2.value + ")");
        LogSystem.LogOnFile("Messages on Screen: " + WindowList.Count);
    }

    public void DeleteMessage(MessengerWindow item) {
        WindowList.Remove(item);
    }

    public void DeleteAllMessages() {
        foreach(MessengerWindow it in WindowList) {
            it.RequestStateChange(MessengerState.outgoing);
        }
    }
}
