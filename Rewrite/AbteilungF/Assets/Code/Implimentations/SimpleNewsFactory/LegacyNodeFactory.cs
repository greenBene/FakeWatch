using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AbteilungF.SNF
{
	public class LegacyNodeFactory : INodeFactory
	{
		string myFile;

		List<Node> myNodes = null;
		Dictionary<newsElement, List<newsElement>> myConnections = null;

		public LegacyNodeFactory(string aPath)
		{
			if (!System.IO.File.Exists(aPath)) {
				return;
			}

			myFile = "\r\n" + System.IO.File.ReadAllText(aPath) + "\r\n";
		}

		public Dictionary<newsElement, List<newsElement>> GetConnectionMap() {
			if(myConnections == null) {
				Init();
			}
			return myConnections;
		}

		public List<Node> GetNodes()
		{
			if(myNodes == null) {
				Init();
			}
			return myNodes;
		}

		void Init() {
			var nodes = new Dictionary<string, Node>();

			var nodeDeclarations = Regex.Matches(myFile, "\\n((.*)_.*):");
			foreach(Match it in nodeDeclarations) {
				nodes[it.Groups[1].Value] = new Node(NewsElementFromString(it.Groups[2].Value), it.Groups[1].Value);
			}

			var combinations = Regex.Matches(myFile, "\\n(.*) -> (.*)\\r");
			foreach(Match it in combinations) {
				if(!nodes.ContainsKey(it.Groups[1].Value)) {
					Debug.LogWarning("node " + it.Groups[1].Value + " cant be found");
					continue;
				}
				if(!nodes.ContainsKey(it.Groups[2].Value)) {
					Debug.LogWarning("node " + it.Groups[1].Value + " cant be found");
					continue;
				}
				var firstNode = nodes[it.Groups[1].Value];
				var secondNode = nodes[it.Groups[2].Value];
				firstNode.AddNode(secondNode);
				secondNode.AddNode(firstNode);

				if(myConnections[firstNode.GetElement()] == null) {
					myConnections[firstNode.GetElement()] = new List<newsElement>();
				}
				myConnections[firstNode.GetElement()].Add(secondNode.GetElement());
				if(myConnections[secondNode.GetElement()] == null) {
					myConnections[secondNode.GetElement()] = new List<newsElement>();
				}
				myConnections[secondNode.GetElement()].Add(firstNode.GetElement());
			}

			myNodes = new List<Node>();
			foreach(var it in nodes) {
				myNodes.Add(it.Value);
			}
		}

		newsElement NewsElementFromString(string aKey)
		{
			if (aKey == "ZEITUNG") {
				return newsElement.newspaper;
			}
			if (aKey == "AUTOR") {
				return newsElement.autor;
			}
			if (aKey == "ORT") {
				return newsElement.place;
			}
			if (aKey == "TAG") {
				return newsElement.day;
			}
			if (aKey == "FACHGEBIET") {
				return newsElement.areaOfExpertise;
			}
			if (aKey == "EVENT") {
				return newsElement.title;
			}
			if (aKey == "DATE") {
				return newsElement.date;
			}
			throw new System.ArgumentException("Key was: " + aKey);
		}
	}
}
