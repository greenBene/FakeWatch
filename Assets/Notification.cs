﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    public Text text;
    private Color startColor;
    private AudioSource source;
    private Image image;

	// Use this for initialization
	void Start () {
        startColor = text.color;
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
        text.color = new Color(0, 0, 0, 0);
        source = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        text.enabled = false;
        image.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn(string message)
    {
        text.text = message;
        source.Play();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        //GetComponent<Image>().color = startColor;
        text.color = startColor;
        image.enabled = true;
        text.enabled = true;
        yield return new WaitForSeconds(0.2f);

        while(text.color.a > 0)
        {
            text.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
            //GetComponent<Image>().color -= new Color(0, 0, 0, 1 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        text.enabled = false;
        image.enabled = false;
	}
        
}
