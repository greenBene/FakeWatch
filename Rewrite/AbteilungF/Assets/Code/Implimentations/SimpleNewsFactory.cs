using System.Collections.Generic;

public class SimpleNewsFactory : INewsFactory
{
	public News GetNextNews(ILocalisator aLocalisator)
	{
		throw new System.NotImplementedException();
	}

	public News GetNextNews(List<newsElement> aContainsElements, ILocalisator aLocalisator)
	{
		throw new System.NotImplementedException();
	}
}
