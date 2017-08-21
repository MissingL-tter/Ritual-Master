using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	GameManager gameManager;

	ResourceSocket resourceSocket;
	RitualSocket ritualSocket;

	public int resourceId = -1;

	// Use this for initialization
	void Start () {

		gameManager = GameManager.instance;

		resourceSocket = gameObject.transform.parent.GetComponent<ResourceSocket>();
		ritualSocket = gameObject.transform.parent.GetComponent<RitualSocket>();

	}

	void OnMouseDown () {

		gameManager.heldResource = gameObject;
		if (ritualSocket != null) {
			ritualSocket.resource = null;
			ritualSocket = null;
		} else {
			resourceSocket.resource = null;
			resourceSocket = null;
		}
		transform.parent = null;

	}

	void OnMouseDrag() {

		transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}

	void OnMouseUp () {

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, LayerMask.GetMask("Socket"));
        if (hit.collider != null) {
			ritualSocket = hit.collider.GetComponent<RitualSocket>();
		}

		gameManager.heldResource = null;
		if (ritualSocket != null) {
			if (ritualSocket.resource != null) {
				Destroy(ritualSocket.transform.GetChild(1).gameObject);
			}
			ritualSocket.resource = gameObject;
			transform.position = ritualSocket.transform.position;
			transform.parent = ritualSocket.transform;
			gameManager.heldResource = null;
		} else {
			Destroy(gameObject);
			gameManager.heldResource = null;
		}

	}

}
