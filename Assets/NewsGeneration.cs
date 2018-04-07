using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsGeneration : MonoBehaviour {

    public GameObject articlePrefab;

	// Use this for initialization
	void Start () {
        Generate("NOT FAKE", "GA", "bild", "12.10.2070", "Hamburg", true);
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
}
