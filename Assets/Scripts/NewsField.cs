using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsField : MonoBehaviour {
	
	public Text info;

	// Use this for initialization
	void Start () {
        info.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetInfo(string newInfo) {
        info.text = newInfo;
	}
}
