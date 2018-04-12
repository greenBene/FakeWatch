using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Article : MonoBehaviour {

    private bool isFake;
    private bool dragging;
    private string error;

    public Text headlineField, zeitungField, journalistField, ortField, datumField, ressortField;
    private Vector3 distanceToMouse;
    private NewsGeneration newsGeneration;

    public float fadeOutTransparency, fadeOutSpeed;

    public void Assign (News news, NewsGeneration ng){
        this.isFake = news.isFake;
        ressortField.text = news.ressort;
        headlineField.text = news.headline;
        zeitungField.text = news.newspaper.ToUpper();
        journalistField.text = news.author;
        ortField.text = news.location;
        datumField.text = news.date;
        newsGeneration = ng;
        error = news.error;
    }

	// Use this for initialization
	void Start () {
        transform.position = RandomPosition();
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

    public void MarkAsTrue() {
        bool correct =  newsGeneration.Answer(isFake, false);

        if(correct){
            Destroy(gameObject);
        }else{
            GameObject.Find("notification").GetComponent<Notification>().Spawn("WRONG", error);

            WrongAnswer();
        }

    }

    public void WrongAnswer() {

        Destroy(gameObject);
    }

    public void MarkAsFake() {
        bool correct = newsGeneration.Answer(isFake, true);
        if(correct)
        {
            Destroy(gameObject);
        } else
        {
            GameObject.Find("notification").GetComponent<Notification>().Spawn("WRONG", "Artikel war korrekt");

            WrongAnswer();
        }
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
