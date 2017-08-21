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
		
		resource = Instantiate(gameManager.GetResourceSprite(resourceId));
		resource.transform.position = transform.position;
		resource.transform.parent = transform;

	}
	
	// Update is called once per frame
	void Update () {
		
		// If this socket has no resource and we are not holding it, create a new resource
		if (resource == null && gameManager.heldResource == null) {
			resource = Instantiate(GameManager.instance.GetResourceSprite(resourceId));
			resource.transform.position = gameObject.transform.position;
			resource.transform.parent = gameObject.transform;
		}

	}
}
