
/* 
 *  Dummy News Generator for testing
 */

public class NewsSourceDummy : NewsSource {
    
    public News getNextNews(){
        return new News();
    }

}
