using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleWindow : Window {

    private NewsField[] fields;
    private News news;
    public AudioClip correctSound, wrongSound;

    // Use this for initialization
    void Start()
    {
        base.SetPosition(RandomPosition());
        base.Show();
    }
	
	// Update is called once per frame
	void Update () {
		
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
        AudioSource source = GetComponent<AudioSource>();
        if(news.IsFake() == !correct)
        {
            source.clip = correctSound;
        }
        else
        {
            source.clip = wrongSound;
        }
        source.Play();
        GameManager.NewsSource.RegisterSolvedNews();
        base.Destroy(source.clip.length);
    }
}
