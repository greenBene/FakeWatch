using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour {

    public Sprite logoEnglish, logoGerman;
    private Image logo;

	// Use this for initialization
	void Start () {
        logo = GetComponent<Image>();
        LogSystem.LogOnFile("Game is Played in " + PlayerPrefs.GetString("language"));
        if (PlayerPrefs.GetString("language") == "german")
            logo.sprite = logoGerman;
        else
            logo.sprite = logoEnglish;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
