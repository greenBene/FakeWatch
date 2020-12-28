using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AsserTOOLres
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class FitToText : MonoBehaviour
	{
		[SerializeField] bool myVariableHight = true;

		TextMeshProUGUI myText;

		private void Start()
		{
			myText = GetComponent<TextMeshProUGUI>();
		}

		private void FixedUpdate()
		{
			((RectTransform)transform).sizeDelta = new Vector2(
				myVariableHight ? ((RectTransform)transform).sizeDelta.x : myText.preferredWidth,
				myVariableHight ? myText.preferredHeight : ((RectTransform)transform).sizeDelta.y);
		}
	}
}
