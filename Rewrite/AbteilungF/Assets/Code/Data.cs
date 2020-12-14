using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

public class Data : Singleton<Data>
{
	public Observable<language> myLanguage;
	public ISDK mySDK;
	public ILocalisator myLocalisator;
}
