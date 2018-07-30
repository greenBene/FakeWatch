using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NewsSourceCSV : MonoBehaviour, NewsSource {

    ArrayList newsArray;
    

	void Start () {
        newsArray = new ArrayList(); //erzeugt array mit unbestimmter größe
        StreamReader sr = new StreamReader(Application.dataPath + "/NewsGen/testData.csv");//öffnet datei
        string line = sr.ReadLine(); //liest erste zeile aus

        while ((line = sr.ReadLine()) != null)//liest zeilen aus solange es zeilen giebt
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

        return new News(headline, newspaper, author, location, date, isFake, "ressort", null);
    }

    public static NewsSource getInstance(GameObject go){
        return (NewsSource) (go.AddComponent(typeof(NewsSourceCSV)) as NewsSource) ;
    }

    public News getNextNews(){
        News news = (News) newsArray[Random.Range(0, newsArray.Capacity-1)]; //holt sich ein zufälliges element aus dem array und giebt es zurück
        return news;
    }

    public News GetNextNews(int complexity) {
        throw new System.NotImplementedException();
    }
}
