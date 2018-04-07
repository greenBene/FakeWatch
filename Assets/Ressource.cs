using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ressource : MonoBehaviour {

    int ressource;
    private Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LowerRessource()
    {
        ressource--;
        text.text = ressource.ToString();
    }

    public void AddRessource()
    {
        ressource++;
        text.text = ressource.ToString();

    }
}
