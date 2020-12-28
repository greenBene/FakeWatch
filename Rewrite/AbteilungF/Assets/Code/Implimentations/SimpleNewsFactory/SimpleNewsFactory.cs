using System.Collections.Generic;
using AbteilungF.SNF;
using UnityEngine;

namespace AbteilungF
{
	public class SimpleNewsFactory : INewsFactory
	{
		private News myNewsPrototype;

		private List<Node> myNodes;
		private INodeFactory myNodeFactory = new LegacyNodeFactory(UnityEngine.Application.streamingAssetsPath + "/factsDE.txt");

		public SimpleNewsFactory()
		{
			myNodes = myNodeFactory.GetNodes();
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
			NodeCollection worker = new NodeCollection();

			do {
				myNodes.Shuffle();
				foreach (var it in myNodes) {
					if (!aContainsElements.Contains(it.GetElement())
						|| worker.GetContainingElements().Contains(it.GetElement())) {
						continue;
					}

					worker.InsertNode(it);
				}
			} while (!worker.GetContainingElements().AreTheSame(aContainsElements));


			News news = ScriptableObject.Instantiate(myNewsPrototype);
			news.Setup(worker.Collaps(), aLocalisator);
			return news;
		}

		public void SetNewsPrototype(News aNews)
		{
			myNewsPrototype = aNews;
		}
	}
}
