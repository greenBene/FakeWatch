using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AbteilungF
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class StringLocalisator : MonoBehaviour
	{
		public string myKey;
		TextMeshProUGUI myText;

		private void Start()
		{
			myText = GetComponent<TextMeshProUGUI>();
			Data.GetInstance().myLanguage.OnValueChangeWithState += UpdateIcon;
			UpdateIcon(Data.GetInstance().myLanguage.value);
		}

		void UpdateIcon(language aLanguage)
		{
			myText.text = Data.GetInstance().myLocalisator.GetLocaString(aLanguage, myKey);
		}
	}
}
