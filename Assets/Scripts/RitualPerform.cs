using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualPerform : MonoBehaviour, IButton {

	public GameObject ritual;

	public void OnClick() {
		GameObject curRitual = transform.parent.gameObject;
		//RitualGenerator curGenerator = curRitual.GetComponent<RitualGenerator>();
		RitualManager curManager = curRitual.GetComponent<RitualManager>();

		int[] results = curManager.EvaluateGuess();


	}

}
