using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualSocket : MonoBehaviour {

	GameManager gameManager;

	public GameObject resource;
	public int resourceId;

	// Use this for initialization
	void Start () {

		gameManager = GameManager.instance;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Take the resource and establish parenthood over resource
	public void TakeResource (GameObject newResource) {
		if (resource != null) {
			Destroy(transform.GetChild(1).gameObject);
		}
		resource = newResource;
		resourceId = newResource.GetComponent<Resource>().id;
		resource.transform.position = gameObject.transform.position;
		resource.transform.parent = gameObject.transform;
		gameManager.hasResource = false;
	}

	// Remove the resource currently in the socket
	public void RemoveResource () {
		gameManager.hasResource = true;
		resource.transform.parent = null;
		resourceId = -1;
		resource = null;
	}
}
