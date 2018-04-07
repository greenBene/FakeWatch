using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
        News[] allTheNews = new News[5];
        allTheNews[0] = new News();
        allTheNews[1] = new News();
        allTheNews[2] = new News();
        allTheNews[3] = new News();
        allTheNews[4] = new News();

        print(allTheNews[0]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
