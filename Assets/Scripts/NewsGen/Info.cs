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
        this.type = ParseTypeString(type);
        this.value = value;
    }

    public static InfoType ParseTypeString(string type)
    {
        InfoType infoType;
        switch (type)
        {
            case "TAG":
                infoType = InfoType.date;
                break;
            case "ZEITUNG":
                infoType = InfoType.paper;
                break;
            case "AUTOR":
                infoType = InfoType.author;
                break;
            case "FACHGEBIET":
                infoType = InfoType.ressort;
                break;
            case "ORT":
                infoType = InfoType.place;
                break;
            case "EVENT":
                infoType = InfoType.headline;
                break;
            default:
                infoType = InfoType.none;
                break;
        }
        return infoType;
    }
}
