using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionButtons : MonoBehaviour
{
	[SerializeField] float mySpacing;

	int myChildCount = 0;

	private void FixedUpdate()
	{
		if(transform.childCount == myChildCount) {
			return;
		}
		myChildCount = transform.childCount;

		float nextPosition = 0;
		foreach(RectTransform it in transform) {
			it.pivot = new Vector2(1, 0.5f);
			it.anchoredPosition = new Vector2(-nextPosition, 0);
			nextPosition += it.sizeDelta.x + mySpacing;
		}
	}
}
