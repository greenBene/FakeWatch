using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsGeneration : MonoBehaviour {

    public GameObject articlePrefab;

    private static int articleCount = 0;

    [Range(0, 120)] [SerializeField] float startDuration = 60f;
    [Range(0, 1)] [SerializeField] float rate = 0.9f;


    private NewsSource newsSource;
    private float currentDuration;

	// Use this for initialization
	void Start () {
        currentDuration = startDuration;
        newsSource = NewsSourceCSV.getInstance(gameObject);

        Invoke("NextNewsInitiater", 1f);
    }
	

    public void GenerateArticle(News news)
    {
        
        GameObject newArticle = Instantiate(articlePrefab, transform);
        newArticle.GetComponent<Article>().Assign(news.headline, 
                                                  news.newspaper, 
                                                  news.author,
                                                  news.location,
                                                  news.date,
                                                  news.isFake,
                                                 this);
        NewsGeneration.articleCount++;
    }



    public void NextNewsInitiater(){
        ShowNextNews();
        currentDuration *= rate;
        currentDuration = Mathf.Clamp(currentDuration, 0.25f, 120f);
        Invoke("NextNewsInitiater", currentDuration);
    }

    /*
     *
     *
     */
    public bool Answer(bool isFake, bool newsIsRejected){
        NewsGeneration.articleCount--;

        if(isFake && !newsIsRejected){
            return true;
        }

        return false;
    }


    public void ShowNextNews(){
        GenerateArticle(newsSource.getNextNews());
    }
}
