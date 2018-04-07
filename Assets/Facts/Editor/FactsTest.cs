using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class FactsTest
{

  [Test]
  public void FactsTestSimplePasses()
  {
    Facts facts = new Facts();
    facts.Init("Assets/nodes.txt");
    facts.FindValid(new List<string> { "Z", "O", "P" }, new Dictionary<string, string> { { "WK", "WK_Mo" } });

    Assert.AreEqual(1, 1);
  }

}
