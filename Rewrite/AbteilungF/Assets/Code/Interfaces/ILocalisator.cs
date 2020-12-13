using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILocalisator
{
	string GetLocaString(language aLanguage, string aKey);
	Sprite GetLocaSprite(language aLanguage, string aKey);
}
