using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSocket : MonoBehaviour {

	GameManager gameManager;

	public GameObject resource;
	public int resourceId = -1;

	// Use this for initialization
	void Start () {

		gameManager = GameManager.instance;
		
		// Load a resource into the socket
		resource = Instantiate(gameManager.GetResourceSprite(resourceId));
		resource.transform.position = transform.position;
		resource.transform.parent = transform;

	}
	
	// Update is called once per frame
	void Update () {
		
		// If this socket has no resource and we are not holding one, create a new resource
		if (resource == null && !gameManager.hasResource) {
			resource = Instantiate(GameManager.instance.GetResourceSprite(resourceId));
			resource.transform.position = transform.position;
			resource.transform.parent = transform;
		}

	}

	// Remove the resource currently in the socket
	public void RemoveResource () {
		gameManager.hasResource = true;
		resource.transform.parent = null;
		resource = null;
	}

}
