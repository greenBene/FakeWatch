using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NewsField : MonoBehaviour {

    private Text info;
    public InfoType type;

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
	
	public void SetInfo(string newInfo) {
        if (info == null)
        {
            info = GetComponent<Text>();
        }
        info.text = newInfo;
	}
}
