using UnityEngine;
using System.Collections;

public class HistoryV2 : MonoBehaviour {

	// TODO: this script should really use instantiation instead of GO creation
	// help us help you get 50% faster creation frames!

	public float paddingAbove = 0.1f; // percentage of yDelta to use for padding above each element
	public float paddingSide = 0.1f; // ALSO percentage of yDelta, for side padding
	public float elementHeight = 0.25f; // percentage of height of historywindow of each history item
	public float maskWindowScale = 0.85f; // percentage of history frame that history items are shown in
	public float attemptCircleScale = 0.9f; // percentage of height of one item
	public float attemptElementScale = 0.9f; // percentage of maximum non-overlapping size

	private Vector3 topRight; // top right corner in world coords
	private Vector3 bottomLeft; // bottom left corner in world coords
	private Vector3 topCenter; // world coords

	private Material maskMat;
	private Material maskedSpriteMat;
	private Sprite maskSprite;

	private GameObject window;
	private GameObject histPane;

	private int numGuesses; // number of guesses made so far. initialized to 0 during setup
	private float yDelta; // units to move downwards from one guess to the next
	// (same as height of one item)
	private float windowWidth;
	private float graphicSize;
	private float elementSize;

	// Initialize history window and get references
	public void Setup () {
		numGuesses = 0;
		// get references for materials to make the window work
		maskMat = Hub.central.resources.maskMaterial;
		maskedSpriteMat = Hub.central.resources.maskedSpriteMaterial;
		maskSprite = Hub.central.resources.maskSprite;
		// get coordinates for window
		topRight = transform.position;
		topRight.x += transform.localScale.x;
		topRight.y += transform.localScale.y;
		bottomLeft = transform.position;
		bottomLeft.x -= transform.localScale.x;
		bottomLeft.y -= transform.localScale.y;
		// create pane that holds all items
		// pane is masked and uses dragging to move children (history elements)
		// set the window material to Sprite_Mask,
		// and the pane material and children to Sprite_DefaultMasked

		// create window
		window = new GameObject("Mask Window");
		// add sprite renderer and mask sprite
		SpriteRenderer maskSpriteRenderer = window.AddComponent<SpriteRenderer> ();
		maskSpriteRenderer.sprite = maskSprite;
		// set material
		maskSpriteRenderer.material = maskMat;
		// set transform as the same as parent window
		window.transform.SetParent(transform);
		window.transform.localPosition = Vector3.zero;
		window.transform.localScale = Vector3.one * maskWindowScale;
		windowWidth = window.transform.lossyScale.x;
		yDelta = window.transform.lossyScale.y * elementHeight;

		// create parent for all history items
		histPane = new GameObject("History Item Pane");
		// set position to the middle of the top edge of the mask
		topCenter = window.transform.position;
		topCenter.y += window.transform.lossyScale.y * 0.5f;
		// move down by half the height of one element
		topCenter.y -= yDelta * 0.5f;
		histPane.transform.position = topCenter;
		// set window as parent
		histPane.transform.SetParent(window.transform);
		// make sure local scale is still 1
		histPane.transform.localScale = Vector3.one;

		graphicSize = yDelta * attemptCircleScale * 0.5f; // radius?
		float maxSize = graphicSize * Mathf.Sin(Mathf.PI / (float)Hub.central.gm.numTypes);
		elementSize = maxSize * attemptElementScale; // radius
	}
	
	// adds a history item representing the guess made and the result of that guess
	// guess[]: each int is the code for resource type in that slot
	// result[]: result[0] is full, result[1] is partial
	public void AddHistoryItem (int[] guess, int[] result) {
		// create the parent object for this item
		GameObject newHistItem = new GameObject ("HistoryItem");
		newHistItem.transform.position = topCenter - (Vector3.up * yDelta * numGuesses);
		newHistItem.transform.SetParent (histPane.transform,true);

		float leftEdge = newHistItem.transform.position.x - (windowWidth * 0.5f);
		Vector3 graphicCenter = newHistItem.transform.position;
		graphicCenter.x = leftEdge + (yDelta * 0.5f) + (paddingSide * yDelta) + elementSize;
		graphicCenter.y -= (paddingAbove * yDelta) + elementSize;
		//Debug.DrawRay (graphicCenter, Vector3.right, Color.white, 2.0f, false);
		float radDelta = -Mathf.PI * (2.0f / Hub.central.gm.numSlots); // one n-th of a full circle in rads
		Vector3 instPosition; // position to instantiate to (avoiding GC)
		float rad; // avoiding GC
		for (int i = 0; i < Hub.central.gm.numSlots; i++) {
			rad = (radDelta * i) + (0.5f * Mathf.PI);
			instPosition.x = Mathf.Cos (rad);
			instPosition.y = Mathf.Sin (rad);
			instPosition.z = 0;
			instPosition = (graphicSize) * instPosition;

			GameObject go = Instantiate (Hub.central.resources.historyElementPrefab [guess [i]]);
			go.SetActive (true);
			go.transform.position = graphicCenter + instPosition;
			go.transform.SetParent (newHistItem.transform);
			go.transform.localScale = Vector3.one * elementSize;
		}

		numGuesses++;
	}
}
