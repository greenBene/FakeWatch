namespace AbteilungF
{
	public static class StringCollecton
	{
		public const string LOCKSCREEN = "LockScreen";
		public const string INGAME = "InGame";

		public const string FAKE = "Fake";
		public const string CORRECT = "Correct";

		public const string NO_CONNECTION = "None|None";

		public static string KeyFromNewsElement(newsElement aElement)
		{
			switch (aElement) {
			case newsElement.title:
				return "Title";
			case newsElement.autor:
				return "Autor";
			case newsElement.newspaper:
				return "Newspaper";
			case newsElement.place:
				return "Place";
			case newsElement.date:
			case newsElement.day:
				return "Date";
			case newsElement.areaOfExpertise:
				return "AOE";
			default:
				return "";
			}
		}

		public static string KeyFromConnection(newsElement aLhs, newsElement aRhs)
		{
			string key;
			if (IsConnection(aLhs, aRhs, newsElement.date, newsElement.title, out key)) {
				return key;
			}
			if (IsConnection(aLhs, aRhs, newsElement.autor, newsElement.areaOfExpertise, out key)) {
				return key;
			}
			if (IsConnection(aLhs, aRhs, newsElement.autor, newsElement.newspaper, out key)) {
				return key;
			}
			if (IsConnection(aLhs, aRhs, newsElement.newspaper, newsElement.place, out key)) {
				return key;
			}
			if (IsConnection(aLhs, aRhs, newsElement.date, newsElement.newspaper, out key)) {
				return key;
			}

			return "";
		}

		private static bool IsConnection(newsElement aLhs, newsElement aRhs, newsElement aCheckA, newsElement aCheckB, out string key)
		{
			if ((aLhs == aCheckA && aRhs == aCheckB)
				|| (aRhs == aCheckA && aLhs == aCheckB)) {
				key = KeyFromNewsElement(aCheckA) + "|" + KeyFromNewsElement(aCheckB);
				return true;
			}
			key = "";
			return false;
		}
	}
}
