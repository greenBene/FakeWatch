using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public enum xmlFiles { scene};

public class XMLLoader{

    private Dictionary<xmlFiles, XmlDocument> xmlDict;
    private Dictionary<xmlFiles, XmlNode> nodesDict;
    private string language;

    public XMLLoader()
    {
        xmlDict = new Dictionary<xmlFiles, XmlDocument>();

        XmlDocument scenes = new XmlDocument();
        scenes.Load("Language/Scene.xml");
        xmlDict.Add(xmlFiles.scene, scenes);

        UpdateLanguage(false);
    }

    public XmlNode GetRoot(xmlFiles type)
    {
        return nodesDict[type];
    }

    public void UpdateLanguage(bool updateScene)
    {
        nodesDict = new Dictionary<xmlFiles, XmlNode>();
        language = PlayerPrefs.GetString("language");
        foreach (xmlFiles file in xmlDict.Keys)
        {
            XmlNodeList nodes = xmlDict[file].SelectNodes(string.Format("//collection[@language='{0}'", language));
            int numElements = nodes.Count;
            if (numElements == 0)
            {
                throw new System.Exception("No collection-element found with language=" + language);
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
