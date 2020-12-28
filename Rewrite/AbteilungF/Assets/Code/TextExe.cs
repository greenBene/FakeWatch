using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AbteilungF
{
	[CreateAssetMenu(fileName = "TextExe", menuName = "TextViewer")]
	public class TextExe : Executable
	{
		[SerializeField] string myKey;
		[SerializeField] GameObject myPrefab;

		TextMeshProUGUI myText;

		public override void Init(RectTransform aWindowContent, RectTransform aButtonPanle)
		{
			var element = Instantiate(myPrefab, aWindowContent);
			myText = element.GetComponentInChildren<TextMeshProUGUI>();
			Data.GetInstance().myLanguage.OnValueChangeWithState += UpdateTest;
			UpdateTest(Data.GetInstance().myLanguage.value);
		}

		public override void Kill()
		{
			if (Data.Exists()) {
				Data.GetInstance().myLanguage.OnValueChangeWithState -= UpdateTest;
			}
		}

		public override void Pause()
		{
		}

		public override void Resume()
		{
		}

		void UpdateTest(language aLanguage)
		{
			myText.text = Data.GetInstance().myLocalisator.GetLocaString(aLanguage, myKey);
		}
	}
}
