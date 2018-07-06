using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private NewsSourceForReal newsSource;

	// Use this for initialization
	void Start () {
        newsSource = new NewsSourceForReal();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public News GenerateNews()
    {
        // if you want to do progression logic, please do it here instead of in the newssource!
        return newsSource.GetNextNews(2);
    }
}
