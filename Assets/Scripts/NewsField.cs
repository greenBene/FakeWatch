using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsField : MonoBehaviour {

    private Text info;
    public InfoType type;
    private Image Highlight;

	// Use this for initialization
	void Start () {
        if (info == null)
        {
            info = GetComponent<Text>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetInfo(string newInfo, bool isCorrect = false) {
        if (info == null)
        {
            info = GetComponentInChildren<Text>();
        }
        info.text = newInfo;
        if(!Highlight)
            Highlight = GetComponent<Image>();
        Highlight.enabled = isCorrect;
	}
}
