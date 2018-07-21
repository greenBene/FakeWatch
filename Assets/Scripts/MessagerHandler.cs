using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagerHandler : MonoBehaviour {

    [SerializeField] GameObject MessengerPrefab;

    List<MessengerWindow> WindowList;

    public void NewMessage() {
        MessengerWindow newMessenger = Instantiate(MessengerPrefab, GameManager.MainScreen.transform).GetComponent<MessengerWindow>();
        foreach(MessengerWindow it in WindowList) {
            it.SlideUp(5);//TODO: get Message hight
        }
        WindowList.Add(newMessenger);
        newMessenger.Show("Dummy Message");//TODO: richtige Message übergeben
    }
}
