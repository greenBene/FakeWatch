using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbteilungF
{
	public struct highScoreEntry
	{
		string name;
		int correct;
		int falsePositive;
		int falseNegative;
	}

	[System.Serializable]
	public enum language
	{
		deDE,
		enEN,
	}

	public enum achivement
	{

	}

	public interface ISDK
	{
		void UpdateSDK();
		string GetUserName();
		void SetScore(highScoreEntry aEntry);
		List<highScoreEntry> GetHighScoreList();
		void SetLanguage(language aLanguage);
		language GetCurrentLanguage();
		List<language> GetAvailableLanguages();
		void SetAchivement(achivement aAchivement);
	}
}
