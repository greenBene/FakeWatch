namespace AbteilungF
{
	public static class StringCollecton
	{
		public const string LOCKSCREEN = "LockScreen";
		public const string INGAME = "InGame";
		public const string CREDITS = "Credits";

		public const string TUTORIAL = "Tutorial";
		public const string START = "Start";
		public const string QUIT = "Quit";
		public const string GAMELABLE = "GameLable";
		public const string AUTORLABLE = "AutorLable";
		public const string CREDITSLABLE = "CreditsLable";

		public const string GAMETITLE = "GameTitle";
		public const string CREDITSTITLE = "CreditsTitle";

		public const string FAKE = "Fake";
		public const string CORRECT = "Correct";
		public const string PASSWORD = "Password";

		public const string NO_CONNECTION = "None|None";
		public const string TITLE = "Title";
		public const string AUTOR = "Autor";
		public const string NEWSPAPER = "Newspaper";
		public const string PLACE = "Place";
		public const string DATE = "Date";
		public const string AOE = "AOE";

		public static string KeyFromNewsElement(newsElement aElement)
		{
			switch (aElement) {
			case newsElement.title:
				return TITLE;
			case newsElement.autor:
				return AUTOR;
			case newsElement.newspaper:
				return NEWSPAPER;
			case newsElement.place:
				return PLACE;
			case newsElement.date:
			case newsElement.day:
				return DATE;
			case newsElement.areaOfExpertise:
				return AOE;
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
				key = "[" + KeyFromNewsElement(aCheckA) + "|" + KeyFromNewsElement(aCheckB) + "]";
				return true;
			}
			key = "";
			return false;
		}
	}
}
