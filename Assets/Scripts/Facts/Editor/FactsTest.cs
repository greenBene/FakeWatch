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
    facts.Init("Assets/facts.txt");
    facts.FindValid(new List<string> { "ZEITUNG", "AUTOR", "ORT", "REGION" }, new Dictionary<string, string> { });

    Assert.AreEqual(1, 1);
  }

}
