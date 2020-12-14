using UnityEngine;

public interface ILocalisator
{
	string GetLocaString(language aLanguage, string aKey);
	Sprite GetLocaSprite(language aLanguage, string aKey);
}
