using System.Collections.Generic;

namespace AbteilungF.SNF
{
	public class Node
	{
		List<Node> myConnections = new List<Node>();
		newsElement myElement;
		string myKey;

		public Node(newsElement aElement, string aKey)
		{
			myElement = aElement;
			myKey = aKey;
		}

		public void AddNode(Node aNode)
		{
			myConnections.Add(aNode);
		}

		public newsElement GetElement()
		{
			return myElement;
		}

		public void CollapsNode(ref Dictionary<newsElement, string> aNews)
		{
			aNews[myElement] = myKey;
		}

		public bool IsNodeValide(Node aNode)
		{
			if (aNode.GetElement() == myElement) {
				return false;
			}

			bool hasAnyNodeOfType = false;
			foreach (var it in myConnections) {
				if (it == aNode) {
					return true;
				}

				if (it.GetElement() == aNode.GetElement()) {
					hasAnyNodeOfType = true;
				}
			}
			return !hasAnyNodeOfType;
		}
	}
}
