using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NotificationWindow : MonoBehaviour
{
	const float locSmallNumber = 0.001f;

	public System.Action OnRemove;

	[SerializeField] TextMeshProUGUI myMessage;
	[SerializeField] Animation myMoveInAnimation;
	[SerializeField] Animation myMoveOutAnimation;

	LinkedListNode<NotificationWindow> mySelfIterator;
	float myTargetPositionHight;
	float myHight;
	float mySpeed;

	public void Setup(LinkedListNode<NotificationWindow> aSelfIterator, float aSpeed, float aTimeToLife)
	{
		mySelfIterator = aSelfIterator;
		StartCoroutine(Kill(aTimeToLife));
		mySpeed = aSpeed;
		myMoveInAnimation.Play();
	}

	public void ChangeText(string aText)
	{
		myMessage.text = aText;
		// TODO: caluclate new Hight
		ChangeTargetPositionHight(myTargetPositionHight);
	}

	void ChangeTargetPositionHight(float aTargetPositionHight)
	{
		myTargetPositionHight = aTargetPositionHight;
		mySelfIterator.Next?.Value.ChangeTargetPositionHight(aTargetPositionHight + myHight);
	}

	public void Disappear()
	{
		StopAllCoroutines();
		mySelfIterator.Next?.Value.ChangeTargetPositionHight(myTargetPositionHight);

		myMoveOutAnimation.Play();
		OnRemove?.Invoke();
	}

	IEnumerator Kill(float delay)
	{
		yield return new WaitForSeconds(delay);
		Disappear();
		mySelfIterator.List.Remove(mySelfIterator);
	}

	void Update()
	{
		var rectTrans = (RectTransform)transform;
		if (Mathf.Abs(myTargetPositionHight - rectTrans.anchoredPosition.x) < locSmallNumber) {
			return;
		}

		bool HasToGoUp = myTargetPositionHight > rectTrans.anchoredPosition.x;

		rectTrans.anchoredPosition += new Vector2(HasToGoUp ? mySpeed : -mySpeed, 0);

		if ((myTargetPositionHight > rectTrans.anchoredPosition.x) != HasToGoUp) {
			rectTrans.anchoredPosition = new Vector2(myTargetPositionHight, rectTrans.anchoredPosition.y);
		}
	}
}
