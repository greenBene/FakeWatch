using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AbteilungF
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class StringLocalisator : MonoBehaviour
	{
		[SerializeField] string myKey;
		TextMeshProUGUI myIcon;

		private void Start()
		{
			myIcon = GetComponent<TextMeshProUGUI>();
			Data.GetInstance().myLanguage.OnValueChangeWithState += UpdateIcon;
			UpdateIcon(Data.GetInstance().myLanguage.value);
		}

		void UpdateIcon(language aLanguage)
		{
			myIcon.text = Data.GetInstance().myLocalisator.GetLocaString(aLanguage, myKey);
		}
	}
}
