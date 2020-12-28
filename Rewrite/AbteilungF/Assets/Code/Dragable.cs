using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	Vector2 myCurserOffset;

	public void OnBeginDrag(PointerEventData eventData)
	{
		myCurserOffset = (Vector2)transform.position - eventData.position;
		transform.SetAsLastSibling();
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position + myCurserOffset;
	}

	public void OnEndDrag(PointerEventData eventData)
	{

	}

	public void OnPointerClick(PointerEventData eventData)
	{
		transform.SetAsLastSibling();
	}
}
