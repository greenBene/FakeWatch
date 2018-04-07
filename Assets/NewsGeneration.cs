using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsGeneration : MonoBehaviour {

    public GameObject articlePrefab;

    private static int articleCount = 0;

    [Range(0, 120)] [SerializeField] float startDuration = 60f;
    [Range(0, 1)] [SerializeField] float rate = 0.9f;
    [SerializeField] float timeToPlayInSeconds = 600f;



    private NewsSource newsSource;
    private float currentDurationBetweenNews;
    public float timeLeft;

    private bool hasEnded = false;

    private int correctMarkedArticles = 0;
    private int wronglyMarkedArticlesAsTrue = 0;
    private int wronglyMarkedArticlesAsFalse = 0;


	// Use this for initialization
	void Start () {
        currentDurationBetweenNews = startDuration;
        timeLeft = timeToPlayInSeconds;

        newsSource = NewsSourceCSV.getInstance(gameObject);

        Invoke("NextNewsInitiater", 1f);
    }

	private void Update()
	{
        if (!hasEnded)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                hasEnded = true;
            }
        }
	}

	public void GenerateArticle(News news) {
        if (hasEnded) return;
        GameObject newArticle = Instantiate(articlePrefab, transform);
        newArticle.GetComponent<Article>().Assign(news, this);
        NewsGeneration.articleCount++;
    }

    public void NextNewsInitiater() {
        if (hasEnded) return;
        ShowNextNews();
        currentDurationBetweenNews *= rate;
        currentDurationBetweenNews = Mathf.Clamp(currentDurationBetweenNews, 0.25f, 120f);
        Invoke("NextNewsInitiater", currentDurationBetweenNews);
    }

    public void ShowNextNews() {
        if (hasEnded) return;
        GenerateArticle(newsSource.getNextNews());
    }

    /*
     * returns if answer is correct
     *
     */
    public bool Answer(bool isFake, bool newsIsRejected) {
        NewsGeneration.articleCount--;

        if(NewsGeneration.articleCount == 0) {
            Invoke("ShowNextNews", 1f);
        }

        if (isFake == newsIsRejected)
        {
            correctMarkedArticles++;
            return true;
        }
        else
        {
            if (newsIsRejected)
                wronglyMarkedArticlesAsTrue++;
            else
                wronglyMarkedArticlesAsFalse++;
            return false;
        }

    }
}
