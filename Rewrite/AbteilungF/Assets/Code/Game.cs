using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] News myNewsPrototype;

	IProgression myProgression;
	INewsFactory myFactory;
	NotificationHandler myNotificationHandler = null;
	bool myIsInTutorial = true;
	Timer myTimer;

	float myLastNews;
	float myCurrentDelay;

	SortedSet<News> myNewses = new SortedSet<News>();

	public void MyStart(NotificationHandler aNotificationHandler, float aStartDelay)
	{
		Kill();

		myNotificationHandler = aNotificationHandler;

		myFactory = new SimpleNewsFactory();
		myFactory.SetNewsPrototype(myNewsPrototype);

		myProgression = new TutorialProgression();
		myIsInTutorial = true;

		myTimer = new Timer();

		myTimer.OnCountdownEnded += Kill;

		myLastNews = Time.time;
		myCurrentDelay = aStartDelay;
	}

	private void Update()
	{
		if(myLastNews + myCurrentDelay <= Time.time) {
			var news = myProgression.TriggerNews(myFactory);
			news.Show(OnClickFake, OnClickCorrect);
			myNewses.Add(news);

			myLastNews = Time.time;
			myCurrentDelay = myProgression.GetCurrentDelay();

			if (myIsInTutorial && myProgression.HasReachedMaxProgression()) {
				myIsInTutorial = false;
				myProgression = new DynamicProgression();
			}
		}
	}

	private void OnClickFake(News aNews)
	{
		aNews.MarkAs(false, myProgression, myNotificationHandler).Execute();
		myCurrentDelay = myProgression.GetCurrentDelay();
		myNewses.Remove(aNews);
	}

	private void OnClickCorrect(News aNews)
	{
		aNews.MarkAs(true, myProgression, myNotificationHandler).Execute();
		myCurrentDelay = myProgression.GetCurrentDelay();
		myNewses.Remove(aNews);
	}

	private void Kill()
	{
		foreach(var it in myNewses) {
			it.Kill();
		}
		myNotificationHandler.MyReset();
	}
}
