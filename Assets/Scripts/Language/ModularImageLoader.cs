using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class ModularImageLoader : ModularLanguageLoader {

    Image ImageToChange;

    // Use this for initialization
    protected override void Start () {
        ImageToChange = GetComponent<Image>();
        base.Start();
	}

    protected override string GetElementName()
    {
        return "path";
    }

    protected override void ExecuteChange(string newValue)
    {
        string path = PlayerPrefs.GetString("language") + "/";
        Sprite newSprite;
        newSprite = Resources.Load<Sprite>(path + newValue);
        Debug.Log("Loaded sprite at " + path + newValue);
        if (newSprite == null) {
            Debug.Log("Sprite is null");
        } else {
            ImageToChange.sprite = newSprite;
        }
    }
}
