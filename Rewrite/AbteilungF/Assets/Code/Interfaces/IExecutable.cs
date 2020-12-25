using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbteilungF
{
	public interface IExecutable
	{
		System.Action OnFinished { get; set; }
		System.Action OnRequestPause { get; set; }
		System.Action<float, float> OnRequestSize { get; set; }

		void Init(RectTransform aWindowContent);
		void Pause();
		void Resume();
		void Kill();
	}
}
