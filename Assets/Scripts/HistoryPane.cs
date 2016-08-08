using UnityEngine;
using System.Collections;

public class HistoryPane : MonoBehaviour {

	private int numGuesses;
	private float yDelta;
	private float winHeight;
	private float minHeight;
	private float maxHeight;

	private Vector3 lastMousePosition;
	private Vector3 startPosition;
	private Vector3 unclampedPos;

	void Start () {
		numGuesses = 0;
	}

	// initialize, give size of each element for scroll locking and
	// take position when called as implicit min height
	public void Init (float yDelta, float windowHeight) {
		startPosition = transform.position;
		this.yDelta = yDelta;
		this.winHeight = windowHeight;
		this.minHeight = startPosition.y;
	}

	// reset max height to
	// bottomOfWindow + yDelta*numGuesses
	public void Resize (int numGuesses) {
		maxHeight = minHeight - (winHeight) + (yDelta * numGuesses);
		if (maxHeight < minHeight) maxHeight = minHeight;
		
	}

	public void GetMouseDown () {
		lastMousePosition = Input.mousePosition;
	}

	public void GetMouseDrag () {
		float delta = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - 
			Camera.main.ScreenToWorldPoint(lastMousePosition).y;
		lastMousePosition = Input.mousePosition;
		// clamp position
		unclampedPos = transform.position;
		unclampedPos.y += delta;
		unclampedPos.y = Mathf.Clamp(unclampedPos.y,minHeight,maxHeight);
		transform.position = unclampedPos;
	}
}
