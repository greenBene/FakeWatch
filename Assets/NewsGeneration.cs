using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsGeneration : MonoBehaviour {

    public GameObject articlePrefab;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Generate(string headline, string zeitung, string journalist, string ort, string datum, bool fake)
    {
        GameObject newArticle = Instantiate(articlePrefab, transform);
        newArticle.GetComponent<Article>().Assign(headline, zeitung, journalist, ort, datum, fake);
    }
}
