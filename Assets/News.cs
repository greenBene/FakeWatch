[System.Serializable]
public class News
{
    public News(string headline,
                string author,
                string newspaper,
                string date,
                string location,
                bool isFake,
                string ressort){

        this.headline = headline;
        this.author = author;
        this.newspaper = newspaper;
        this.date = date;
        this.location = location;
        this.isFake = isFake;
        this.ressort = ressort;
    }

    public News() : this("NOT FAKE", "GA", "bild", "12.10.2070", "Hamburg", true, "Politik"){

    }

    public string headline;
    public string author;
    public string newspaper;
    public string date;
    public string location;
    public bool isFake;
    public string ressort;

    override public string ToString(){
        string ret = "NEWS: \n";

        ret += "headline: " + headline + ", ";
        ret += "author: " + author + ", ";
        ret += "newspaper: " + newspaper + ", ";
        ret += "date: " + date + ", ";
        ret += "location: " + location + ", ";
        ret += "isFake: " + isFake;
        ret += "ressort: " + ressort;

        return ret;
    }

}
