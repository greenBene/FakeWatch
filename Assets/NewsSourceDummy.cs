using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Dummy News Generator for testing
 */

public class NewsSourceDummy : NewsSource {

    private string[] headlines = { "No Pun", "It's him!", "No more!", "Staph" };
    private string[] author = { "Bene", "Andreas", "Christoph", "Daniel ", "Felix", "Jörg", "Sven"};
    private string[] newspaper = { "Bild", "FAZ", "SZ", "Taz" };
    private string[] date = { "01.01.1900", "02.02.2018", "07.12.2017", "03.04.2015"};
    private string[] location = { "Hamburg", "New York", "Berlin", "München", "Köln",  };

    public News getNextNews(){
        return new News(headlines[Random.Range(0, headlines.Length - 1)],
                        author[Random.Range(0, author.Length - 1)],
                        newspaper[Random.Range(0, newspaper.Length - 1)],
                        date[Random.Range(0, date.Length - 1)],
                        location[Random.Range(0, location.Length - 1)],
                        (Random.Range(0, 1) == 0 ? true: false),
                        "Politik");
    }

}
