public class DynamicProgression : IProgression
{
	public float GetCurrentDelay()
	{
		throw new System.NotImplementedException();
	}

	public bool HasReachedMaxProgression()
	{
		throw new System.NotImplementedException();
	}

	public void SetCorrect()
	{
		throw new System.NotImplementedException();
	}

	public void SetFalseNegative()
	{
		throw new System.NotImplementedException();
	}

	public void SetFalsePositive()
	{
		throw new System.NotImplementedException();
	}

	public News TriggerNews(INewsFactory aFactory)
	{
		return aFactory.GetNextNews(Data.GetInstance().myLocalisator);
	}
}
