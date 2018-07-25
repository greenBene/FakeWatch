using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerHandler : MonoBehaviour {

    [SerializeField] GameObject MessengerPrefab;
    [SerializeField] private string pathToErrorMessages;

    private ErrorMessageGenerator generator;
    List<MessengerWindow> WindowList;

    private void Start() {
        WindowList = new List<MessengerWindow>();
        generator = new ErrorMessageGenerator(pathToErrorMessages);
    }

    public void NewMessage(Inconsistency handle) {
        MessengerWindow newMessenger = Instantiate(MessengerPrefab, GameManager.MainScreen.transform).GetComponent<MessengerWindow>();
        float moveHight = newMessenger.Show(generator.GetMessage(handle))/2;

        foreach (MessengerWindow it in WindowList) {
            it.SlideUp(moveHight);
        }

        WindowList.Add(newMessenger);
        LogSystem.LogOnFile("Messages on Screen: " + WindowList.Count);
    }

    public void DeleteMessage(MessengerWindow item) {
        WindowList.Remove(item);
    }
}
