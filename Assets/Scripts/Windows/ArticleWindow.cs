﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomExtensions;

public class ArticleWindow : Window {

    private NewsField[] fields;
    private News news;
    public AudioClip correctSound, wrongSound;
    public int Progression = 0;

    // Use this for initialization
    public override void Start()
    {
        base.SetPosition(RandomPosition());
        base.Show();
    }

    public override void Update()
    {
        base.Update();
    }


    private Vector2 RandomPosition()
    {
        float halfVerticalSize = GetComponent<RectTransform>().rect.height / 2;
        float halfHorizontalSize = GetComponent<RectTransform>().rect.width / 2;
        //dirty fix für aspect ratio, ist nicht ganz richtig antscheinend
        return new Vector2(UnityEngine.Random.Range(0 + halfHorizontalSize * GameManager.MainScreen.transform.localScale.x, Screen.width - halfHorizontalSize * GameManager.MainScreen.transform.localScale.x), UnityEngine.Random.Range(0 + halfVerticalSize, Screen.height - halfVerticalSize));
    }

    public void AssignNews(News news)
    {
        Progression = ProgressionManager.GetProgression();
        bool ProgressFlag = false;
        if (Progression == 3)
            ProgressFlag = true;

        this.news = news;
        if(fields == null)
        {
            fields = GetComponentsInChildren<NewsField>();
        }

        fields.Shuffle();
        foreach (NewsField field in fields)
        {
            bool showCorrect = false;
            if (Progression > 0 && field.type != InfoType.headline && news.GetTruthValue(field.type)) {
                if(!ProgressFlag || (ProgressFlag && field.type != InfoType.date)) {
                    Progression--;
                    showCorrect = true;
                }
            }
            
            field.SetInfo(news.GetInfo(field.type), showCorrect);
        }
        
    }

    public void MarkAs(bool correct)
    {
        LogSystem.LogOnFile("(N = " + !news.IsFake() + " |P = " + correct + ") " + news.ToString());
        AudioSource source = GetComponent<AudioSource>();
        if(news.IsFake() != correct)
        {
            source.clip = correctSound;
        }
        else
        {
            source.clip = wrongSound;
            GameManager.MessengerHandler.NewMessage(news.conflict);
        }
        source.Play();
        GameManager.Instance.Score(!news.IsFake(), correct);
        GameManager.NewsSource.RegisterSolvedNews();
        base.Destroy(source.clip.length);
    }
}
