using UnityEngine;
using System.Collections;

public class HistoryPane : MonoBehaviour {

	private int numGuesses;
	private float yDelta;
	private float winHeight;
	private float minHeight;
	private float maxHeight;

	private Vector3 lastMousePosition;

	void Start () {
		numGuesses = 0;
	}

	// initialize, give size of each element for scroll locking and
	// take position when called as implicit min height
	public void Init (float yDelta, float windowHeight) {
		this.yDelta = yDelta;
		this.winHeight = windowHeight;
	}

	public void Resize (int numGuesses) {
		
	}

	public void GetMouseDown () {
		lastMousePosition = Input.mousePosition;
	}

	public void GetMouseDrag () {
		float delta = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - 
			Camera.main.ScreenToWorldPoint(lastMousePosition).y;
		transform.Translate (0, delta, 0);
		lastMousePosition = Input.mousePosition;
	}
}
