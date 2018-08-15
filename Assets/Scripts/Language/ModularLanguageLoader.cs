using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public abstract class ModularLanguageLoader : MonoBehaviour {

    [SerializeField] string attrcategory;
    [SerializeField] string attrname;
    private string attrelement;

    // Use this for initialization
    protected virtual void Start ()
    {
        attrelement = GetElementName();
        Renew();
	}

    public void Renew()
    {
        var root = GameManager.XMLLoader.GetRoot(xmlFiles.scene);
        string xmlValue = getValue(root);
        ExecuteChange(xmlValue);
    }

    private string buildSearchString()
    {
        Debug.Log("xpath: " + string.Format("{0}[@name='{1}' and @category='{2}']", attrelement, attrname, attrcategory));
        return string.Format("{0}[@name='{1}' and @category='{2}']", attrelement, attrname, attrcategory);
    }

    private string getValue(XmlNode root)
    {
        XmlNodeList nodes = root.SelectNodes(buildSearchString());
        int numElements = nodes.Count;
        if (numElements == 0)
        {
            return string.Format("No {0}-element found with attributes name={1} and category={2}", attrelement, attrname, attrcategory);
        }
        else if (numElements > 1)
        {
            return string.Format("Multiple {0}-elements found with attributes name={1} and category={2}", attrelement.ToString(), attrname, attrcategory);
        }
        else return nodes[0].InnerText;
    }

    protected abstract string GetElementName();
    protected abstract void ExecuteChange(string newValue);
}
