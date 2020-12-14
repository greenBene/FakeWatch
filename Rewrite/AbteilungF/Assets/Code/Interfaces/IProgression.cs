
public interface IProgression
{
	News TriggerNews(INewsFactory aFactory);
	float GetCurrentDelay();
	void SetFalsePositive();
	void SetFalseNegative();
	void SetCorrect();
}
