using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsGeneration : MonoBehaviour {

    public GameObject articlePrefab;

    [Range(0, 120)] [SerializeField] float startDuration = 60f;
    [Range(0, 1)] [SerializeField] float rate = 0.9f;


    private NewsSource newsSource;
    private float currentDuration;

	// Use this for initialization
	void Start () {
        currentDuration = startDuration;
        newsSource = new NewsSourceDummy();

        nextNews();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Generate(News news)
    {
        
        GameObject newArticle = Instantiate(articlePrefab, transform);
        newArticle.GetComponent<Article>().Assign(news.headline, 
                                                  news.newspaper, 
                                                  news.author,
                                                  news.location,
                                                  news.date,
                                                  news.isFake);
    }

    private void nextNews(){
        Generate(newsSource.getNextNews());
        print("now");

        currentDuration += rate;
        Invoke("nextNews", currentDuration);
    }
}
