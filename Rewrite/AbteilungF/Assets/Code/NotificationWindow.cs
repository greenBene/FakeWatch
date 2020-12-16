using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationWindow : MonoBehaviour
{
	public System.Action OnRemove;

	[SerializeField] TextMeshProUGUI myMessage;
	[SerializeField] Animation myMoveInAnimation;
	[SerializeField] Animation myMoveOutAnimation;

	LinkedListNode<NotificationWindow> mySelfIterator;
	float myTargetPositionHight;
	float myHight;

	public void Setup(LinkedListNode<NotificationWindow> aSelfIterator, float aTimeToLife)
	{
		mySelfIterator = aSelfIterator;
		StartCoroutine(Kill(aTimeToLife));
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
		myMoveOutAnimation.Play();
		OnRemove?.Invoke();
	}

	IEnumerator Kill(float delay)
	{
		yield return new WaitForSeconds(delay);
		mySelfIterator.List.Remove(mySelfIterator);
		Disappear();
	}
	
    void Update()
    {
        //TODO: get to target position
    }
}
