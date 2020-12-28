using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbteilungF
{
	[RequireComponent(typeof(Image))]
	public class ImageLocalisator : MonoBehaviour
	{
		[SerializeField] string myKey;
		Image myIcon;

		private void Start()
		{
			myIcon = GetComponent<Image>();
			Data.GetInstance().myLanguage.OnValueChangeWithState += UpdateIcon;
			UpdateIcon(Data.GetInstance().myLanguage.value);
		}

		void UpdateIcon(language aLanguage)
		{
			myIcon.sprite = Data.GetInstance().myLocalisator.GetLocaSprite(aLanguage, myKey);
		}
	}
}
