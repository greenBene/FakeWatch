using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationHandler : MonoBehaviour
{
	[SerializeField] GameObject myMessengerPrefab;
	[SerializeField] float myTimeToLife;

	LinkedList<NotificationWindow> myWindowList;

	public NotificationWindow CreateNotification()
	{
		NotificationWindow newMessenger = Instantiate(myMessengerPrefab, transform).GetComponent<NotificationWindow>();
		newMessenger.Setup(myWindowList.AddFirst(newMessenger), myTimeToLife);

		return newMessenger;
	}

	public void MyReset()
	{
		foreach(var it in myWindowList) {
			it.Disappear();
		}
		myWindowList.Clear();
	}
}
