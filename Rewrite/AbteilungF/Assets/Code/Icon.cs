using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AbteilungF {
	public class Icon : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] string myKey;
		[SerializeField] Image myIcon;
		[SerializeField] IExecutable myExecutable;

		private void Start()
		{
			Data.GetInstance().myLanguage.OnValueChangeWithState += UpdateIcon;
			UpdateIcon(Data.GetInstance().myLanguage.value);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if(eventData.clickCount != 2) {
				return;
			}

			OS.GetInstance().StartExe(myExecutable);
		}

		void UpdateIcon(language aLanguage)
		{
			myIcon.sprite = Data.GetInstance().myLocalisator.GetLocaSprite(aLanguage, myKey);
		}
	}
}
