using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public enum xmlFiles { scene, errors};

public class XMLLoader{

    private Dictionary<xmlFiles, XmlDocument> xmlDict;
    private Dictionary<xmlFiles, XmlNode> nodesDict;
    private string language;

    public XMLLoader()
    {
        string fileScenes = "Assets/Language/Scene.xml";
        string fileErrors = "Assets/Language/Errors.xml";

        xmlDict = new Dictionary<xmlFiles, XmlDocument>();

        XmlDocument scenes = new XmlDocument();
        scenes.Load(fileScenes);
        xmlDict.Add(xmlFiles.scene, scenes);

        XmlDocument errors = new XmlDocument();
        errors.Load(fileErrors);
        xmlDict.Add(xmlFiles.errors, errors);

        UpdateLanguage(false);
    }

    public XmlNode GetRoot(xmlFiles type)
    {
        return nodesDict[type];
    }

    public string GetValue(xmlFiles type, string attrelement, string[] key, string[] value) {
        if(key.Length != value.Length) {
            new System.Exception("Key value pairs not assignable");
        }

        string SearchString = attrelement + "[";
        for(int i = 0; i < key.Length-1; i++) {
            SearchString += "@" + key[i] + "='" + value[i] + "' and ";
        }
        SearchString += "@" + key[key.Length-1] + "='" + value[value.Length-1] + "']";

        Debug.Log("SerchString is: " + SearchString);

        XmlNodeList nodes = nodesDict[type].SelectNodes(SearchString);
        int numElements = nodes.Count;
        if (numElements == 0) {
            return string.Format("No {0}-element found in {1}", attrelement, SearchString);
        } else if (numElements > 1) {
            return string.Format("Multiple {0}-elements found in {1}", attrelement, SearchString);
        } else {
            return nodes[0].InnerText;
        }
    }

    public void UpdateLanguage(bool updateScene)
    {
        nodesDict = new Dictionary<xmlFiles, XmlNode>();
        language = PlayerPrefs.GetString("language");
        foreach (xmlFiles file in xmlDict.Keys)
        {
            XmlNodeList nodes = xmlDict[file].SelectNodes(string.Format("//collection[@language='{0}']", language));
            int numElements = nodes.Count;
            if (numElements == 0)
            {
                throw new System.Exception("No collection-element found with language=" + language + " - selection: " + string.Format("//collection[@language='{0}']", language));
            }
            else if (numElements > 1)
            {
                throw new System.Exception("Multiple collection-elements found with language=" + language);
            }
            else
            {
                nodesDict.Add(file, nodes[0]);
            }
        }

        if(updateScene)
        {
            ModularLanguageLoader[] sceneObjects = GameManager.Instance.gameObject.GetComponentsInChildren<ModularLanguageLoader>();
            foreach(ModularLanguageLoader obj in sceneObjects)
            {
                obj.Renew();
            }
        }
    }
}
