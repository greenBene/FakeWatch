using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

static class CatCatToId
{
  public static Dictionary<Category, Dictionary<Category, int>> data = new Dictionary<Category, Dictionary<Category, int>>();
}

public class Frontier
{
  public List<Category> missingCats;
  public Dictionary<Category, Element> foundPairs;
  public HashSet<int> usedConstraintIds;

  public Frontier(List<Category> missingCats,
Dictionary<Category, Element> foundPairs,
HashSet<int> usedConstraintIds)
  {
    this.missingCats = missingCats;
    this.foundPairs = foundPairs;
    this.usedConstraintIds = usedConstraintIds;
  }

  public Frontier RemoveMissingCat(Category cat)
  {
    var cats = new List<Category>(missingCats);
    cats.Remove(cat);
    return new Frontier(cats, foundPairs, usedConstraintIds);
  }

  public Frontier AddFoundPairs(Category cat, Element el)
  {
    var pairs = new Dictionary<Category, Element>(foundPairs);
    pairs.Add(cat, el);
    return new Frontier(missingCats, pairs, usedConstraintIds);
  }

  public Frontier AddUsedConstraintId(int id)
  {
    var ids = new HashSet<int>(usedConstraintIds);
    ids.Add(id);
    return new Frontier(missingCats, foundPairs, ids);
  }
}

public class Category
{
  public string symbol;
  public List<Element> members = new List<Element>();

  public Category(string symbol)
  {
    this.symbol = symbol;
  }

  public void Shuffle()
  {
    foreach (var m in members) m.Shuffle();
    members.Shuffle();
  }

  public Frontier FulfillConstraints(Frontier frontier)
  {
    foreach (var m in members)
    {
      Console.WriteLine("First Layer {0}", m.name);
      var workingFront = m.CanFulfillConstraints(frontier.AddFoundPairs(this, m));
      if (workingFront != null) return workingFront;
    }
    return null;
  }

}

public class Element
{
  public Dictionary<Category, List<Element>> to = new Dictionary<Category, List<Element>>();
  public Category memberOf;
  public string name;
  public Element(string name, Category memberOf)
  {
    this.name = name;
    this.memberOf = memberOf;
  }

  public void Shuffle()
  {
    foreach (var list in to.Values) list.Shuffle();
  }

  private Frontier CanReachAll(Frontier frontier, List<Category> reachable)
  {
    Console.Write("canReachAll {0}", this.name);
    reachable.ForEach(r => Console.Write(r.symbol + ", "));
    Console.WriteLine();
    if (reachable.Count == 0) return frontier;
    var cat = reachable.First();
    var nextReachable = new List<Category>(reachable);
    nextReachable.Remove(cat);
    var currentId = CatCatToId.data[this.memberOf][cat];
    if (frontier.usedConstraintIds.Contains(currentId)) return CanReachAll(frontier, nextReachable);
    if (!to.ContainsKey(cat))
    {
      Console.WriteLine("{0} has no {1}.", this.name, cat.symbol);
      return null;
    }

    Console.WriteLine("adding catcatId {0}", currentId);
    var nextFront = frontier.AddUsedConstraintId(currentId).RemoveMissingCat(cat);
    foreach (var el in to[cat])
    {
      Console.WriteLine("  try {0} -> {1}", cat.symbol, el.name);
      var nextFront2 = nextFront.AddFoundPairs(cat, el);
      var workingFront = el.CanFulfillConstraints(nextFront2);
      if (workingFront != null)
      {
        Console.WriteLine("! try {0}", el.name);
        var workingFront2 = CanReachAll(workingFront, nextReachable);
        if (workingFront2 != null) return workingFront2;
      }
      Console.WriteLine("X try {0} -> {1}", cat.symbol, el.name);
    }
    return null;

  }

  public Frontier CanFulfillConstraints(Frontier frontier)
  {
    Console.WriteLine("In member {0}", this.name);

    if (CatCatToId.data.ContainsKey(memberOf))
    {
      foreach (var cat in CatCatToId.data[memberOf].Keys)
      {
        var currentId = CatCatToId.data[this.memberOf][cat];
        if (frontier.usedConstraintIds.Contains(currentId)) continue;
        if (!to.ContainsKey(cat))
        {
          Console.WriteLine("{0} has no {1}!", this.name, cat.symbol);
          return null;
        }
        if (frontier.foundPairs.ContainsKey(cat))
        {
          foreach (var el in to[cat])
          {
            Console.WriteLine("  TRY {0}", el.name);
            if (el == frontier.foundPairs[cat])
            {
              Console.WriteLine("! TRY {0}", el.name);
              return frontier.AddUsedConstraintId(currentId);
            }
            Console.WriteLine("X TRY {0}", el.name);
          }
          return null;
        }
      }
    }

    List<Category> reachable = new List<Category>();
    foreach (var cat in frontier.missingCats)
    {
      if (CatCatToId.data.ContainsKey(memberOf) && CatCatToId.data[memberOf].ContainsKey(cat))
      {
        reachable.Add(cat);
      }

    }
    return this.CanReachAll(frontier, reachable);
  }
}

public class Facts
{

  private Dictionary<string, Category> symbolToCategory = new Dictionary<string, Category>();
  private Dictionary<string, Element> shortNameToElement = new Dictionary<string, Element>();

  public void Init(string path)
  {
    int currentId = 1;
    foreach (var line in File.ReadAllLines(path))
    {
      Match r;
      if ((r = Regex.Match(line, @"(\S+)_(\S+)\s*:\s*(.+)")).Success)
      {
        Category cat = null;
        if (!symbolToCategory.TryGetValue(r.Groups[1].Value, out cat))
        {
          cat = new Category(r.Groups[1].Value);
          symbolToCategory[cat.symbol] = cat;
        }
        var e = new Element(r.Groups[3].Value, cat);
        cat.members.Add(e);
        shortNameToElement.Add(r.Groups[1].Value + "_" + r.Groups[2].Value, e);
      };
      if ((r = Regex.Match(line, @"(\S+)\s*->\s*(.+)")).Success)
      {
        Element el1;
        if (!shortNameToElement.TryGetValue(r.Groups[1].Value, out el1))
        {
          throw new Exception("unknown node " + el1);
        }
        Element el2;
        if (!shortNameToElement.TryGetValue(r.Groups[2].Value, out el2))
        {
          throw new Exception("unknown node " + el2);
        }
        Console.WriteLine(el1.name + " => " + el2.name);
        if (!(CatCatToId.data.ContainsKey(el1.memberOf) && CatCatToId.data[el1.memberOf].ContainsKey(el2.memberOf)))
        {
          if (!CatCatToId.data.ContainsKey(el1.memberOf))
          {
            CatCatToId.data.Add(el1.memberOf, new Dictionary<Category, int> { { el2.memberOf, currentId } });
          }
          else
          {
            CatCatToId.data[el1.memberOf].Add(el2.memberOf, currentId);
          }
          if (!CatCatToId.data.ContainsKey(el2.memberOf))
          {
            CatCatToId.data.Add(el2.memberOf, new Dictionary<Category, int> { { el1.memberOf, currentId } });
          }
          else
          {
            CatCatToId.data[el2.memberOf].Add(el1.memberOf, currentId);
          }
          Console.WriteLine("With id {0}", currentId);
          currentId += 1;
        }
        List<Element> l1;
        if (!el1.to.TryGetValue(el2.memberOf, out l1))
        {
          l1 = new List<Element>();
          el1.to.Add(el2.memberOf, l1);
        }
        l1.Add(el2);

        List<Element> l2;
        if (!el2.to.TryGetValue(el1.memberOf, out l2))
        {
          l2 = new List<Element>();
          el2.to.Add(el1.memberOf, l2);
        }
        l2.Add(el1);
      };
    }
    // edges.ForEach(Console.WriteLine);
  }

  public bool FindValid(List<string> symbols, Dictionary<string, string> existingConstraints)
  {

    var cats = new List<Category>(symbols.Select(s => symbolToCategory[s]));
    foreach (var cat in cats) cat.Shuffle();

    var frontier = new Frontier(cats, new Dictionary<Category, Element>(), new HashSet<int>());
    foreach (var k in existingConstraints.Keys)
    {

      frontier.foundPairs.Add(symbolToCategory[k], shortNameToElement[existingConstraints[k]]);
    }
    while (frontier.missingCats.Count > 0)
    {
      foreach (var cat in frontier.missingCats) Console.WriteLine("Missing: {0}", cat.symbol);
      var next = frontier.missingCats.First();
      Console.WriteLine("Try {0}", next.symbol);

      frontier = next.FulfillConstraints(frontier.RemoveMissingCat(next));
      if (frontier == null)
      {
        Console.WriteLine("FAIL!!");
        return false;
      }
    }
    Console.WriteLine("SUCCESS!");
    foreach (var cat in frontier.foundPairs.Keys)
    {
      Console.WriteLine("{0}: {1}", cat.symbol, frontier.foundPairs[cat].name);
    }
    return false;
  }

}
