using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSocket : Socket {

	GameManager gameManager;
	Player player;

	public int resourceId = -1;

	// Use this for initialization
	void Start () {

		gameManager = GameManager.instance;
		player = Player.instance;
		
		// Load a resource into the socket
		resource = Instantiate(gameManager.GetResource(resourceId));
		resource.transform.position = transform.position;
		resource.transform.parent = transform;

	}
	
	// Update is called once per frame
	void Update () {

		// If we have no children, then we should have no resource
		if (transform.childCount == 0) {
			resource = null;
		}
		
		// If this socket has no resource and we are not holding one, create a new resource
		if (resource == null && player.heldResource == null) {
			resource = Instantiate(GameManager.instance.GetResource(resourceId));
			resource.transform.position = transform.position;
			resource.transform.parent = transform;
		}

	}

}
