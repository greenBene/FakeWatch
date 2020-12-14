using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	IProgression myProgression;
	INewsFactory myFactory;

	float myLastNews;
	float myCurrentDelay;

	SortedSet<News> myNewses;

	private void Start()
	{
		myLastNews = Time.time;
		myCurrentDelay = 1; // TODO(andreas): get this to be settable
	}

	private void Update()
	{
		if(myLastNews + myCurrentDelay <= Time.time) {
			var news = myProgression.TriggerNews(myFactory);
			news.Show(OnClickFake, OnClickCorrect);
			myNewses.Add(news);

			myLastNews = Time.time;
			myCurrentDelay = myProgression.GetCurrentDelay();
		}
	}

	private void OnClickFake(News aNews)
	{
		aNews.MarkAs(false, myProgression).Execute();
		myCurrentDelay = myProgression.GetCurrentDelay();
		myNewses.Remove(aNews);
	}

	private void OnClickCorrect(News aNews)
	{
		aNews.MarkAs(true, myProgression).Execute();
		myCurrentDelay = myProgression.GetCurrentDelay();
		myNewses.Remove(aNews);
	}
}
