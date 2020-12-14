using System.Collections.Generic;

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
