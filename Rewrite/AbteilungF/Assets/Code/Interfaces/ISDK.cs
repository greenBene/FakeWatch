﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct highScoreEntry
{
	string name;
	int correct;
	int falsePositive;
	int falseNegative;
}

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
	string GetUserName();
	void SetScore(highScoreEntry aEntry);
	List<highScoreEntry> GetHighScoreList();
	void SetLanguage(language aLanguage);
	language GetCurrentLanguage();
	List<language> GetAvailableLanguages();
	void SetAchivement(achivement aAchivement);
}
