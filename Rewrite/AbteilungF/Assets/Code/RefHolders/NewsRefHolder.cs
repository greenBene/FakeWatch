using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace AbteilungF
{
	public class NewsRefHolder : MonoBehaviour
	{
		public System.Action OnClickFake;
		public System.Action OnClickCorrect;

		public TextMeshProUGUI myTitle;
		public TextMeshProUGUI myAutor;
		public TextMeshProUGUI myNewspaper;
		public TextMeshProUGUI myPlace;
		public TextMeshProUGUI myDate;
		public TextMeshProUGUI myAreaOfExpertise;

		private void Start()
		{
			if (!myTitle) {
				Debug.LogError("Title Not Set");
				Destroy(gameObject);
				return;
			}
			if (!myAutor) {
				Debug.LogError("Autor Not Set");
				Destroy(gameObject);
				return;
			}
			if (!myNewspaper) {
				Debug.LogError("Newspaper Not Set");
				Destroy(gameObject);
				return;
			}
			if (!myPlace) {
				Debug.LogError("Place Not Set");
				Destroy(gameObject);
				return;
			}
			if (!myDate) {
				Debug.LogError("Date Not Set");
				Destroy(gameObject);
				return;
			}
			if (!myAreaOfExpertise) {
				Debug.LogError("AreaOfExpertise Not Set");
				Destroy(gameObject);
				return;
			}
		}

		public void ClickFake()
		{
			OnClickFake?.Invoke();
		}

		public void ClickCorrect()
		{
			OnClickCorrect?.Invoke();
		}
	}
}
