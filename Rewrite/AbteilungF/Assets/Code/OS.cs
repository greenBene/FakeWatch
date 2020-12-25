using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace AbteilungF
{
	public class OS : Singleton<OS>
	{
		Dictionary<IExecutable, WindowRefHolder> myExecutables;
		[SerializeField] GameObject myWindowPrototype;
		[SerializeField] NotificationHandler myNotificationHandler;

		public void StartExe(IExecutable aExecutable)
		{
			if (myExecutables.ContainsKey(aExecutable)) {
				return;
			}
			var refHolder = Instantiate(myWindowPrototype, transform).GetComponent<WindowRefHolder>();
			myExecutables[aExecutable] = refHolder;

			aExecutable.Init(refHolder.myExeContent);
			aExecutable.OnFinished += () => HandleExeFinished(aExecutable);
			aExecutable.OnRequestPause += () => HandleRequestPause(aExecutable);
			aExecutable.OnRequestSize += (float aWidth, float aHight) => HandleRequestSize(aExecutable, aWidth, aHight);

			refHolder.OnClose += aExecutable.Kill;
			refHolder.OnMinimice += aExecutable.Pause;
		}

		public NotificationHandler GetNotificationHandler()
		{
			return myNotificationHandler;
		}

		void HandleExeFinished(IExecutable aExecutable)
		{
			if (!myExecutables.ContainsKey(aExecutable)) {
				return;
			}
			var refHolder = myExecutables[aExecutable];
			myExecutables.Remove(aExecutable);

			Destroy(refHolder.gameObject);

			aExecutable.Kill();
		}

		void HandleRequestPause(IExecutable aExecutable)
		{
			if (!myExecutables.ContainsKey(aExecutable)
				|| !myExecutables[aExecutable].gameObject.activeSelf) {
				return;
			}

			myExecutables[aExecutable].gameObject.SetActive(false);
			aExecutable.Pause();
		}

		void HandleRequestSize(IExecutable aExecutable, float aWidth, float aHight)
		{
			if (!myExecutables.ContainsKey(aExecutable)) {
				return;
			}
			var rectTransform = (RectTransform)myExecutables[aExecutable].transform;

			Vector2 boarder = rectTransform.sizeDelta - myExecutables[aExecutable].myExeContent.sizeDelta;

			rectTransform.sizeDelta = new Vector2(boarder.x + aWidth, boarder.y + aHight);
		}
	}
}
