using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerHandler : MonoBehaviour {

    [SerializeField] GameObject MessengerPrefab;

    List<MessengerWindow> WindowList;

    private void Start() {
        WindowList = new List<MessengerWindow>();
    }

    public void NewMessage(Inconsistency handle = null) {
        MessengerWindow newMessenger = Instantiate(MessengerPrefab, GameManager.MainScreen.transform).GetComponent<MessengerWindow>();
        float moveHight = newMessenger.Show("Dummy Message")/2;//TODO: richtige Message übergeben

        foreach (MessengerWindow it in WindowList) {
            it.SlideUp(moveHight);
        }

        WindowList.Add(newMessenger);
    }
}
