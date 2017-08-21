using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//Awake is always called before any Start functions
	void Awake() {

		if (instance == null) {
			instance = this;  
		} else if (instance != this) {
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);

	}

	public GameObject[] resourceSprites;
	public bool hasResource = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public GameObject GetResourceSprite (int resourceId) {
		if (resourceId >= 0) {
			return resourceSprites[resourceId];
		} else {
			Debug.Log("Tried to load sprite of resource type '-1'");
			return null;
		}
	}
}
