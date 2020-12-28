using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AbteilungF
{
	public class WindowRefHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public System.Action OnClose;
		public System.Action OnMinimice;

		public RectTransform myExeContent;
		public RectTransform myButtonPanle;

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

		private void Start()
		{
			if (!myExeContent) {
				Debug.LogError("Executable Content Not Set");
				Destroy(gameObject);
				return;
			}
		}

		public void Close()
		{
			OnClose?.Invoke();
		}
	}
}
