﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewsGeneration2 : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    bool ActivAtStart = false;

    [Header("Artical Variables")]
    public GameObject articlePrefab;


    [SerializeField] int newsWithoutInvokingAutmatically = 4;
    [Range(0, 120)] [SerializeField] float startDuration = 60f;
    [Range(0, 1)] [SerializeField] float rate = 0.9f;
    [SerializeField] bool UseAdaptiveSystem = false;
    [SerializeField] float AdaptiveStartDuration = 20f;
    [SerializeField] float AdaptivePressure = 1f;
    [SerializeField] float AdaptiveForgiveness = 0.7f;
    [SerializeField] float AdaptiveMinDuration = 2f;

    [Header("No Funktion")]
    [SerializeField] float timeToPlayInSeconds = 600f;

    private int correctMarkedArticles = 0;
    private int wronglyMarkedArticlesAsTrue = 0;
    private int wronglyMarkedArticlesAsFalse = 0;

    public Image endScreen;
    public Text endText;
    public GameObject restartButton;

    private int newsTillAutoInvoke;
    private NewsSource newsSource;

    [Header("Debug")]
    [SerializeField] private float currentDurationBetweenNews;
    [SerializeField] private int articleCount = 0;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetString("language") == "german"){
            newsSource = new NewsSourceForReal();
        } else {
            newsSource = new NewsSourceForRealEn();
        }

        if(ActivAtStart)
            StartGeneration();
    }

	private void Update()
	{
	}

    public void StartGeneration()
    {
        articleCount = 0;
        if (UseAdaptiveSystem)
            currentDurationBetweenNews = AdaptiveStartDuration + AdaptivePressure; //to compensate first News Invoce
        else
            currentDurationBetweenNews = startDuration;
        newsTillAutoInvoke = newsWithoutInvokingAutmatically;
        Invoke("NextNewsInitiater", 1f);
    }

    public void StopGeneration() {
        CancelInvoke();
        foreach (ArticleWindow it in GameManager.MainScreen.GetComponentsInChildren<ArticleWindow>()) {
            it.Destroy();
        }
    }

    private void NextNewsInitiater() {
        if (UseAdaptiveSystem) {
            AdaptiveNextNewsInitiator();
            return;
        }
        ShowNextNews();
        currentDurationBetweenNews *= rate;
        currentDurationBetweenNews = Mathf.Clamp(currentDurationBetweenNews, 0.25f, 120f);
        Invoke("NextNewsInitiater", currentDurationBetweenNews);
    }

    /// <summary>
    /// adapteds difficulty to playerspeed
    /// </summary>
    /// <remarks>
    /// AdaptiveSlowDown should be smaler than AdaptiveSpeedUp
    /// </remarks>
    private void AdaptiveNextNewsInitiator() {
        CancelInvoke("AdaptiveNextNewsInitiator");
        if (articleCount > 0)
            currentDurationBetweenNews += AdaptiveForgiveness;
        else
            currentDurationBetweenNews -= AdaptivePressure;
        if (currentDurationBetweenNews < AdaptiveMinDuration)
            currentDurationBetweenNews = AdaptiveMinDuration;

        ShowNextNews();
        Invoke("AdaptiveNextNewsInitiator", currentDurationBetweenNews);
    }

    private void ShowNextNews() {
        GenerateArticle(newsSource.GetNextNews(2));
    }

    private void GenerateArticle(News news) {
        GameObject newArticle = Instantiate(articlePrefab, GameManager.MainScreen.transform);
        newArticle.transform.SetSiblingIndex(4);
        newArticle.GetComponent<ArticleWindow>().AssignNews(news);
        articleCount++;
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
