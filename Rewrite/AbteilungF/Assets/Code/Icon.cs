using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AbteilungF {
	public class Icon : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] Executable myExecutable;

		public void OnPointerClick(PointerEventData eventData)
		{
			if(eventData.clickCount != 2) {
				return;
			}

			OS.GetInstance().StartExe(myExecutable);
		}
	}
}
