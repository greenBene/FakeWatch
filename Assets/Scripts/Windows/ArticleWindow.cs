using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleWindow : Window {
	
	public NewsField headlineField, zeitungField, journalistField, ortField, datumField, ressortField;

	// Use this for initialization
	void Start () {
		SetPosition(RandomPosition());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private Vector2 RandomPosition()
    {
        float halfVerticalSize = GetComponent<RectTransform>().rect.height / 2;
        float halfHorizontalSize = GetComponent<RectTransform>().rect.width / 2;

        return new Vector2(UnityEngine.Random.Range(0 + halfHorizontalSize, Screen.width - halfHorizontalSize), UnityEngine.Random.Range(0 + halfVerticalSize, Screen.height - halfVerticalSize));
    }
}
