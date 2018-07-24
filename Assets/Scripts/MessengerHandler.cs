using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerHandler : MonoBehaviour {

    [SerializeField] GameObject MessengerPrefab;

    List<MessengerWindow> WindowList;
    Dictionary<Inconsistency, string[]> Messages;

    private void Start() {
        WindowList = new List<MessengerWindow>();
        //TODO: Phase informations into Messages Dictionatry
    }

    public void NewMessage(Inconsistency handle = null) {
        MessengerWindow newMessenger = Instantiate(MessengerPrefab, GameManager.MainScreen.transform).GetComponent<MessengerWindow>();
        float moveHight = newMessenger.Show("Dummy Message")/2;//TODO: richtige Message übergeben
        //float moveHight = newMessenger.Show(string.Format(Messages[handle][Random.Range(0, Messages[handle].Length)], handle.info1.value, handle.info2.value) / 2;

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
