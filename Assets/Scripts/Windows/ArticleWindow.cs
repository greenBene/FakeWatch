using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleWindow : Window {

    private NewsField[] fields;
    private News news;
    public AudioClip correctSound, wrongSound;

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

        return new Vector2(UnityEngine.Random.Range(0 + halfHorizontalSize, Screen.width - halfHorizontalSize), UnityEngine.Random.Range(0 + halfVerticalSize, Screen.height - halfVerticalSize));
    }

    public void AssignNews(News news)
    {
        this.news = news;
        if(fields == null)
        {
            fields = GetComponentsInChildren<NewsField>();
        }
        foreach (NewsField field in fields)
        {
            field.SetInfo(news.GetInfo(field.type));
        }
    }

    public void MarkAs(bool correct)
    {
        LogSystem.LogOnFile("(N = " + news.IsFake() + " |P = " + correct + ") " + news.ToString());
        AudioSource source = GetComponent<AudioSource>();
        if(news.IsFake() == correct)//hier wahr ein !correct. stimmt das?
        {
            source.clip = correctSound;
        }
        else
        {
            source.clip = wrongSound;
            GameManager.MessengerHandler.NewMessage();//TODO: Inconsistensy übergeben
        }
        source.Play();
        GameManager.Instance.Score(news.IsFake(), correct);
        GameManager.NewsSource.RegisterSolvedNews();
        base.Destroy(source.clip.length);
    }
}
