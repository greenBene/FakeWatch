using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AbteilungF
{
	[CreateAssetMenu(fileName = "NewsPrototype", menuName = "NewsPrototype")]
	public class News : ScriptableObject
	{
		private bool myIsCorrect;
		private newsElement myLhs;
		private newsElement myRhs;
		private Dictionary<newsElement, string> myContent;
		private ILocalisator myLoalisator;
		[SerializeField] MonoBehaviour myPrefab;

		private NewsRefHolder myRefHolder;

		public void Setup(Dictionary<newsElement, string> aContent, ILocalisator aLocalisator)
		{
			myIsCorrect = false;
			myContent = aContent;
			myLoalisator = aLocalisator;
		}

		public void Setup(Dictionary<newsElement, string> aContent, ILocalisator aLocalisator, newsElement aLhs, newsElement aRhs)
		{
			myIsCorrect = true;
			myLhs = aLhs;
			myRhs = aRhs;
			myContent = aContent;
			myLoalisator = aLocalisator;
		}

		public void Show(System.Action<News> aFakeCallback, System.Action<News> aCorrectCallback)
		{
			myPrefab = Instantiate(myPrefab);
			myRefHolder = myPrefab.GetComponent<NewsRefHolder>();

			Data.GetInstance().myLanguage.OnValueChangeWithState += UpdateLanguage;
			UpdateLanguage(Data.GetInstance().myLanguage.value);

			myRefHolder.OnClickFake += aFakeCallback;
			myRefHolder.OnClickCorrect += aCorrectCallback;
		}

		void UpdateLanguage(language aLanguage)
		{
			myRefHolder.myTitle.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.title]);
			myRefHolder.myAutor.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.autor]);
			myRefHolder.myNewspaper.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.newspaper]);
			myRefHolder.myPlace.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.place]);
			myRefHolder.myDate.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.date]); //TODO: make date and day work together
			myRefHolder.myAreaOfExpertise.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.areaOfExpertise]);

			myRefHolder.myFake.text = myLoalisator.GetLocaString(aLanguage, StringCollecton.FAKE);
			myRefHolder.myCorrect.text = myLoalisator.GetLocaString(aLanguage, StringCollecton.CORRECT);
		}

		public void Kill()
		{
			if (Data.Exists()) {
				Data.GetInstance().myLanguage.OnValueChangeWithState -= UpdateLanguage;
			}
			Destroy(myPrefab);
		}

		public ICommand MarkAs(bool aCorrect, IProgression aProgression, NotificationHandler aHandler)
		{
			Kill();

			if (aCorrect == myIsCorrect) {
				return new NullCommand(aProgression);
			} else {
				if (myIsCorrect) {
					return new WasCorrectCommand(aProgression, aHandler, myLoalisator);
				} else {
					return new ErrorMessageCommand(aProgression, aHandler, myLoalisator, myLhs, myRhs);
				}
			}
		}

		class ErrorMessageCommand : ICommand
		{
			private IProgression myProgression;
			private NotificationHandler myHandler;
			private ILocalisator myLocalisator;
			private newsElement myLhs;
			private newsElement myRhs;

			private NotificationWindow myNotification;

			public ErrorMessageCommand(IProgression aProgression, NotificationHandler aHandler, ILocalisator aLocalisator, newsElement aLhs, newsElement aRhs)
			{
				myProgression = aProgression;
				myHandler = aHandler;
				myLocalisator = aLocalisator;
				myLhs = aLhs;
				myRhs = aRhs;
			}

			public void Execute()
			{
				myProgression.SetFalseNegative();
				myNotification = myHandler.CreateNotification();
				Data.GetInstance().myLanguage.OnValueChangeWithState += ChangLang;
				ChangLang(Data.GetInstance().myLanguage.value);

				myNotification.OnRemove += Finish;
			}

			void ChangLang(language aLanguage)
			{
				myNotification.ChangeText(myLocalisator.GetLocaString(aLanguage, StringCollecton.KeyFromConnection(myLhs, myRhs)));
			}

			void Finish()
			{
				if (Data.Exists()) {
					Data.GetInstance().myLanguage.OnValueChangeWithState -= ChangLang;
				}
			}
		}

		class WasCorrectCommand : ICommand
		{
			private IProgression myProgression;
			private NotificationHandler myHandler;
			private ILocalisator myLocalisator;

			private NotificationWindow myNotification;

			public WasCorrectCommand(IProgression aProgression, NotificationHandler aHandler, ILocalisator aLocalisator)
			{
				myProgression = aProgression;
				myHandler = aHandler;
				myLocalisator = aLocalisator;
			}

			public void Execute()
			{
				myProgression.SetFalsePositive();
				myNotification = myHandler.CreateNotification();
				Data.GetInstance().myLanguage.OnValueChangeWithState += ChangLang;
				ChangLang(Data.GetInstance().myLanguage.value);

				myNotification.OnRemove += Finish;
			}

			void ChangLang(language aLanguage)
			{
				myNotification.ChangeText(myLocalisator.GetLocaString(aLanguage, StringCollecton.NO_CONNECTION));
			}

			void Finish()
			{
				if (Data.Exists()) {
					Data.GetInstance().myLanguage.OnValueChangeWithState -= ChangLang;
				}
			}
		}

		class NullCommand : ICommand
		{
			private IProgression myProgression;

			public NullCommand(IProgression aProgression)
			{
				myProgression = aProgression;
			}

			public void Execute()
			{
				myProgression.SetCorrect();
			}
		}
	}
}
