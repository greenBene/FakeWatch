using AsserTOOLres;

public class Data : Singleton<Data>
{
	public Observable<language> myLanguage;
	public ISDK mySDK;
	public ILocalisator myLocalisator;
}
