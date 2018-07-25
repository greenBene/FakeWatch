using System.Collections.Generic;

public enum InfoType
{
    headline,
    paper,
    author,
    date,
    place,
    ressort,
    none
}

[System.Serializable]
public class News {

    private Dictionary<InfoType, Info> info;
    public Inconsistency conflict { get; private set; }

    public string error;

/*
    //konstruktor
    //in: headline, author, zeitung, datum und ort als string.
    //wahrheitswert ob news fake ist oder nicht.
    //rossort (politik, wissenschaft und so)
    //error string noch nicht herausgefunden
    public News(string headline,
                string author,
                string newspaper,
                string date,
                string location,
                string ressort,
                Inconsistency conflict){

        info.Add(InfoType.headline, new Info(InfoType.headline, headline));
        info.Add(InfoType.author, new Info(InfoType.author, author));
        info.Add(InfoType.paper, new Info(InfoType.paper, newspaper));
        info.Add(InfoType.date, new Info(InfoType.date, date));
        info.Add(InfoType.place, new Info(InfoType.place, location));
        info.Add(InfoType.ressort, new Info(InfoType.ressort, ressort));
        this.conflict = conflict;
    }*/

    public News(string headline,
                string author,
                string newspaper,
                string date,
                string location,
                bool isFake,
                string ressort,
                string error)
    {
        info = new Dictionary<InfoType, Info>();
        info.Add(InfoType.headline, new Info(InfoType.headline, headline));
        info.Add(InfoType.author, new Info(InfoType.author, author));
        info.Add(InfoType.paper, new Info(InfoType.paper, newspaper));
        info.Add(InfoType.date, new Info(InfoType.date, date));
        info.Add(InfoType.place, new Info(InfoType.place, location));
        info.Add(InfoType.ressort, new Info(InfoType.ressort, ressort));

        if(isFake)
        {
            conflict = new Inconsistency(error);
        }
        else
        {
            conflict = new Inconsistency(false);
        }
    }

    public News(Info[] infos, Inconsistency conflict)
    {
        if(infos.Length != 6)
        {
            throw new System.Exception("infos must contain exactly 6 elements!");
        }
        info = new Dictionary<InfoType, Info>();
        foreach (Info info in infos)
        {
            this.info.Add(info.type, info);
        }
        this.conflict = conflict;
    }

    /*//überladener konstruktor
    public News() : this("NOT FAKE", "GA", "bild", "12.10.2070", "Hamburg", true, "Politik", null){

    }*/

    //to string funktion überschireben
    override public string ToString(){
        string ret = "NEWS: \n";

        ret += "headline: " + info[InfoType.headline].value + ", ";
        ret += "author: " + info[InfoType.author].value + ", ";
        ret += "newspaper: " + info[InfoType.paper].value + ", ";
        ret += "date: " + info[InfoType.date].value + ", ";
        ret += "location: " + info[InfoType.place].value + ", ";
        ret += "isFake: " + !conflict.exists + ", ";
        ret += "ressort: " + info[InfoType.ressort].value;

        return ret;
    }

    public string GetInfo(InfoType type)
    {
        return info[type].value;
    }

    public bool IsFake()
    {
        return conflict.exists;
    }

}
