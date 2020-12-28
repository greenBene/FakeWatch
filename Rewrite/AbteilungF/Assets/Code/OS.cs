using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace AbteilungF
{
	public class OS : Singleton<OS>
	{
		Dictionary<Executable, WindowRefHolder> myExecutables = new Dictionary<Executable, WindowRefHolder>();
		[SerializeField] GameObject myWindowPrototype;
		[SerializeField] NotificationHandler myNotificationHandler;
		[SerializeField] RectTransform myDesktop;

		public void StartExe(Executable aExecutable)
		{
			if (myExecutables.ContainsKey(aExecutable)) {
				return;
			}
			var refHolder = Instantiate(myWindowPrototype, myDesktop).GetComponent<WindowRefHolder>();
			myExecutables[aExecutable] = refHolder;

			aExecutable.OnFinished += () => HandleExeFinished(aExecutable);
			aExecutable.OnRequestPause += () => HandleRequestPause(aExecutable);
			aExecutable.OnRequestSize += (float aWidth, float aHight) => HandleRequestSize(aExecutable, aWidth, aHight);

			refHolder.OnClose += () => HandleExeFinished(aExecutable);
			refHolder.OnMinimice += aExecutable.Pause;

			aExecutable.Init(refHolder.myExeContent, refHolder.myButtonPanle);
		}

		public NotificationHandler GetNotificationHandler()
		{
			return myNotificationHandler;
		}

		public RectTransform GetDesktop()
		{
			return myDesktop;
		}

		void HandleExeFinished(Executable aExecutable)
		{
			if (!myExecutables.ContainsKey(aExecutable)) {
				return;
			}
			var refHolder = myExecutables[aExecutable];
			myExecutables.Remove(aExecutable);

			Destroy(refHolder.gameObject);

			aExecutable.Kill();
		}

		void HandleRequestPause(Executable aExecutable)
		{
			if (!myExecutables.ContainsKey(aExecutable)
				|| !myExecutables[aExecutable].gameObject.activeSelf) {
				return;
			}

			myExecutables[aExecutable].gameObject.SetActive(false);
			aExecutable.Pause();
		}

		void HandleRequestSize(Executable aExecutable, float aWidth, float aHight)
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
