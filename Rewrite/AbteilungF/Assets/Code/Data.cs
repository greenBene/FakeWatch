using AsserTOOLres;

namespace AbteilungF
{
	public class Data : Singleton<Data>
	{
		public Observable<language> myLanguage = new Observable<language>();
		public Observable<int> myCorrect = new Observable<int>();
		public Observable<int> myFalsePositive = new Observable<int>();
		public Observable<int> myFalseNegative = new Observable<int>();

		public ISDK mySDK;
		public ILocalisator myLocalisator;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
			myLanguage.value = mySDK.GetCurrentLanguage();
		}

		private void FixedUpdate()
		{
			if (mySDK != null) {
				mySDK.UpdateSDK();
			}
		}
	}
}
