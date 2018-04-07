using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Article : MonoBehaviour {



    public string headline, zeitung, journalist, ort, datum;
    public bool fake;
    private Text headlineField, zeitungJournalistField, ortField, datumField;


    public void Assign (string headline, string zeitung, string journalist,string ort, string datum, bool fake)
    {
        this.headline = headline;
        this.zeitung = zeitung;
        this.journalist = journalist;
        this.ort = ort;
        this.fake = fake;
        this.datum = datum;

        headlineField = GameObject.Find("headline").GetComponent<Text>();
        zeitungJournalistField = GameObject.Find("zeitung").GetComponent<Text>();
        ortField = GameObject.Find("ort").GetComponent<Text>();
        datumField = GameObject.Find("datum").GetComponent<Text>();

        headlineField.text = headline;
        zeitungJournalistField.text = zeitung.ToUpper() + " / " + journalist;
        ortField.text = ort;
        datumField.text = datum;
    }

	// Use this for initialization
	void Start () {
        transform.position = RandomPosition();
        
    }



    // Update is called once per frame
    void Update () {
		
	}

    Vector2 RandomPosition()
    {
        float halfVerticalSize = GetComponent<RectTransform>().rect.height / 2;
        float halfHorizontalSize = GetComponent<RectTransform>().rect.width / 2;

        return new Vector2(UnityEngine.Random.Range(0 + halfHorizontalSize, Screen.width - halfHorizontalSize), UnityEngine.Random.Range(0 + halfVerticalSize, Screen.height - halfVerticalSize));
    }
}
