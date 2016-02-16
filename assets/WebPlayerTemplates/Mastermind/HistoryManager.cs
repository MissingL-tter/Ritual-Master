using UnityEngine;
using System.Collections;

public class HistoryManager : MonoBehaviour {
	
	public Camera historyCamera;
	public Transform historyRect;
	public Transform historyElementPrefab;
	public Transform firstPosition;
	
	public float yDifference;
	public int numHistItems = 0;
	
	private Transform lastHistoryItem;

	void Update () {
		// get pixel coords for camera to render into from historyRect
		Rect pxRect = new Rect();
		
		Vector3 topRight = historyRect.position;
		topRight.y += 0.5f * historyRect.localScale.y;
		topRight.x += 0.5f * historyRect.localScale.x;
		Vector3 botLeft = historyRect.position;
		botLeft.y -= 0.5f * historyRect.localScale.y;
		botLeft.x -= 0.5f * historyRect.localScale.x;
		
		pxRect.min = Camera.main.WorldToScreenPoint(botLeft);
		pxRect.max = Camera.main.WorldToScreenPoint(topRight);
		historyCamera.GetComponent<Camera>().pixelRect = pxRect;
		
		yDifference = historyElementPrefab.lossyScale.y;
	}
	
	public void AddHistoryItem (int[] guesses, int numFullCorrect, int numPartialCorrect) {
		Vector3 newPosition = firstPosition.position;
		newPosition.y -= yDifference * numHistItems;
		
		Transform newHistItem = GameObject.Instantiate(historyElementPrefab);
		newHistItem.position = newPosition;
		newHistItem.GetComponent<HistoryElement>().BuildHistoryBlock(guesses, numFullCorrect, numPartialCorrect);
		
		lastHistoryItem = newHistItem;
		numHistItems++;
	}
	
	public Vector3 GetLowestPoint () {
		if (lastHistoryItem != null) {
			Vector3 returnVal = lastHistoryItem.position;
			returnVal.y -= yDifference*0.5f;
			return returnVal;
		}
		return Vector3.zero;
	}
	
}
