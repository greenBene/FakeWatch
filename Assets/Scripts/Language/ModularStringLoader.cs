using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class ModularStringLoader : ModularLanguageLoader {

    Text textToChange;

    // Use this for initialization
    void Start ()
    {
        textToChange = gameObject.GetComponent<Text>();
        base.Renew();
	}

    protected override string GetElementName()
    {
        return "text";
    }

    protected override void ExecuteChange(string newValue)
    {
        textToChange.text = newValue;
    }
}
