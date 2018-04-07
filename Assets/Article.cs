using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Article : MonoBehaviour {



    public string headline, zeitung, journalist, ort, datum;
    public bool fake;
    public Text headlineField, zeitungJournalistField, ortField, datumField;

    private Ressource ressource;
    private NewsGeneration newsGeneration;

    private bool dragging;
    private Vector3 distanceToMouse;

    public void Assign (string headline, 
                        string zeitung, 
                        string journalist,
                        string ort, 
                        string datum, 
                        bool fake,
                        NewsGeneration ng)
    {
        this.headline = headline;
        this.zeitung = zeitung;
        this.journalist = journalist;
        this.ort = ort;
        this.fake = fake;
        this.datum = datum;

        headlineField.text = headline;
        zeitungJournalistField.text = zeitung.ToUpper() + " / " + journalist;
        ortField.text = ort;
        datumField.text = datum;
        newsGeneration = ng;
    }

	// Use this for initialization
	void Start () {
        transform.position = RandomPosition();
        ressource = GameObject.Find("ressource").GetComponent<Ressource>();
    }



    // Update is called once per frame
    void Update () {
        if(dragging)
            transform.position = Input.mousePosition + distanceToMouse;
    }

    Vector2 RandomPosition()
    {
        float halfVerticalSize = GetComponent<RectTransform>().rect.height / 2;
        float halfHorizontalSize = GetComponent<RectTransform>().rect.width / 2;

        return new Vector2(UnityEngine.Random.Range(0 + halfHorizontalSize, Screen.width - halfHorizontalSize), UnityEngine.Random.Range(0 + halfVerticalSize, Screen.height - halfVerticalSize));
    }

    public void True()
    {
        
        if(fake)
        {
            ressource.LowerRessource();
        } else
        {
            ressource.AddRessource();

        }
        newsGeneration.nextNews();

        Destroy(gameObject);

    }

    public void Fake()
    {
        if(fake)
        {
            ressource.AddRessource();
        } else
        {
            ressource.LowerRessource();

        }

        newsGeneration.nextNews();

        Destroy(gameObject);
    }

    public void StartDragging()
    {
        distanceToMouse = transform.position - Input.mousePosition;
        dragging = true;
    }

    public void StopDragging()
    {
        dragging = false;
    }
}
