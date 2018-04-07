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
    facts.init("Assets/nodes.txt");
    facts.findValid(new List<string> { "Z", "O", "P", "D", "WK" }, new Dictionary<string, string>());

    Assert.AreEqual(1, 1);
  }

}
