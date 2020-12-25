using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbteilungF
{
	public class DummyProgression : IProgression
	{

		public float GetCurrentDelay()
		{
			return 10;
		}

		public bool HasReachedMaxProgression()
		{
			return true;
		}

		public void SetCorrect()
		{
		}

		public void SetFalseNegative()
		{
		}

		public void SetFalsePositive()
		{
		}

		public News TriggerNews(INewsFactory aFactory, ILocalisator aLocalisator)
		{
			return aFactory.GetNextNews(aLocalisator);
		}
	}
}
