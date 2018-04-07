[System.Serializable]
public class News
{
    public News(string headline, 
                string author, 
                string newspaper, 
                string date,
                string location, 
                bool isFake){
        
        this.headline = headline;
        this.author = author;
        this.newspaper = newspaper;
        this.date = date;
        this.location = location;
        this.isFake = isFake;
    }

    public News() : this("NOT FAKE", "GA", "bild", "12.10.2070", "Hamburg", true){
        
    }

    public string headline;
    public string author;
    public string newspaper;
    public string date;
    public string location;
    public bool isFake;

    override public string ToString(){
        string ret = "NEWS: \n";

        ret += "headline: " + headline + ", ";
        ret += "author: " + author + ", ";
        ret += "newspaper: " + newspaper + ", ";
        ret += "date: " + date + ", ";
        ret += "location: " + location + ", ";
        ret += "isFake: " + isFake;

        return ret;
    }

}