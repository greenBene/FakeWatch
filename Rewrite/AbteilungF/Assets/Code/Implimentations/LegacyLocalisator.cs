using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

namespace AbteilungF
{
	public class LegacyLocalisator : ILocalisator
	{
		string myFactsDEPath;
		string myFactsENPath;
		string myCreditsDEPath;
		string myCreditsENPath;
		string mySceneDEPath;
		string mySceneENPath;
		string myNotificationsPath;

		bool myLoadedFactsDE = false;
		bool myLoadedFactsEN = false;
		bool myLoadedCreditsDE = false;
		bool myLoadedCreditsEN = false;
		bool myLoadedSceneDE = false;
		bool myLoadedSceneEN = false;
		bool myLoadedNotifications = false;

		Dictionary<System.Tuple<language, string>, List<string>> myLoca = new Dictionary<System.Tuple<language, string>, List<string>>();
		Dictionary<System.Tuple<language, string>, Sprite> mySprites;

		public LegacyLocalisator(string aFactsDEPath, string aFactsENPath, string aCreditsDEPath, string aCreditsENPath, string aSceneDEPath, string aSceneENPath, string aNotificationsPath, Dictionary<System.Tuple<language, string>, Sprite> aSprites)
		{
			myFactsDEPath = aFactsDEPath;
			myFactsENPath = aFactsENPath;
			myCreditsDEPath = aCreditsDEPath;
			myCreditsENPath = aCreditsENPath;
			mySceneDEPath = aSceneDEPath;
			mySceneENPath = aSceneENPath;
			myNotificationsPath = aNotificationsPath;
			mySprites = aSprites;
		}

		public Sprite GetLocaSprite(language aLanguage, string aKey)
		{
			return mySprites[new System.Tuple<language, string>(aLanguage, aKey)];
		}

		public string GetLocaString(language aLanguage, string aKey)
		{
			aKey = KeyToLegacyKey(aKey);

			{
				string file = "";
				string file2 = "";
				switch (aLanguage) {
				case language.deDE:
					if (!myLoadedFactsDE
						&& File.Exists(myFactsDEPath)) {
						file = File.ReadAllText(myFactsDEPath);
						myLoadedFactsDE = true;
					}
					if (!myLoadedCreditsDE
						&& File.Exists(myCreditsDEPath)) {
						SaveAdd(new System.Tuple<language, string>(language.deDE, StringCollecton.CREDITS), File.ReadAllText(myCreditsDEPath));
						myLoca[new System.Tuple<language, string>(language.enEN, StringCollecton.CREDITS)] = myLoca[new System.Tuple<language, string>(language.deDE, StringCollecton.CREDITS)];
						myLoadedCreditsDE = true;
					}
					if (!myLoadedSceneDE
						&& File.Exists(mySceneDEPath)) {
						file2 = File.ReadAllText(mySceneDEPath);
						myLoadedSceneDE = true;
					}
					break;
				case language.enEN:
					if (!myLoadedFactsEN
						&& File.Exists(myFactsENPath)) {
						file = File.ReadAllText(myFactsENPath);
						myLoadedFactsEN = true;
					}
					if (!myLoadedCreditsEN
						&& File.Exists(myCreditsENPath)) {
						SaveAdd(new System.Tuple<language, string>(language.deDE, StringCollecton.CREDITS), File.ReadAllText(myCreditsENPath));
						myLoca[new System.Tuple<language, string>(language.enEN, StringCollecton.CREDITS)] = myLoca[new System.Tuple<language, string>(language.deDE, StringCollecton.CREDITS)];
						myLoadedCreditsEN = true;
					}
					if (!myLoadedSceneEN
						&& File.Exists(mySceneENPath)) {
						file2 = File.ReadAllText(mySceneENPath);
						myLoadedSceneEN = true;
					}
					break;
				default:
					throw new System.NotSupportedException();
				}
				var matcher = Regex.Matches(file, "[\\n|^](.*): (.*)[\\r|$]");
				foreach (Match it in matcher) {
					SaveAdd(new System.Tuple<language, string>(aLanguage, it.Groups[1].Value), it.Groups[2].Value);
				}

				matcher = Regex.Matches(file2, "[\\n|^](.*): (.*)[\\r|$]");
				foreach (Match it in matcher) {
					SaveAdd(new System.Tuple<language, string>(aLanguage, it.Groups[1].Value), it.Groups[2].Value);
				}
			}

			if (!myLoadedNotifications
				&& File.Exists(myNotificationsPath)) {
				string[] file = File.ReadAllLines(myNotificationsPath);

				bool nextLineIsKey = true;
				string currentKey = "";
				foreach (var line in file) {
					if (nextLineIsKey) {
						currentKey = line;
						nextLineIsKey = false;
						continue;
					}
					if (line == "") {

						myLoca[new System.Tuple<language, string>(language.enEN, currentKey)] = myLoca[new System.Tuple<language, string>(language.deDE, currentKey)];
						nextLineIsKey = true;
						continue;
					}

					SaveAdd(new System.Tuple<language, string>(language.deDE, currentKey), line);
				}
				myLoadedNotifications = true;
			}

			if(myLoca.ContainsKey(new System.Tuple<language, string>(aLanguage, aKey))) {
				var options = myLoca[new System.Tuple<language, string>(aLanguage, aKey)];
				return options[Random.Range(0, options.Count)];
			}
			return "";
		}

		string KeyToLegacyKey(string aKey)
		{
			if (aKey == StringCollecton.NO_CONNECTION) {
				return "KORREKT";
			}
			if (aKey == StringCollecton.TITLE) {
				return "EVENT";
			}
			if(aKey == StringCollecton.AUTOR) {
				return "AUTOR";
			}
			if(aKey == StringCollecton.NEWSPAPER) {
				return "ZEITUNG";
			}
			if(aKey == StringCollecton.PLACE) {
				return "ORT";
			}
			if(aKey == StringCollecton.DATE) {
				return "DATE";
			}
			if(aKey == StringCollecton.AOE) {
				return "FACHGEBIET";
			}

			return aKey;
		}

		void SaveAdd(System.Tuple<language, string> aKey, string aValue)
		{
			if (!myLoca.ContainsKey(aKey)) {
				myLoca[aKey] = new List<string>();
			}
			myLoca[aKey].Add(aValue);
		}
	}
}
