﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsRefHolder : MonoBehaviour
{
	public System.Action<News> OnClickFake;
	public System.Action<News> OnClickCorrect;

	public TextMeshProUGUI myTitle;
	public TextMeshProUGUI myAutor;
	public TextMeshProUGUI myNewspaper;
	public TextMeshProUGUI myPlace;
	public TextMeshProUGUI myDate;
	public TextMeshProUGUI myAreaOfExpertise;
	public TextMeshProUGUI myFake;
	public TextMeshProUGUI myCorrect;

	[HideInInspector] public News myNews;

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
		OnClickFake?.Invoke(myNews);
	}

	public void ClickCorrect()
	{
		OnClickCorrect?.Invoke(myNews);
	}
}
