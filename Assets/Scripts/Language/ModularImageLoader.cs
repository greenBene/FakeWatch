using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class ModularImageLoader : ModularLanguageLoader {

    Image ImageToChange;

    // Use this for initialization
    void Start () {
        ImageToChange = GetComponent<Image>();
        Renew();
	}

    protected override string GetElementName()
    {
        return "path";
    }

    protected override void ExecuteChange(string newValue)
    {
        Sprite newSprite;
        try
        {
            newSprite = Resources.Load(newValue) as Sprite;
        }
        catch
        {
            throw new System.Exception("Problem changing Image" + ImageToChange.name);
        }
        ImageToChange.sprite = newSprite;
    }
}
