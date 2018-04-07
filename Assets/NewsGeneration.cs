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

	private void Update()
	{
        print(NewsGeneration.articleCount);
	}


	public void GenerateArticle(News news) {
        
        GameObject newArticle = Instantiate(articlePrefab, transform);
        newArticle.GetComponent<Article>().Assign(news, this);
        NewsGeneration.articleCount++;
    }



    public void NextNewsInitiater() {
        ShowNextNews();
        currentDuration *= rate;
        currentDuration = Mathf.Clamp(currentDuration, 0.25f, 120f);
        Invoke("NextNewsInitiater", currentDuration);
    }


    public void ShowNextNews() {
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

        if (isFake != newsIsRejected) {
            return false;
        }

        return true;
    }
}
