using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class ModularStringLoader : ModularLanguageLoader {

    Text textToChange;

    // Use this for initialization
    protected override void Start ()
    {
        textToChange = gameObject.GetComponent<Text>();
        base.Start();
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
