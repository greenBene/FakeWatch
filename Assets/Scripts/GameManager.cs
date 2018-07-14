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

    private NewsGeneration2 s_newsSource;
    public static NewsGeneration2 NewsSource
    {
        get
        {
            return Instance.s_newsSource;
        }
    }

    private Canvas s_mainScreen;
    public static Canvas MainScreen
    {
        get
        {
            return Instance.s_mainScreen;
        }
    }

    // Use this for initialization
    void Start () {
        s_newsSource = GetComponent<NewsGeneration2>();
        s_mainScreen = FindObjectOfType<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
