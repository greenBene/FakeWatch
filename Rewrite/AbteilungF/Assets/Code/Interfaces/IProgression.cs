namespace AbteilungF
{
	public interface IProgression
	{
		News TriggerNews(INewsFactory aFactory, ILocalisator aLocalisator);
		bool HasReachedMaxProgression();
		float GetCurrentDelay();
		void SetFalsePositive();
		void SetFalseNegative();
		void SetCorrect();
	}
}
