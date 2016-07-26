using UnityEngine;
using System.Collections;

public class PerformRitualButton : MonoBehaviour {
	
	// tell game manager to perform the ritual
	public void OnMouseDown () {
		Hub.central.gm.AttemptRitual();
	}
}
