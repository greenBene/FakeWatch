using AsserTOOLres;

namespace AbteilungF
{
	public class Data : Singleton<Data>
	{
		public Observable<language> myLanguage;
		public Observable<int> myCorrect;
		public Observable<int> myFalsePositive;
		public Observable<int> myFalseNegative;

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
}
