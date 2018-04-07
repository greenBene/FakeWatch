using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ressource : MonoBehaviour {

    static string RESSOURCE_NAME = "Glaubwürdigkeit";

    int ressource = 5;
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        UpdateText();
	}

    private void UpdateText(){
        text.text = RESSOURCE_NAME + ": " + ressource;
    } 

    public void LowerRessource()
    {
        ressource--;
        UpdateText();
    }

    public void AddRessource()
    {
        ressource++;
        UpdateText();
    }
}
