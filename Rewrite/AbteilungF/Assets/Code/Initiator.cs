using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AbteilungF
{
	public class Initiator : MonoBehaviour
	{
		[System.Serializable]
		struct LocaDict
		{
			public language key1;
			public string key2;
			public System.Tuple<language, string> key { get => new System.Tuple<language, string>(key1, key2); }
			public Sprite value;
		}
		[SerializeField] List<LocaDict> myLocaSprites;

		private void Start()
		{
			Data.GetInstance().mySDK = new LocalDataSDK();

			var locaSprites = new Dictionary<System.Tuple<language, string>, Sprite>();
			foreach(var it in myLocaSprites) {
				locaSprites[it.key] = it.value;
			}
			Data.GetInstance().myLocalisator = new LegacyLocalisator(
				Application.streamingAssetsPath + "/factsDE.txt",
				Application.streamingAssetsPath + "/factsEN.txt",
				Application.streamingAssetsPath + "/CreditsDE.txt",
				Application.streamingAssetsPath + "/CreditsEN.txt",
				Application.streamingAssetsPath + "/SceneDE.txt",
				Application.streamingAssetsPath + "/SceneEN.txt",
				Application.streamingAssetsPath + "/Fehlermeldung_Abteilungsleitung.txt",
				locaSprites);

			SceneManager.LoadScene(StringCollecton.INGAME);

			Destroy(this);
		}
	}
}
