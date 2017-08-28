using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualPerform : MonoBehaviour {

	public GameObject ritual;
	RitualManager ritualManager;

    void Start () {
		ritualManager = ritual.GetComponent<RitualManager>();
	}

	void OnMouseDown () {
		int[] results = ritualManager.EvaluateGuess();
	}

}
