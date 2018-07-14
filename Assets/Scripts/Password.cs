﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Password : MonoBehaviour {
    public string correctPW;
    public InputField pwField;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckPW()
    {
        if (pwField.textComponent.text==correctPW)
        {
            ChangeScene();
        }
        else
        {
            pwField.textComponent.text = "";
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
