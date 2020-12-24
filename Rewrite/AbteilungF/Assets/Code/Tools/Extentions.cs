using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
	public static void Shuffle<T>(this List<T> aList)
	{
		int size = aList.Count;
		for(int i = 0; i < size; i++) {
			int other = Random.Range(0, size);

			T element = aList[i];
			aList[i] = aList[other];
			aList[other] = element;
		}
	}

	public static bool AreTheSame<T>(this List<T> aList, List<T> aOther)
	{
		foreach(var it in aList) {
			if (!aOther.Contains(it)) {
				return false;
			}
		}
		foreach(var it in aOther) {
			if (!aList.Contains(it)) {
				return false;
			}
		}
		return true;
	}
}
