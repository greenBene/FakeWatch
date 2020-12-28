using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AbteilungF
{
	public abstract class Executable : ScriptableObject
	{
		public System.Action OnFinished { get; set; }
		public System.Action OnRequestPause { get; set; }
		public System.Action<float, float> OnRequestSize { get; set; }

		public abstract void Init(RectTransform aWindowContent, RectTransform aButtonPanle);
		public abstract void Pause();
		public abstract void Resume();
		public abstract void Kill();
	}
}
