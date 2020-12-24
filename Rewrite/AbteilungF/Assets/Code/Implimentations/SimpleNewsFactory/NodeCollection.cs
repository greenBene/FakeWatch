using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbteilungF.SNF
{
	public class NodeCollection
	{
		List<Node> myCollectedNodes = new List<Node>();

		public Dictionary<newsElement, string> Collaps()
		{
			var news = new Dictionary<newsElement, string>();

			foreach (var it in myCollectedNodes) {
				it.CollapsNode(ref news);
			}

			return news;
		}

		public List<newsElement> GetContainingElements()
		{
			List<newsElement> elements = new List<newsElement>();

			foreach (var it in myCollectedNodes) {
				elements.Add(it.GetElement());
			}

			return elements;
		}

		public bool InsertNode(Node aNode)
		{
			foreach (var it in myCollectedNodes) {
				if (!it.IsNodeValide(aNode)) {
					return false;
				}
			}
			myCollectedNodes.Add(aNode);
			return true;
		}
	}
}
