public static class StringCollecton
{
	public const string LOCKSCREEN = "LockScreen";
	public const string INGAME = "InGame";

	public const string FAKE = "Fake";
	public const string CORRECT = "Correct";

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
			return "Date";
		case newsElement.areaOfExpertise:
			return "AOE";
		default:
			return "";
		}
	}
}
