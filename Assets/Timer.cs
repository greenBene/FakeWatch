using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text text;
    private NewsGeneration news;

	// Use this for initialization
	void Start () {
        news = GameObject.Find("Canvas").GetComponent<NewsGeneration>();
        text = GetComponent<Text>();
<<<<<<< HEAD

    }
	
=======
	}

>>>>>>> 3d2c639fdc6f877f0a7f6995bb9b1949019e2c89
	// Update is called once per frame
	void Update () {
        string minutes = ((int)(news.timeLeft / 60)).ToString();
        string seconds = ((int)(news.timeLeft % 60)).ToString();
        if (minutes.Length < 2) minutes = "0" + minutes;
        if (seconds.Length < 2) seconds = "0" + seconds;
        text.text =  minutes + ":" + seconds;
<<<<<<< HEAD
=======
        // Debug.Log(seconds);
>>>>>>> 3d2c639fdc6f877f0a7f6995bb9b1949019e2c89
	}
}
