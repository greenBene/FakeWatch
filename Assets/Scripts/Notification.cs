using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    public Text text, errorReason;
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
        errorReason.enabled = false;
        image.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Spawn(string message, string errorReason)
    {
        text.text = message;
        this.errorReason.text = errorReason;
        source.Play();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        //GetComponent<Image>().color = startColor;
        text.color = startColor;
        image.enabled = true;
        text.enabled = true;
        errorReason.enabled = true;
        errorReason.color = text.color;
        yield return new WaitForSeconds(0.5f);

        while(text.color.a > 0)
        {
            text.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
            errorReason.color = text.color;
            //GetComponent<Image>().color -= new Color(0, 0, 0, 1 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        text.enabled = false;
        errorReason.enabled = false;
        image.enabled = false;
	}
        
}
