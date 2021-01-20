using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbteilungF.SNF
{
	public class NodeCollection
	{
		List<Node> myCollectedNodes = new List<Node>();

		bool myIsFake = false;
		newsElement myLhs;
		newsElement myRhs;
		Node myFirstNode;
		Node mySecondNode;

		public NodeCollection() { }
		public NodeCollection(newsElement aLhs, newsElement aRhs)
		{
			myIsFake = true;
			myLhs = aLhs;
			myRhs = aRhs;
		}

		public Dictionary<newsElement, string> Collaps()
		{
			var news = new Dictionary<newsElement, string>();

			if(myIsFake) {
				myFirstNode.CollapsNode(ref news);
				mySecondNode.CollapsNode(ref news);
			}

			foreach (var it in myCollectedNodes) {
				it.CollapsNode(ref news);
			}

			return news;
		}

		public List<newsElement> GetContainingElements()
		{
			var elements = new List<newsElement>();

			if(myFirstNode != null) {
				elements.Add(myFirstNode.GetElement());
			}
			if(mySecondNode != null) {
				elements.Add(mySecondNode.GetElement());
			}

			foreach (var it in myCollectedNodes) {
				elements.Add(it.GetElement());
			}

			return elements;
		}

		public bool InsertNode(Node aNode)
		{
			if(myIsFake) {
				var type = aNode.GetElement();
				if(type == myLhs || type == myRhs) {
					if(myFirstNode == null) {
						myFirstNode = aNode;
					} else if(mySecondNode == null
						&& myFirstNode.GetElement() != aNode.GetElement()) {
						mySecondNode = aNode;
					} else {
						return false;
					}
					return true;
				}
			}

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
