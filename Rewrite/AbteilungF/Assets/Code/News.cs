using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class News : ScriptableObject
{
	private bool myIsFake;
	private newsElement myLhs;
	private newsElement myRhs;
	private Dictionary<newsElement, string> myContent;
	private ILocalisator myLoalisator;
	[SerializeField] MonoBehaviour myPrefab;

	private NewsRefHolder myRefHolder;

	public News(Dictionary<newsElement, string> aContent, ILocalisator aLocalisator)
	{
		myIsFake = false;
		myContent = aContent;
		myLoalisator = aLocalisator;
	}

	public News(Dictionary<newsElement, string> aContent, ILocalisator aLocalisator, newsElement aLhs, newsElement aRhs)
	{
		myIsFake = true;
		myLhs = aLhs;
		myRhs = aRhs;
		myContent = aContent;
		myLoalisator = aLocalisator;
	}

	public void Show()
	{
		myPrefab = Instantiate(myPrefab);
		myRefHolder = myPrefab.GetComponent<NewsRefHolder>();

		Data.GetInstance().myLanguage.OnValueChangeWithState += UpdateLanguage;
		UpdateLanguage(Data.GetInstance().myLanguage.value);
	}

	void UpdateLanguage(language aLanguage)
	{
		myRefHolder.myTitle.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.title]);
		myRefHolder.myAutor.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.autor]);
		myRefHolder.myNewspaper.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.newspaper]);
		myRefHolder.myPlace.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.place]);
		myRefHolder.myDate.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.date]);
		myRefHolder.myAreaOfExpertise.text = myLoalisator.GetLocaString(aLanguage, myContent[newsElement.areaOfExpertise]);
	}

	public void Kill()
	{
		if (Data.Exists()) {
			Data.GetInstance().myLanguage.OnValueChangeWithState -= UpdateLanguage;
		}
		Destroy(myPrefab);
	}

	public ICommand MarkAs(bool aFake)
	{
		Kill();

		if (aFake == myIsFake) {
			return new NullCommand();
		} else {
			if (myIsFake) {
				return new ErrorMessageCommand(myLoalisator, myLhs, myRhs);
			} else {
				return new WasCorrectCommand(myLoalisator);
			}
		}
	}

	class ErrorMessageCommand : ICommand
	{
		private ILocalisator myLocalisator;
		private newsElement myLhs;
		private newsElement myRhs;

		public ErrorMessageCommand(ILocalisator aLocalisator, newsElement aLhs, newsElement aRhs)
		{
			myLocalisator = aLocalisator;
			myLhs = aLhs;
			myRhs = aRhs;
		}

		public void Execute()
		{
			throw new System.NotImplementedException();
		}
	}

	class WasCorrectCommand : ICommand
	{
		private ILocalisator myLocalisator;

		public WasCorrectCommand(ILocalisator aLocalisator)
		{
			myLocalisator = aLocalisator;
		}

		public void Execute()
		{
			throw new System.NotImplementedException();
		}
	}

	class NullCommand : ICommand
	{
		public void Execute() { }
	}
}
