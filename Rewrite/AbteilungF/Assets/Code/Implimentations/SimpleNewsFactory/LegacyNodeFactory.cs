using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AbteilungF.SNF
{
	public class LegacyNodeFactory : INodeFactory
	{
		string myFile;

		public LegacyNodeFactory(string aPath)
		{
			if (!System.IO.File.Exists(aPath)) {
				return;
			}

			myFile = System.IO.File.ReadAllText(aPath);
		}

		public List<Node> GetNodes()
		{
			var nodes = new Dictionary<string, Node>();

			var nodeDeclarations = Regex.Matches(myFile, "^((.*)_.*):");
			foreach (Match it in nodeDeclarations) {
				nodes[it.Groups[0].Value] = new Node(NewsElementFromString(it.Groups[1].Value), it.Groups[0].Value);
			}

			var combinations = Regex.Matches(myFile, "^(.*)->(.*)$");
			foreach (Match it in combinations) {
				nodes[it.Groups[0].Value].AddNode(nodes[it.Groups[1].Value]);
				nodes[it.Groups[1].Value].AddNode(nodes[it.Groups[0].Value]);
			}

			var list = new List<Node>();
			foreach (var it in nodes) {
				list.Add(it.Value);
			}
			return list;
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
			throw new System.ArgumentException();
		}
	}
}
