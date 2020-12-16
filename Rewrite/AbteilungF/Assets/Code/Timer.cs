using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public System.Action OnCountdownEnded;
	public System.Action<float> OnCountdownReset;
	public System.Action OnDoEffect;

	[SerializeField] AnimationCurve myEffectTriggers;
	[SerializeField] Animation myStartAnimation;
	[SerializeField] Animation myEffectAnimation;
	int myLastEffectValue;

	bool myIsInCountdown = false;
	float myCountdownLeft;

	public void StartCountdown(float aLength)
	{
		if (myIsInCountdown) {
			OnCountdownReset?.Invoke(myCountdownLeft);
		}
		myIsInCountdown = true;

		myCountdownLeft = aLength;
		myLastEffectValue = (int)myEffectTriggers.Evaluate(aLength);

		myStartAnimation.Play();
	}

	private void Update()
	{
		if (!myIsInCountdown) {
			return;
		}

		myCountdownLeft -= Time.deltaTime;

		if(myCountdownLeft <= 0) {
			OnCountdownEnded?.Invoke();
			myIsInCountdown = true;
			return;
		}

		int newValue = (int)myEffectTriggers.Evaluate(myCountdownLeft);
		if(myLastEffectValue != newValue) {
			myLastEffectValue = newValue;
			OnDoEffect();
			myEffectAnimation.Play();
		}
	}
}
