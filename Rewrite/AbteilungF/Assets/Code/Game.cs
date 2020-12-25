using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbteilungF
{
	public class Game : MonoBehaviour
	{
		public System.Action OnGameHasEnded;

		[SerializeField] News myNewsPrototype;

		IProgression myProgression;
		INewsFactory myFactory;
		NotificationHandler myNotificationHandler = null;
		bool myIsInTutorial = true;
		Timer myTimer;

		float myLastNews;
		float myCurrentDelay;

		SortedSet<News> myNewses = new SortedSet<News>();

		public void MyStart(NotificationHandler aNotificationHandler)
		{
			Kill();

			myNotificationHandler = aNotificationHandler;

			myFactory = new SimpleNewsFactory();
			myFactory.SetNewsPrototype(myNewsPrototype);

			myProgression = new DummyProgression();
			//myProgression = new TutorialProgression();
			myIsInTutorial = true;

			myTimer = new Timer();

			myTimer.OnCountdownEnded += Kill;
			myTimer.OnCountdownEnded += EndOfGame;

			myLastNews = Time.time;
			myCurrentDelay = myProgression.GetCurrentDelay();

			Data.GetInstance().myCorrect.value = 0;
			Data.GetInstance().myFalsePositive.value = 0;
			Data.GetInstance().myFalseNegative.value = 0;
		}

		private void Update()
		{
			if (myLastNews + myCurrentDelay <= Time.time) {
				var news = myProgression.TriggerNews(myFactory, Data.GetInstance().myLocalisator);
				news.Show(() => OnClickFake(news), () => OnClickCorrect(news));
				myNewses.Add(news);

				myLastNews = Time.time;
				myCurrentDelay = myProgression.GetCurrentDelay();

				if (myIsInTutorial && myProgression.HasReachedMaxProgression()) {
					myIsInTutorial = false;
					//myProgression = new DynamicProgression();
				}
			}
		}

		private void OnDestroy()
		{
			Kill();
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
			foreach (var it in myNewses) {
				it.Kill();
			}
			myNotificationHandler.MyReset();
		}

		private void EndOfGame()
		{
			OnGameHasEnded?.Invoke();
		}
	}
}
