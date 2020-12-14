using AsserTOOLres;

public class Data : Singleton<Data>
{
	public Observable<language> myLanguage;
	public ISDK mySDK;
	public ILocalisator myLocalisator;

	private void Start()
	{

		myLanguage.value = mySDK.GetCurrentLanguage();
	}

	private void FixedUpdate()
	{
		mySDK.UpdateSDK();
	}
}
