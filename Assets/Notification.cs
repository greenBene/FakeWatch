using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    public Text text;
    private Color startColor;

	// Use this for initialization
	void Start () {
        startColor = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
        text.color = new Color(0, 0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn(string message)
    {
        text.text = message;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        GetComponent<Image>().color = startColor;
        text.color = Color.black;

        yield return new WaitForSeconds(1);

        while(GetComponent<Image>().color.a > 0)
        {
            text.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
            GetComponent<Image>().color -= new Color(0, 0, 0, 1 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
	}
        
}
