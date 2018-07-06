using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Icon : MonoBehaviour {
	public float maxTimeBetweenClicks = 0.2f;
	
	private bool clicked = false;
	private float timeSinceClick = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(clicked) {
			timeSinceClick += Time.deltaTime;
			if(timeSinceClick > maxTimeBetweenClicks) {
				Reset();
			}
		}
	}
	
	void OnMouseDown() {
		if (clicked) {
			Reset();
			Execute();
		}
		else {
			HandleFirstClick();
		}
	}
	
	private void Reset() {
		clicked = false;
		timeSinceClick = 0.0f;
	}
	
	private void HandleFirstClick() {
		clicked = true;
		timeSinceClick = 0.0f;
	}
	
	protected abstract void Execute();
}
