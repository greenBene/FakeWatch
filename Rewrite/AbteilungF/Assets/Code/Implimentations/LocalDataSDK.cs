using System.Collections.Generic;

namespace AbteilungF
{
	public class LocalDataSDK : ISDK
	{
		public List<language> GetAvailableLanguages()
		{
			return new List<language>() { language.deDE, language.enEN };
		}

		public language GetCurrentLanguage()
		{
			return language.deDE;
		}

		public List<highScoreEntry> GetHighScoreList()
		{
			return new List<highScoreEntry>();
		}

		public string GetUserName()
		{
			return "";
		}

		public void SetAchivement(achivement aAchivement)
		{
		}

		public void SetLanguage(language aLanguage)
		{
		}

		public void SetScore(highScoreEntry aEntry)
		{
		}

		public void UpdateSDK()
		{
		}
	}
}
