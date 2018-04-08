using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

/*
 *  Dummy News Generator for testing
 */
class HeadlineInfo
{
  public string headline;
  public string eventCode;
  public string topicCode;
  public bool isReal;

  public HeadlineInfo(string headline, string eventCode, string topicCode, bool isReal)
  {
    this.headline = headline;
    this.eventCode = eventCode;
    this.topicCode = topicCode;
    this.isReal = isReal;
  }

  public News toNews(string author, string newspaper, string date, string location)
  {
    return new News((isReal ? "T: " : "F: ") + headline, author, newspaper, date, location, !isReal);
  }
}

public class NewsSourceForReal : NewsSource
{

  List<HeadlineInfo> News = new List<HeadlineInfo>
  {
    new HeadlineInfo(
      "Wehmert zu Vertrauten: Gerlinde Wuschel besitzt keine Führungskompetenz.",
      "EVENT_WK",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Unregelmäßigkeiten bei Wuschel-Aufstellung",
      "EVENT_WK",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Wehmert überrascht von Wuschels Kandidatur",
      "EVENT_WK",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Anhänger der DVGF feiern die Aufstellung der ersten weiblichen Kanzlerkandidatur",
      "EVENT_WK",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Gerlinde Wuschel erklärt Kandidatur für das Amt der Bundeskanzlerin.",
      "EVENT_WK",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Skandal: In seiner Zeit als Bürgermeister hat sich Wehmert Steuergelder veruntreut, um Swimmingpool zu bauen.",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Bürgermeister Wehmert erschlich sich Sozialleistungen im Amt.",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Whistleblower bei der FPF: Wehmert hat jahrelang Gelder veruntreut.",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Scheinbeschäftigung? Bürgermeister Wehmert hat eigene Frau als Sekretärin eingestellt.",
      "EVENT_VS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Vetternwirtschaft im Rathaus: Wehmert beschäftigt Familienmitglieder!",
      "EVENT_VS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Bürgermeister Wehmert hat keine weiße Weste: Sexismus hinter verschlossener Tür.",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Schmutzige Sex-Praktiken in Wehmerts Rathaus: Ein Insider packt aus.",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Sexskandal im Rathaus: Gedemütigte Praktikantin packt aus!",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Rehnholds cholerischer Anfall im Bundestag \"Sexist Wehmert als Politiker nicht geeignet und als Mensch nicht tragbar!\"",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "“Das stimmt so nicht!” Wehmert vehement gegen Sexismusvorwürfe.",
      "EVENT_SS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "DVGF-Vorsitzender Rehnold bezeichnet Wehmert als Sexist",
      "EVENT_SS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Rückendeckung für Bürgermeister Wehmert im Sex-Sturm: Konkurrentin Wuschel gegen Fake News im Netz",
      "EVENT_SS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Wehmerts Skandal Tweet: Frauen raus aus der Politik!",
      "EVENT_WF",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Mob aggressiver Feministinnen verwüsten Innenstadt",
      "EVENT_WF",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Gerlinde Wuschel auf Kundgebung: \"Nieder mit dem Patriarchat!\"",
      "EVENT_WF",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Demonstration zur Verbesserung der Arbeitsbedingungen für Mütter",
      "EVENT_WF",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Pestizid-Skandal: Wussten Politiker von der Verunreinigung unseres Trinkwassers?",
      "EVENT_US",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "\"Finger weg von unseren Eiern!\" Wehmerts Gesundheitsminister in Bedrängnis",
      "EVENT_US",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Nach Wehmert-Veruntreuung: Industrie A47 Aktie stürzt ein",
      "EVENT_VS",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfo(
      "Die dreckige Spur des Düngegeldes",
      "EVENT_US",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfo(
      "Bauern schuld an Pestizidvergiftung durch Düngemittel",
      "EVENT_US",
      "FACHGEBIET_Sc",
      false
    ),
    new HeadlineInfo(
      "So vergiften Pestizide unsere Kühe",
      "EVENT_US",
      "FACHGEBIET_Sc",
      false
    ),
    new HeadlineInfo(
      "Gerlinde Wuschel mit Mehrheit der Stimmen zur Bundeskanzlerin gewählt.",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Gerlinde Wuschel gewinnt Wahl zur Bundeskanzlerin.",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Gerlinde Wuschel neue Bundeskanzlerin: \"Alles wird anders!\"",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Wahllokale nur bedingt barrierefrei: Rentner mussten Alternativen finden.",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Betrug bei der Bürgermeisterwahl: Illegale Immigranten gaben Stimmen für Wuschel ab.",
      "EVENT_WT",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Unstimmigkeiten bei der Wahl: Wuschel erklärt sich selbst zur Bürgermeisterin.",
      "EVENT_WT",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Wehmert verliert Bundeskanzlerwahl trotz 86%: Rentner durften nicht wählen!",
      "EVENT_WT",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Börse reagiert verhalten auf Umweltskandal.",
      "EVENT_US",
      "FACHGEBIET_Sc",
      true
    ),
    new HeadlineInfo(
      "Nach Pestizidskandal: Wissenschaft findet neuen Weg zur nachhaltigen Schädlingsbekämpfung",
      "EVENT_US",
      "FACHGEBIET_Sc",
      true
    ),
    new HeadlineInfo(
      "Bundeskanzlerwahl: Finanzwelt mit optimistischer Zukunftsprognose.",
      "EVENT_WT",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfo(
      "Aus aktuellem Anlass: Bund weiblicher Vorstände fordert höhere Löhne für Frauen",
      "EVENT_SS",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfo(
      "Eilverfahren: Wehmert Medienunternehmen muss für veruntreutes Geld haften!",
      "EVENT_VS",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfo(
      "Apollo 20 findet Hinweise auf Wasser auf dem Mond",
      "EVENT_ML",
      "FACHGEBIET_Sc",
      false
    ),
    new HeadlineInfo(
      "Coca Cola will nach Mondladung Werbung im All prüfen",
      "EVENT_ML",
      "FACHGEBIET_Sc",
      true
    ),
    new HeadlineInfo(
      "Fusion von Grunmeyer und Haalenkamp nach Umweltskandal gescheitert",
      "EVENT_US",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfo(
      "Aktienmarkt geschockt von Wuschels Kandidatur",
      "EVENT_WK",
      "FACHGEBIET_Wi",
      true
    )
  };

  int idx = 0;
  Facts facts = new Facts();

  public NewsSourceForReal()
  {
    News.Shuffle();
    facts.Init("Assets/facts.txt");
  }

  private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
  {
    // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
    int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
    return start.AddDays(daysToAdd);
  }

  private static Dictionary<string, int> monthNameToInt = new Dictionary<string, int>{{"Feb", 2}, {"Mar", 3}};
  private static Dictionary<string, DayOfWeek> weekdayNameToDayOfWeek = new Dictionary<string, DayOfWeek>{
    {"Montag", DayOfWeek.Monday},
    {"Dienstag", DayOfWeek.Tuesday},
    {"Mittwoch", DayOfWeek.Wednesday},
    {"Donnerstag", DayOfWeek.Thursday},
    {"Freitag", DayOfWeek.Friday},
    {"Samstag", DayOfWeek.Saturday},
    {"Sonntag", DayOfWeek.Sunday},
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
    HeadlineInfo info = null;
    while (solution == null)
    {
      info = News[idx];
      idx = (idx + 1) % News.Count;
      var findCats = new List<string> { "ZEITUNG", "AUTOR", "ORT", "REGION", "DATE", "TAG" };
      var constr = new Dictionary<string, string> { { "EVENT", info.eventCode }, { "FACHGEBIET", info.topicCode } };
      solution = info.isReal ? facts.FindValid(findCats, constr) : facts.FindInvalid(findCats, constr);
      if (solution == null) Console.WriteLine("COULD FIND NO SOLUTION FOR '{0}'", info.headline);
    }
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
    return info.toNews(author, newspaper, GetNextWeekday(date, day).ToString("dd.MM.yyyy"), location);
  }

}
