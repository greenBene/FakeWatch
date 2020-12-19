﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AbteilungF
{
	public class NotificationHandler : MonoBehaviour
	{
		[SerializeField] GameObject myMessengerPrefab;
		[SerializeField] float myTimeToLife;
		[SerializeField] float mySpeed;

		LinkedList<NotificationWindow> myWindowList;

		public NotificationWindow CreateNotification()
		{
			NotificationWindow newMessenger = Instantiate(myMessengerPrefab, transform).GetComponent<NotificationWindow>();
			newMessenger.Setup(myWindowList.AddFirst(newMessenger), mySpeed, myTimeToLife);

			return newMessenger;
		}

		public void MyReset()
		{
			foreach (var it in myWindowList) {
				it.Disappear();
			}
			myWindowList.Clear();
		}
	}
}
