using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsGeneration : MonoBehaviour {

    public GameObject articlePrefab;

    private int articleCount = 0;

    [Range(0, 120)] [SerializeField] float startDuration = 60f;
    [Range(0, 1)] [SerializeField] float rate = 0.9f;
    [SerializeField] float timeToPlayInSeconds = 600f;
    [SerializeField] int newsWithoutInvokingAutmatically = 4;


    private int newsTillAutoInvoke;
    private NewsSource newsSource;
    private float currentDurationBetweenNews;
    public float timeLeft;

    private bool hasEnded = false;


    private int correctMarkedArticles = 0;
    private int wronglyMarkedArticlesAsTrue = 0;
    private int wronglyMarkedArticlesAsFalse = 0;

    private AudioSource source;
    public AudioClip winning, newMessage, end;

    public Image endScreen;
    public Text endText;
    public GameObject restartButton;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        currentDurationBetweenNews = startDuration;
        timeLeft = timeToPlayInSeconds;
        newsTillAutoInvoke = newsWithoutInvokingAutmatically;

        newsSource = new NewsSourceForReal();

        Invoke("ShowNextNews", 1f);
    }

	private void Update()
	{
        if (!hasEnded)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                hasEnded = true;
                ShowEndScreen();
            }
        }
	}

	public void GenerateArticle(News news) {
        if (hasEnded) return;
        GameObject newArticle = Instantiate(articlePrefab, transform);
        newArticle.transform.SetSiblingIndex(4);
        source.clip = newMessage;
        source.Play();
        newArticle.GetComponent<Article>().Assign(news, this);
        articleCount++;
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
            source.clip = winning;
            source.Play();
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

    public void ShowEndScreen(){
        // Todo Show End screen

        source.clip = end;
        source.Play();
        endScreen.enabled = true;
        endText.enabled = true;
        endText.text = "Mitarbeiter Evaluation von FactcheckerIn ID: 0189310. \n Sie haben " + correctMarkedArticles + " Nachrichten korrekt auf ihren Warheitsgehalt beurteilt. Dagegen haben Sie " + wronglyMarkedArticlesAsTrue + " falsche Nachrichten als wahr " + "und " + wronglyMarkedArticlesAsFalse + " wahre Nachrichten als falsch eingestuft.";
        restartButton.SetActive(true);



        print("Result: " +
              "correctMarkedArticles: " + correctMarkedArticles + "\n" +
              "wronglyMarkedArticlesAsTrue: " + wronglyMarkedArticlesAsTrue + "\n" +
              "wronglyMarkedArticlesAsFalse: " + wronglyMarkedArticlesAsFalse + "\n");
    }
}
