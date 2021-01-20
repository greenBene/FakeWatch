using System.Collections.Generic;
using AbteilungF.SNF;
using UnityEngine;

namespace AbteilungF
{
	public class SimpleNewsFactory : INewsFactory
	{
		private News myNewsPrototype;

		private List<Node> myNodes;
		Dictionary<newsElement, List<newsElement>> myConnections;
		private INodeFactory myNodeFactory = new LegacyNodeFactory(UnityEngine.Application.streamingAssetsPath + "/factsDE.txt");

		private float myEdge;

		public SimpleNewsFactory()
		{
			myNodes = myNodeFactory.GetNodes();
			myConnections = myNodeFactory.GetConnectionMap();
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
			NodeCollection worker;
			if(Random.value > myEdge) {
				worker = new NodeCollection();
			} else {
				var firstElement = (newsElement)Random.Range(0, (int)newsElement.size);
				worker = new NodeCollection(firstElement, myConnections[firstElement][Random.Range(0, myConnections[firstElement].Count)]);
			}

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
