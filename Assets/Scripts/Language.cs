using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour {

    //Use PlayerPrefs.GetString("language") to get either german or english

    public Sprite germanFlag, englishFlag, logoEnglish, logoGerman;
    public Image button;
    public Image logo;
    public Text loginText;
    public string loginTextGerman, loginTextEnglish;

	// Use this for initialization
	void Start () {
        if(PlayerPrefs.GetString("language") == " ")
            PlayerPrefs.SetString("language", "german");
        PlayerPrefs.SetString("language", "german");
        ChangeTo(PlayerPrefs.GetString("language"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change()
    {
        if (PlayerPrefs.GetString("language") == "german") {
            ChangeTo("english");
            
        } else {
            ChangeTo("german");
        }
    }

    public void ChangeTo(string language)
    {
        if (language == "german")
        {
            PlayerPrefs.SetString("language", "german");
            //button.sprite = germanFlag;
            //loginText.text = loginTextGerman;
            logo.sprite = logoGerman;
            
        }
        else
        {
            PlayerPrefs.SetString("language", "english");
           // button.sprite = englishFlag;
            //loginText.text = loginTextEnglish;
            logo.sprite = logoEnglish;
        }
    }
}


