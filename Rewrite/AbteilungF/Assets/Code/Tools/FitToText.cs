using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AsserTOOLres
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class FitToText : MonoBehaviour
	{
		TextMeshProUGUI myText;

		private void Start()
		{
			myText = GetComponent<TextMeshProUGUI>();
		}

		private void FixedUpdate()
		{
			((RectTransform)transform).sizeDelta = new Vector2(((RectTransform)transform).sizeDelta.x, myText.preferredHeight);
		}
	}
}
