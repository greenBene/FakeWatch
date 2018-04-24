using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

/*
 *  Dummy News Generator for testing
 */
class HeadlineInfoEn
{
  public string headline;
  public string eventCode;
  public string topicCode;
  public bool isReal;

  private static Dictionary<string, string> ressortKeyToLabel = new Dictionary<string, string>{
    {"FACHGEBIET_Po", "Politics"},
    {"FACHGEBIET_Wi", "Economy"},
    {"FACHGEBIET_Sc", "Science"},
  };

  public HeadlineInfoEn(string headline, string eventCode, string topicCode, bool isReal)
  {
    this.headline = headline;
    this.eventCode = eventCode;
    this.topicCode = topicCode;
    this.isReal = isReal;
  }

  public News toNews(string author, string newspaper, string date, string location, string error)
  {
    return new News(headline, author, newspaper, date, location, !isReal, ressortKeyToLabel[this.topicCode], error);
  }
}

public class NewsSourceForRealEn : NewsSource
{

  List<HeadlineInfoEn> News = new List<HeadlineInfoEn>
  {
    new HeadlineInfoEn(
      "Wehmert to his confidants: \"Gerlinde Wuschel has no competence in leadership!\" ",
      "EVENT_WK",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Irregularities regarding Wuschel nomination",
      "EVENT_WK",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Wehmert surprised by Wuschel's candidacy",
      "EVENT_WK",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Members and followers of the DVGF celebrate the nomination of the first female chancellor candidate for the upcoming federal election",
      "EVENT_WK",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Gerlinde Wuschel declares her chancellor candidacy for the upcoming federal election",
      "EVENT_WK",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Scandalous: In his days as mayor, chancellor candidate Wehmert embezzled tax money to finance his swimming pool",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Social benefits for mayor Wehmert while in office?",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "FPF Whistleblower: Wehmert misappropriated tax money for years",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Mayor Wehmert secretly employed his own wife as secretary",
      "EVENT_VS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Nepotism in the city hall: Mayor Wehmert employed familiy members!",
      "EVENT_VS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Scandal in Wehmert's city hall office: Sexism behind closed doors!",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Frivolous details from Wehmert's former job as mayor: Insider reveals dirty sex practices in office ",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Sex-scandal in the town hall: Humiliated female intern reveals details",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Rehnhold's choleric rant in the House of Representatives: \"Wehmert is sexist, not suitable for the Federal Chancellery and a despicable human being!\"",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "\"Heinous and untrue!\" Wehmert fights vehemently against sexism allegations",
      "EVENT_SS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Rehnhold, chairman of DVGF, calls Wehmert a sexist",
      "EVENT_SS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Backing for Mayor Wehmert in Sex-Shitstorm: Competitor Wuschel criticizes Fake News on the internet",
      "EVENT_SS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Wehmert's scandalous Tweet: \"Keep women away from politics!\"",
      "EVENT_WF",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Violent mob of feminists ravages the city center of the capital!",
      "EVENT_WF",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Gerlinde Wuschel at political rally: \"Down with patriarchy!\"",
      "EVENT_WF",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Thousands march to improve working conditions for mothers",
      "EVENT_WF",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Pesticide Scandal: Did our politicians know about the contamination of our drinking water?",
      "EVENT_US",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "\"Stay away from our eggs!\" Wehmert's health minister concerned about pesticide scandal",
      "EVENT_US",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Wehmert's misappropriation scandal: Industry shares plummet",
      "EVENT_VS",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfoEn(
      "Fertilizer scandal: The dirty trace leads to Wehmert",
      "EVENT_US",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfoEn(
      "Farmers to blame for the pesticide contamination of the groundwater",
      "EVENT_US",
      "FACHGEBIET_Sc",
      false
    ),
    new HeadlineInfoEn(
      "This is how pesticides poison our cows",
      "EVENT_US",
      "FACHGEBIET_Sc",
      false
    ),
    new HeadlineInfoEn(
      "Gerlinde Wuschel elected as Federal Chancellor by majority vote ",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Gerlinde Wuschel wins election as Federal Chancellor",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Gerlinde Wuschel as new Federal Chancellor: \"Everything is going to change!\"",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Polling stations only partially barrier-free: Old and disabled people had to look for alternatives to give their vote",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfoEn(
      "Fraud in federal election: Thousands of illegal immigrants voted for Wuschel",
      "EVENT_WT",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Wuschel election fraud: Wuschel declares herself Federal Chancellor",
      "EVENT_WT",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Wehmert loses Federal Chancellor election despite getting 86% of the votes: Old people weren't allowed to vote!",
      "EVENT_WT",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfoEn(
      "Stock market hardly reacts to environmental scandal",
      "EVENT_US",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfoEn(
      "Latest news on the pesticide scandal: Scientists discover a new way for sustainable pest control",
      "EVENT_US",
      "FACHGEBIET_Sc",
      true
    ),
    new HeadlineInfoEn(
      "Federal Chancellor election: Optimistic future forecast for the financial market",
      "EVENT_WT",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfoEn(
      "Due to recent political events: Association of female CEOs calls for higher wages for women",
      "EVENT_SS",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfoEn(
      "Misappropriation Process: Wehmert Media Company is being held accountable for embezzled tax money",
      "EVENT_VS",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfoEn(
      "Apollo 20 finds evidence for water on the moon",
      "EVENT_ML",
      "FACHGEBIET_Sc",
      false
    ),
    new HeadlineInfoEn(
      "After the moon landing: Well-known manufacturer of soft drinks strives for advertising in space",
      "EVENT_ML",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfoEn(
      "Merger of Grunmeyer and Haalenkamp failed because of environmental scandal",
      "EVENT_US",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfoEn(
      "Stock market shaken by Wuschel's candidacy ",
      "EVENT_WK",
      "FACHGEBIET_Wi",
      true
    )
  };

  int idx = 0;
  int progression = 1;
  Facts facts = new Facts();
  public static List<List<string>> simpleCats = new List<List<string>>{
    new List<string> { "ZEITUNG" , "AUTOR"},
    new List<string> { "ZEITUNG" , "ORT"},
    new List<string> { "ZEITUNG" , "DATE", "TAG"}
  };
  public static List<List<string>> mediumCats = new List<List<string>>{
    new List<string> { "ZEITUNG" , "AUTOR", "ORT"},
    new List<string> { "ZEITUNG" , "ORT", "DATE", "TAG"},
    new List<string> { "ZEITUNG" , "AUTOR", "DATE", "TAG",}
  };

  public static List<List<string>> hardCats = new List<List<string>>{
    new List<string> { "ZEITUNG" , "AUTOR", "ORT", "DATE", "TAG"},
  };

  public NewsSourceForRealEn()
  {
    News.Shuffle();
    facts.Init("Assets/factsEn.txt");
    simpleCats.Shuffle();
    mediumCats.Shuffle();
    hardCats.Shuffle();
  }

  private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
  {
    // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
    int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
    return start.AddDays(daysToAdd);
  }

  private static Dictionary<string, int> monthNameToInt = new Dictionary<string, int>{{"Feb", 2}, {"Mar", 3}};
  private static Dictionary<string, DayOfWeek> weekdayNameToDayOfWeek = new Dictionary<string, DayOfWeek>{
    {"Monday", DayOfWeek.Monday},
    {"Tuesday", DayOfWeek.Tuesday},
    {"Wednesday", DayOfWeek.Wednesday},
    {"Thursday", DayOfWeek.Thursday},
    {"Friday", DayOfWeek.Friday},
    {"Saturday", DayOfWeek.Saturday},
    {"Sunday", DayOfWeek.Sunday},
  };

  private static DateTime GetNextWeekday(string dateAsString, string dayOfWeekAsString)
  {
    // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
    var r = Regex.Match(dateAsString, @"(\w+)\s+(\d+)");
    var date = new DateTime(2018, monthNameToInt[r.Groups[1].Value], Int32.Parse(r.Groups[2].Value));
    return GetNextWeekday(date, weekdayNameToDayOfWeek[dayOfWeekAsString]);

  }

  public News getNextNews()
  {
    Dictionary<string, string> solution = null;
    HeadlineInfoEn info = null;
    while (solution == null)
    {
      info = News[idx];
      idx = (idx + 1) % News.Count;
      List<string> findCats = null;
      if (progression < 4) {
        findCats = simpleCats[progression % simpleCats.Count];
      } else if (progression < 7) {
        findCats = mediumCats[progression % mediumCats.Count];
      } else {
        findCats = hardCats[progression % hardCats.Count];
      }
      var constr = new Dictionary<string, string> { { "EVENT", info.eventCode }, { "FACHGEBIET", info.topicCode } };
      solution = info.isReal ? facts.FindValid(findCats, constr) : facts.FindInvalid(findCats, constr);
      if (solution == null) Console.WriteLine("COULD FIND NO SOLUTION FOR '{0}'", info.headline);
    }
    progression += 1;
    string author = null;
    solution.TryGetValue("AUTOR", out author);
    string newspaper = null;
    solution.TryGetValue("ZEITUNG", out newspaper);
    string date = null;
    solution.TryGetValue("DATE", out date);
    string day = null;
    solution.TryGetValue("TAG", out day);
    string location = null;
    solution.TryGetValue("ORT", out location);
    string error = null;
    solution.TryGetValue("ERROR", out error);
    return info.toNews(author, newspaper, date != null ? GetNextWeekday(date, day).ToString("dd.MM.yyyy") : null, location, error);
  }

}
