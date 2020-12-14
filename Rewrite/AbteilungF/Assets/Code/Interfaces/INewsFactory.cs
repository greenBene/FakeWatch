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
	void SetNewsPrototype(News aNews);
	News GetNextNews(ILocalisator aLocalisator);
	News GetNextNews(List<newsElement> aContainsElements, ILocalisator aLocalisator);
}
