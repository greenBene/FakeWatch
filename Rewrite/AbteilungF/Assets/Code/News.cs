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

	public ICommand MarkAs(bool aCorrect, IProgression aProgression)
	{
		Kill();

		if (aCorrect == myIsCorrect) {
			return new NullCommand(aProgression);
		} else {
			if (myIsCorrect) {
				return new WasCorrectCommand(aProgression, myLoalisator);
			} else {
				return new ErrorMessageCommand(aProgression, myLoalisator, myLhs, myRhs);
			}
		}
	}

	class ErrorMessageCommand : ICommand
	{
		private IProgression myProgression;
		private ILocalisator myLocalisator;
		private newsElement myLhs;
		private newsElement myRhs;

		public ErrorMessageCommand(IProgression aProgression, ILocalisator aLocalisator, newsElement aLhs, newsElement aRhs)
		{
			myProgression = aProgression;
			myLocalisator = aLocalisator;
			myLhs = aLhs;
			myRhs = aRhs;
		}

		public void Execute()
		{
			myProgression.SetFalseNegative();
		}
	}

	class WasCorrectCommand : ICommand
	{
		private IProgression myProgression;
		private ILocalisator myLocalisator;

		public WasCorrectCommand(IProgression aProgression, ILocalisator aLocalisator)
		{
			myProgression = aProgression;
			myLocalisator = aLocalisator;
		}

		public void Execute()
		{
			myProgression.SetFalsePositive();
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
