using System.Collections.Generic;

namespace AbteilungF
{
	public class SimpleNewsFactory : INewsFactory
	{
		private News myNewsPrototype;

		public SimpleNewsFactory()
		{

		}

		public News GetNextNews(ILocalisator aLocalisator)
		{
			return GetNextNews(new List<newsElement> {
				newsElement.areaOfExpertise,
				newsElement.autor,
				newsElement.date,
				newsElement.newspaper,
				newsElement.place,
				newsElement.title},
				aLocalisator);
		}

		public News GetNextNews(List<newsElement> aContainsElements, ILocalisator aLocalisator)
		{
			var keys = new Dictionary<newsElement, string>();
			News news = new News();

			foreach (var it in aContainsElements) {
				keys[it] = ""; // TODO: find a key
			}

			news.Setup(keys, aLocalisator);
			return news;
		}

		public void SetNewsPrototype(News aNews)
		{
			myNewsPrototype = aNews;
		}
	}
}
