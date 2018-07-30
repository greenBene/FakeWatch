using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTextHeight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text text = GetComponent<Text>();
        if (!text)
            return;
        text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.preferredHeight);
	}
}
