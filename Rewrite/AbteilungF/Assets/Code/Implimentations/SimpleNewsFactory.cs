using System.Collections.Generic;

public class SimpleNewsFactory : INewsFactory
{
	private News myNews;

	public News GetNextNews(ILocalisator aLocalisator)
	{
		throw new System.NotImplementedException();
	}

	public News GetNextNews(List<newsElement> aContainsElements, ILocalisator aLocalisator)
	{
		throw new System.NotImplementedException();
	}

	public void SetNewsPrototype(News aNews)
	{
		myNews = aNews;
	}
}
