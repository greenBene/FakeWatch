using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AbteilungF
{
	[System.Serializable]
	public abstract class IExecutable : Object
	{
		public System.Action OnFinished { get; set; }
		public System.Action OnRequestPause { get; set; }
		public System.Action<float, float> OnRequestSize { get; set; }

		public void Init(RectTransform aWindowContent) { }
		public void Pause() { }
		public void Resume() { }
		public void Kill() { }
	}
}
