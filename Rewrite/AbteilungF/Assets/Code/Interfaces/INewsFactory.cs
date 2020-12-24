using System.Collections.Generic;

namespace AbteilungF
{
	public enum newsElement
	{
		title,
		autor,
		newspaper,
		place,
		date,
		day,
		areaOfExpertise,
	}

	public interface INewsFactory
	{
		void SetNewsPrototype(News aNews);
		News GetNextNews(ILocalisator aLocalisator);
		News GetNextNews(List<newsElement> aContainsElements, ILocalisator aLocalisator);
	}
}
