using UnityEngine;
using System.Collections;

public class HistoryV2 : MonoBehaviour {

	public Vector3 topRight; // top right corner in world coords
	public Vector3 bottomLeft; // bottom left corner in world coords

	// Initialize history window
	public void Setup () {
		
	}
	
	// adds a history item representing the guess made and the result of that guess
	// guess[]: each int is the code for resource type in that slot
	// result[]: result[0] is full, result[1] is partial
	public void AddHistoryItem (int[] guess, int[] result) {
		
	}
}
