using System.IO;
using System.Collections.Generic;
using System;
using System.Xml;

public class ErrorMessageGenerator
{
    private UnorderedTuple<InfoType> currentTypes;
    private Dictionary<UnorderedTuple<InfoType>,List<string>> messages = new Dictionary<UnorderedTuple<InfoType>, List<string>>();
    private Dictionary<UnorderedTuple<InfoType>, OrderedTuple<InfoType>> argumentOrder = new Dictionary<UnorderedTuple<InfoType>, OrderedTuple<InfoType>>();
    private Random rng = new Random();
    
    public ErrorMessageGenerator()
    {
        var root = GameManager.XMLLoader.GetRoot(xmlFiles.errors);
        XmlNodeList nodes = root.SelectNodes("//error");
        foreach(XmlNode node in nodes)
        {
            string p1 = node.Attributes["info1"].Value;
            string p2 = node.Attributes["info2"].Value;
            InfoType t1 = Info.ParseTypeString(p1);
            InfoType t2 = Info.ParseTypeString(p2);

            UnorderedTuple<InfoType> types = new UnorderedTuple<InfoType>(t1, t2);
            OrderedTuple<InfoType> order = new OrderedTuple<InfoType>(t1, t2);
            if(!messages.ContainsKey(types))
            {
                messages.Add(types, new List<string>());
            }
            if(!argumentOrder.ContainsKey(types))
            {
                argumentOrder.Add(types, order);
            }
            bool switchPlaceholders = false;
            if(argumentOrder[types]!=order)
            {
                switchPlaceholders = true;
            }

            XmlNodeList textnodes = node.SelectNodes("./message");
            foreach(XmlNode textnode in textnodes)
            {
                string text = textnode.InnerText;
                if (switchPlaceholders)
                {
                    text = text.Replace("{0}", "{N}");
                    text = text.Replace("{1}", "{0}");
                    text = text.Replace("{N}", "{1}");
                }
                messages[types].Add(text);
            }
        }
    }

    //===== ===== OLD IMPLEMENTATION ===== =====//
    public ErrorMessageGenerator(string filename)
    {
        if (!File.Exists(filename))
        {
            throw new Exception("File not found!");
        }

        using (StreamReader sr = new StreamReader(filename,System.Text.Encoding.UTF8))
        {
            while (sr.Peek() >= 0)
            {
                HandleLine(sr.ReadLine());
            }
        }
    }

    private void HandleLine(string line)
    {
        if(line.Length==0) // empty line
        {
            return;
        }
        else if(line=="[KORREKT]") // message was correct error type
        {
            currentTypes = new UnorderedTuple<InfoType>(InfoType.none, InfoType.none);
            argumentOrder.Add(currentTypes, new OrderedTuple<InfoType>(InfoType.none, InfoType.none));
        }
        else if(line[0]=='[') // other new error type
        {
            string[] typeStrings = line.Trim('[', ']').Split('|');
            if(typeStrings.Length != 2)
            {
                throw new Exception("Invalid type identifier " + line);
            }
            InfoType t1 = Info.ParseTypeString(typeStrings[0]);
            InfoType t2 = Info.ParseTypeString(typeStrings[1]);
            currentTypes = new UnorderedTuple<InfoType>(t1, t2);
            argumentOrder.Add(currentTypes, new OrderedTuple<InfoType>(t1, t2));
        }
        else // message
        {
            if(currentTypes == null)
            {
                throw new Exception("file does not start with type identifier!");
            }
            if(!messages.ContainsKey(currentTypes))
            {
                messages.Add(currentTypes, new List<string>());
            }
            messages[currentTypes].Add(line);
        }
    }

    public string GetRawMessage(UnorderedTuple<InfoType> types)
    {
        try
        {
            int index = rng.Next(messages[types].Count - 1);
            return messages[types][index];
        }
        catch
        {
            return String.Format("No messages found for InfoTypes {0} and {1}", types.item1, types.item2);
        }
    }

    public string GetMessage(Inconsistency inconsistency)
    {
        UnorderedTuple<InfoType> types = inconsistency.GetTypes();
        string raw = GetRawMessage(types);

        try
        {
            if (argumentOrder[types].item1 == inconsistency.info1.type)
            {
                raw = raw.Replace("{0}", inconsistency.info1.value);
                raw = raw.Replace("{1}", inconsistency.info2.value);
            }
            else
            {
                raw = raw.Replace("{0}", inconsistency.info2.value);
                raw = raw.Replace("{1}", inconsistency.info1.value);
            }
        }
        catch
        {
            return String.Format("No order found for InfoTypes {0} and {1}", types.item1, types.item2);
        }
        return raw;
    }
}