using UnityEngine;
using System.Collections;

public class DraggedItem : MonoBehaviour {
	
	public void Start () {
		gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Props-foreground");
	}

	// move self with mouse movement
	public void Update () {
		Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		newPosition.z = 0;
		transform.position = newPosition;
		
		// when the mouse is released, self destruct. ItemSlot will notify gm for us
		if (Input.GetMouseButtonUp(0)) {
			Hub.central.gm.currentDraggedType = -1;
			Destroy(gameObject);
		}
	}
	
}
