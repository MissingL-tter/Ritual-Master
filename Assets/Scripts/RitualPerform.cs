using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualPerform : MonoBehaviour, IButton {

	public void OnClick() {
		transform.parent.GetComponent<RitualManager>().EvaluateGuess();
	}

}
