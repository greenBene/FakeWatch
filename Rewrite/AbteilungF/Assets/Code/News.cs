using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "NewsPrototype", menuName = "NewsPrototype")]
public class News : ScriptableObject
{
	private bool myIsCorrect;
	private newsElement myLhs;
	private newsElement myRhs;
	private List<newsElement> myContent;
	private ILocalisator myLoalisator;
	[SerializeField] MonoBehaviour myPrefab;

	private NewsRefHolder myRefHolder;

	public News(List<newsElement> aContent, ILocalisator aLocalisator)
	{
		myIsCorrect = false;
		myContent = aContent;
		myLoalisator = aLocalisator;
	}

	public News(List<newsElement> aContent, ILocalisator aLocalisator, newsElement aLhs, newsElement aRhs)
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
		myRefHolder.myTitle.text = myContent.Contains(newsElement.title)
			? myLoalisator.GetLocaString(aLanguage, StringCollecton.KeyFromNewsElement(newsElement.title))
			: "";
		myRefHolder.myAutor.text = myContent.Contains(newsElement.autor)
			? myLoalisator.GetLocaString(aLanguage, StringCollecton.KeyFromNewsElement(newsElement.autor))
			: "";
		myRefHolder.myNewspaper.text = myContent.Contains(newsElement.newspaper)
			? myLoalisator.GetLocaString(aLanguage, StringCollecton.KeyFromNewsElement(newsElement.newspaper))
			: "";
		myRefHolder.myPlace.text = myContent.Contains(newsElement.place)
			? myLoalisator.GetLocaString(aLanguage, StringCollecton.KeyFromNewsElement(newsElement.place))
			: "";
		myRefHolder.myDate.text = myContent.Contains(newsElement.date)
			? myLoalisator.GetLocaString(aLanguage, StringCollecton.KeyFromNewsElement(newsElement.date))
			: "";
		myRefHolder.myAreaOfExpertise.text = myContent.Contains(newsElement.areaOfExpertise)
			? myLoalisator.GetLocaString(aLanguage, StringCollecton.KeyFromNewsElement(newsElement.areaOfExpertise))
			: "";

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

		private TextMeshProUGUI myText;

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
			myText = myHandler.CreateNotification();
			Data.GetInstance().myLanguage.OnValueChangeWithState += ChangLang;
			ChangLang(Data.GetInstance().myLanguage.value);
		}

		public void ChangLang(language aLanguage)
		{
			myText.text = myLocalisator.GetLocaString(aLanguage, StringCollecton.KeyFromConnection(myLhs, myRhs));
		}
	}

	class WasCorrectCommand : ICommand
	{
		private IProgression myProgression;
		private NotificationHandler myHandler;
		private ILocalisator myLocalisator;

		private TextMeshProUGUI myText;

		public WasCorrectCommand(IProgression aProgression, NotificationHandler aHandler, ILocalisator aLocalisator)
		{
			myProgression = aProgression;
			myHandler = aHandler;
			myLocalisator = aLocalisator;
		}

		public void Execute()
		{
			myProgression.SetFalsePositive();
			myText = myHandler.CreateNotification();
			Data.GetInstance().myLanguage.OnValueChangeWithState += ChangLang;
			ChangLang(Data.GetInstance().myLanguage.value);
		}

		public void ChangLang(language aLanguage)
		{
			myText.text = myLocalisator.GetLocaString(aLanguage, StringCollecton.NO_CONNECTION);
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
