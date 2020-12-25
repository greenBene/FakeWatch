using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbteilungF
{
	public class WindowRefHolder : MonoBehaviour
	{
		public System.Action OnClose;
		public System.Action OnMinimice;

		public RectTransform myExeContent;

		private void Start()
		{
			if (!myExeContent) {
				Debug.LogError("Executable Content Not Set");
				Destroy(gameObject);
				return;
			}
		}
	}
}
