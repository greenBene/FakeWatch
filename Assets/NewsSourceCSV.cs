using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NewsSourceCSV : MonoBehaviour, NewsSource {

    ArrayList newsArray;
   
	// Use this for initialization
	void Start () {
        newsArray = new ArrayList();
        StreamReader sr = new StreamReader(Application.dataPath + "/testData.csv");
        string line = sr.ReadLine();

        while ((line = sr.ReadLine()) != null)
        {
            string[] strArr = line.Split(",".ToCharArray());
            News news = arrayToNews(strArr);

            newsArray.Add(news);
        }

	}

    News arrayToNews(string[] strArray){
        string headline = strArray[0];
        string newspaper = strArray[1];
        string author = strArray[2];
        string location = strArray[3];
        string date = strArray[4];
        bool isFake = (strArray[5].ToUpper() == "TRUE"? true: false);

        return new News(headline, newspaper, author, location, date, isFake);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static NewsSource getInstance(GameObject go){
        return (NewsSource) (go.AddComponent(typeof(NewsSourceCSV)) as NewsSource) ;
    }

    public News getNextNews(){
        News news = (News) newsArray[Random.Range(0, newsArray.Capacity-1)];
        return news;
    }
}
