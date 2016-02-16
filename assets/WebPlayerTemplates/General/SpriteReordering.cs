using UnityEngine;
using System.Collections;

public class SpriteReordering : MonoBehaviour {

	public int LayerNumber;

	// Trigger Events
	void OnTriggerEnter2D (Collider2D col) {
		if (colliderIsAcolyte (col)) {
			if (col.transform.position.y > transform.position.y) {
				col.GetComponent<Acolyte> ().ShuffleAcolyteBehind (LayerNumber);
			} else if (col.transform.position.y < transform.position.y) {
				col.GetComponent<Acolyte> ().ShuffleAcolyteInFront (LayerNumber);
			}
		}
	}
	void OnTriggerStay2D (Collider2D col) {
		if (colliderIsAcolyte (col)) {
			if (col.transform.position.y > transform.position.y) {
				col.GetComponent<Acolyte> ().ShuffleAcolyteBehind (LayerNumber);
			} else if (col.transform.position.y < transform.position.y) {
				col.GetComponent<Acolyte> ().ShuffleAcolyteInFront (LayerNumber);
			}
		}
	}
	void OnTriggerExit2D (Collider2D col) {
		
	}

	// Functions
	bool colliderIsAcolyte (Collider2D col) {
		return col.gameObject.tag == "Acolyte";
	}
}
