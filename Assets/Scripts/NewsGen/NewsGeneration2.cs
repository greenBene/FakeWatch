using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewsGeneration2 : MonoBehaviour {

    public GameObject articlePrefab;

    private int articleCount = 0;

    [Range(0, 120)] [SerializeField] float startDuration = 60f;
    [Range(0, 1)] [SerializeField] float rate = 0.9f;
    [SerializeField] float timeToPlayInSeconds = 600f;
    [SerializeField] int newsWithoutInvokingAutmatically = 4;


    private int newsTillAutoInvoke;
    private NewsSource newsSource;
    private float currentDurationBetweenNews;

    private bool hasEnded = false;


    private int correctMarkedArticles = 0;
    private int wronglyMarkedArticlesAsTrue = 0;
    private int wronglyMarkedArticlesAsFalse = 0;

    public Image endScreen;
    public Text endText;
    public GameObject restartButton;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetString("language") == "german"){
            newsSource = new NewsSourceForReal();
        } else {
            newsSource = new NewsSourceForRealEn();
        }

        StartGeneration();
    }

	private void Update()
	{
	}

    public void StartGeneration()
    {
        currentDurationBetweenNews = startDuration;
        newsTillAutoInvoke = newsWithoutInvokingAutmatically;
        Invoke("NextNewsInitiater", 1f);
    }

    public void StopGeneration() {
        CancelInvoke();
    }

	private void GenerateArticle(News news) {
        if (hasEnded) return;
        GameObject newArticle = Instantiate(articlePrefab, transform);
        newArticle.transform.SetParent(GameManager.MainScreen.transform);
        newArticle.transform.SetSiblingIndex(4);
        newArticle.GetComponent<ArticleWindow>().AssignNews(news);
        articleCount++;
    }

    private void NextNewsInitiater() {
        if (hasEnded) return;
        ShowNextNews();
        currentDurationBetweenNews *= rate;
        currentDurationBetweenNews = Mathf.Clamp(currentDurationBetweenNews, 0.25f, 120f);
        Invoke("NextNewsInitiater", currentDurationBetweenNews);
    }

    private void ShowNextNews() {
        if (hasEnded) return;
        GenerateArticle(newsSource.getNextNews());
    }

    public void RegisterSolvedNews()
    {
        articleCount--;

        if (newsTillAutoInvoke > 0)
        {
            newsTillAutoInvoke--;
            if (newsTillAutoInvoke <= 0)
            {
                Invoke("NextNewsInitiater", currentDurationBetweenNews);
            }
        }

        if (articleCount <= 0) // we never want an empty screen -> force new article
        {
            articleCount = 0;
            CancelInvoke("NextNewsInitiater"); // we need to stop the delayed Invoke, otherwise it will plop up probably shortly after the now forced new article
            Invoke("NextNewsInitiater", 1f);
        }
    }

    /*
     * returns if answer is correct
     *
     */
    public bool Answer(bool isFake, bool newsIsRejected) {
        articleCount--;

        if(newsTillAutoInvoke > 0){
            newsTillAutoInvoke--;
            if(newsTillAutoInvoke <= 0){
                Invoke("NextNewsInitiater", currentDurationBetweenNews);
            }
        }

        if(articleCount <= 0) {
            articleCount = 0;
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

    /*private void ShowEndScreen(){
        // Todo Show End screen
        endScreen.enabled = true;
        endText.enabled = true;

        if (PlayerPrefs.GetString("language") == "german")
        {
            endText.text = "Mitarbeiter Evaluation von FactcheckerIn ID: 0189310. \n Sie haben " + correctMarkedArticles + " Nachrichten korrekt auf ihren Warheitsgehalt beurteilt. Dagegen haben Sie " + wronglyMarkedArticlesAsTrue + " falsche Nachrichten als wahr " + "und " + wronglyMarkedArticlesAsFalse + " wahre Nachrichten als falsch eingestuft.";
        } else
        {
            endText.text = "Employee Evaluation of Factchecker ID: 0189310. \n You have checked " + correctMarkedArticles + " news correctly based on their truth. You marked " + wronglyMarkedArticlesAsTrue + " fake news as true " + "and " + wronglyMarkedArticlesAsFalse + " true news as wrong.";
        }

        restartButton.SetActive(true);



        print("Result: " +
              "correctMarkedArticles: " + correctMarkedArticles + "\n" +
              "wronglyMarkedArticlesAsTrue: " + wronglyMarkedArticlesAsTrue + "\n" +
              "wronglyMarkedArticlesAsFalse: " + wronglyMarkedArticlesAsFalse + "\n");
    }*/
}
