using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager s_instance;
    public static GameManager Instance
    {
        get
        {
            if (!s_instance)
            {
                s_instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return s_instance;
        }
    }

    private NewsGeneration s_newsSource;
    public static NewsGeneration NewsSource
    {
        get
        {
            return Instance.s_newsSource;
        }
    }

    // Use this for initialization
    void Start () {
        s_newsSource = GameObject.Find("MainScreen").GetComponent<NewsGeneration>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
