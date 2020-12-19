using UnityEngine;

namespace AbteilungF
{
	public interface ILocalisator
	{
		string GetLocaString(language aLanguage, string aKey);
		Sprite GetLocaSprite(language aLanguage, string aKey);
	}
}
