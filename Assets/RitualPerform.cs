using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualPerform : MonoBehaviour {

	void OnMouseDown() {
		transform.parent.GetComponent<RitualManager>().EvaluateGuess();
	}

}
