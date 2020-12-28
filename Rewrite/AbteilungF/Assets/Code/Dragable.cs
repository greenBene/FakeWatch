using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	Vector2 myCurserOffset;

	public void OnBeginDrag(PointerEventData eventData)
	{
		myCurserOffset = (Vector2)transform.position - eventData.position;
		Debug.Log("start");
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position + myCurserOffset;
	}

	public void OnEndDrag(PointerEventData eventData)
	{

	}
}
