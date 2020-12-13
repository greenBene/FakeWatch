using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum newsElement
{
	title,
	autor,
	newspaper,
	place,
	date,
	areaOfExpertise,
}

public interface INewsFactory
{
	News GetNextNews(ILocalisator aLocalisator);
	News GetNextNews(List<newsElement> aContainsElements, ILocalisator aLocalisator);
}
