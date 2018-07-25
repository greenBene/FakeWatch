public class Inconsistency
{
    public readonly Info info1, info2;
    public readonly bool exists;

    public Inconsistency(Info info1, Info info2)
    {
        exists = true;
        this.info1 = info1;
        this.info2 = info2;
    }

    public Inconsistency(bool exists)
    {
        if (!exists)
        {
            this.exists = false;
            info1 = new Info(InfoType.none, "");
            info2 = new Info(InfoType.none, "");
        }
        else
        {
            throw new System.Exception("To create an existing Inconsistency, use Inconsistency(Info info1, Info info2)!");
        }
    }

    public Inconsistency(string error)
    {
        string[] fields = error.Split('|');
        if(fields.Length != 4)
        {
            throw new System.Exception("Not a valid error message when trying to create Inconsistency!");
        }
        info1 = new Info(fields[0], fields[1]);
        info2 = new Info(fields[2], fields[3]);
        exists = true;
    }

    public UnorderedTuple<InfoType> GetTypes()
    {
        if (exists)
            return new UnorderedTuple<InfoType>(info1.type, info2.type);
        else
            return new UnorderedTuple<InfoType>(InfoType.none, InfoType.none);
    }


}
