using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventTrigger))]
public abstract class Window : MonoBehaviour {
	
	private bool dragging = false;
	private Vector2 distanceToMouse;

	// Use this for initialization
	public virtual void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if (dragging)
        {
            SetPosition((Vector2)Input.mousePosition + distanceToMouse);
        }
	}
	
	public virtual void Show() {
        gameObject.SetActive(true);
	}
	
	public void SetPosition(int x, int y) {
		SetPosition(new Vector2(x, y));
	}
	
	public void SetPosition(Vector2 pos) {
		transform.position = pos;
	}
	
	public void StartDragging()
    {
        distanceToMouse = transform.position - Input.mousePosition;
        dragging = true;
    }

    public void StopDragging()
    {
        dragging = false;
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void Destroy(float delay)
    {
        transform.position = new Vector2(-10000, -10000);
        Destroy(gameObject, delay);
    }
}
