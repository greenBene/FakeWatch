public class Info
{
    public InfoType type { get; private set; }
    public string value { get; private set; }

    public Info(InfoType type, string value)
    {
        this.type = type;
        this.value = value;
    }

    public Info(string type, string value)
    {
        switch(type)
        {
            case "TAG":
                this.type = InfoType.date;
                break;
            case "ZEITUNG":
                this.type = InfoType.paper;
                break;
            case "AUTOR":
                this.type = InfoType.author;
                break;
            case "FACHGEBIET":
                this.type = InfoType.ressort;
                break;
            case "ORT":
                this.type = InfoType.place;
                break;
            default:
                this.type = InfoType.headline;
                break;
        }
        this.value = value;
    }
}
