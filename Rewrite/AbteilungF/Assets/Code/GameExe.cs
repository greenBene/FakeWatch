using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AbteilungF {
	[CreateAssetMenu(fileName = "GameExe", menuName = "Game")]
	public class GameExe : Executable
	{
		[SerializeField] GameObject myGameHandler;
		[SerializeField] GameObject myGoButton;
		[SerializeField] GameObject myTutorialContent;

		Game myGame;

		public override void Init(RectTransform aWindowContent, RectTransform aButtonPanle)
		{
			var element = Instantiate(myTutorialContent, aWindowContent);
			element.GetComponentInChildren<TextMeshProUGUI>().gameObject.AddComponent<StringLocalisator>().myKey = StringCollecton.TUTORIAL;
			var goButton = (RectTransform)Instantiate(myGoButton, aButtonPanle).transform;
			goButton.GetComponent<Button>().onClick.AddListener(OnGo);
		}

		public override void Kill()
		{
			if (myGame) {
				myGame.Kill();
				Destroy(myGame.gameObject);
			}
		}

		public override void Pause()
		{
		}

		public override void Resume()
		{
		}

		void OnGo()
		{
			myGame = Instantiate(myGameHandler).GetComponent<Game>();
			myGame.OnGameHasEnded += Kill;
			OnRequestPause?.Invoke();
		}
	}
}
