using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualNext : MonoBehaviour {

	GameManager gameManager;

	void Start () {
		gameManager = GameManager.instance;
	}

	void OnMouseDown () {
		gameManager.LoadNextLevel();
	}

}
