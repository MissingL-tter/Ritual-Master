using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	GameManager gameManager;

	ResourceSocket resourceSocket;
	RitualSocket ritualSocket;

	public int id = -1;

	// Use this for initialization
	void Start () {

		gameManager = GameManager.instance;

		// Try to get both sockets, only one will return and the other will be null
		resourceSocket = gameObject.transform.parent.GetComponent<ResourceSocket>();
		ritualSocket = gameObject.transform.parent.GetComponent<RitualSocket>();

	}

	void OnMouseDown () {

		// Remove the resource from its socket
		if (ritualSocket != null) {
			ritualSocket.RemoveResource();
		} else {
			resourceSocket.RemoveResource();
		}

	}

	void OnMouseDrag() {

		// When the pointer is moved, move the resource with it
		transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}

	void OnMouseUp () {

		// Raycast only for RitualSockets, if we find a socket then give the this resource to that socket
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, LayerMask.GetMask("RitualSocket"));
        if (hit.collider != null) {
			ritualSocket = hit.collider.GetComponent<RitualSocket>();
			ritualSocket.TakeResource(gameObject);
		} else {
			gameManager.hasResource = false;
			Destroy(gameObject);
		}

	}

}
