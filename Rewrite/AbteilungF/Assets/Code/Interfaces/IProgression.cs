
public interface IProgression
{
	News TriggerNews(INewsFactory aFactory);
	bool HasReachedMaxProgression();
	float GetCurrentDelay();
	void SetFalsePositive();
	void SetFalseNegative();
	void SetCorrect();
}
