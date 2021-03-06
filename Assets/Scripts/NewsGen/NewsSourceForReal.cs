﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

class HeadlineInfo
{
    public string headline;
    public string eventCode;
    public string topicCode;
    public bool isReal;

    private static Dictionary<string, string> ressortKeyToLabel = new Dictionary<string, string>{
    {"FACHGEBIET_Po", "Politik"},
    {"FACHGEBIET_Wi", "Wirtschaft"},
    {"FACHGEBIET_Sc", "Wissenschaft"},
  };

    public HeadlineInfo(string headline, string eventCode, string topicCode, bool isReal)
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

public class NewsSourceForReal : NewsSource
{
    private Random rng = new Random();

    List<HeadlineInfo> News = new List<HeadlineInfo>
  {
    new HeadlineInfo(
      "Wehmert zu Vertrauten: Kandidierende Wuschel besitzt keine Führungskompetenz",
      "EVENT_WK",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Unregelmäßigkeiten bei Aufstellung der Kandidatin Wuschel",
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
      "Anhänger der DVGF feiern die Aufstellung der ersten weiblichen Kanzlerkandidatin",
      "EVENT_WK",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Gerlinde Wuschel erklärt Kandidatur für das Amt der Bundeskanzlerin",
      "EVENT_WK",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Skandal: In seiner Zeit als Bürgermeister veruntreute Wehmert Steuergelder, um Swimmingpool zu bauen!",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Bürgermeister Wehmert erschlich sich Sozialleistungen im Amt",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Whistleblower bei der FPF: Wehmert hat jahrelang Gelder veruntreut",
      "EVENT_VS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Scheinbeschäftigung? Bürgermeister Wehmert stellte eigene Frau als Sekretärin ein",
      "EVENT_VS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Vetternwirtschaft im Rathaus? Wehmert beschäftigte Familienmitglieder!",
      "EVENT_VS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Bürgermeister Wehmert hat keine weiße Weste: Sexismus hinter verschlossener Tür!",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Schmutzige Sex-Praktiken in Wehmerts Rathaus: Ein Insider packt aus",
      "EVENT_SS",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Sex-Skandal im Rathaus: Gedemütigte Praktikantin packt aus!",
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
      "\"Das stimmt so nicht!\" Wehmert vehement gegen Sexismusvorwürfe",
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
      "Rückendeckung für Bürgermeister Wehmert in Sex-Shitstorm: Wuschel kritisiert Fake News Verbreitung",
      "EVENT_SS",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Wehmerts Skandal-Tweet vor Frauenrechts-Demo: Frauen raus aus der Politik!",
      "EVENT_WF",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Mob aggressiver Feministinnen verwüstet Innenstadt",
      "EVENT_WF",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Gerlinde Wuschel auf Frauenrechts-Kundgebung: \"Nieder mit dem Patriarchat!\"",
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
      "\"Finger weg von unseren Eiern!\" Gesundheitsminister wegen Umweltskandal in Bedrängnis",
      "EVENT_US",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Wehmerts Veruntreuungs-Skandal: Industrie Aktie A47 stürzt ein",
      "EVENT_VS",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfo(
      "Die dreckige Spur des Düngergeldes führt in höchste politische Ebene",
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
      "Gerlinde Wuschel mit Mehrheit der Stimmen zur Bundeskanzlerin gewählt",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Gerlinde Wuschel gewinnt Wahl zur Bundeskanzlerin",
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
      "Wahllokale nur bedingt barrierefrei: Rentner mussten Alternativen finden",
      "EVENT_WT",
      "FACHGEBIET_Po",
      true
    ),
    new HeadlineInfo(
      "Betrug bei der Bürgermeisterwahl: Illegale Immigranten gaben Stimmen für Wuschel ab",
      "EVENT_WT",
      "FACHGEBIET_Po",
      false
    ),
    new HeadlineInfo(
      "Unstimmigkeiten bei Wahl: Wuschel erklärt sich selbst zur Bundeskanzlerin!!!",
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
      "Börse reagiert verhalten auf Umweltskandal",
      "EVENT_US",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfo(
      "Pestizidskandal: Wissenschaft sucht neuen Weg zur nachhaltigen Schädlingsbekämpfung",
      "EVENT_US",
      "FACHGEBIET_Sc",
      true
    ),
    new HeadlineInfo(
      "Bundeskanzlerwahl: Finanzwelt mit optimistischer Zukunftsprognose",
      "EVENT_WT",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfo(
      "Aus aktuellem Anlass: Bund weiblicher Vorstände fordert bessere Löhne für Frauen",
      "EVENT_WF",
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
      "Fips Cola will nach Mondladung Möglichkeit für Werbung im All prüfen",
      "EVENT_ML",
      "FACHGEBIET_Wi",
      true
    ),
    new HeadlineInfo(
      "Fusion von Grunmeyer und Haalenkamp wegen Umweltskandal gescheitert",
      "EVENT_US",
      "FACHGEBIET_Wi",
      false
    ),
    new HeadlineInfo(
      "Aktienmarkt reagiert erschüttert auf Wuschels Kandidatur",
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

    public NewsSourceForReal()
    {
        News.Shuffle();
        facts.Init("Assets/News/factsDE.txt");
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

    private static Dictionary<string, int> monthNameToInt = new Dictionary<string, int> { { "Feb", 2 }, { "Mar", 3 } };
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

    //This is for compatibility with Legacy code only. Avoid using!
    public News getNextNews()
    {
        News news;
        if (progression < 4)
        {
            news = GetNextNews(0);
        }
        else if (progression < 7)
        {
            news = GetNextNews(1);
        }
        else
        {
            news = GetNextNews(2);
        }
        return news;
    }

    public News GetNextNews(int complexity)
    {
        Dictionary<string, string> solution = null;
        HeadlineInfo info = null;
        while (solution == null)
        {
            info = News[idx];
            idx = (idx + 1) % News.Count;
            List<string> findCats = null;
            switch (complexity)
            {
                case 0:
                    {
                        findCats = simpleCats[rng.Next() % simpleCats.Count];
                    }
                    break;
                case 1:
                    {
                        findCats = mediumCats[rng.Next() % mediumCats.Count];
                    }
                    break;
                case 2:
                    {
                        findCats = hardCats[rng.Next() % hardCats.Count];
                    }
                    break;
                default:
                    {
                        findCats = hardCats[rng.Next() % hardCats.Count];
                    } break;
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
