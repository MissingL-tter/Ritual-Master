using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour {

	private GameObject resourceHighlight;
	private Vector3 snapToPoint;

	bool goldIsBeingGathered = false;

	// Use this for initialization
	void Start () {
		resourceHighlight = transform.Find ("Resource_Highlight").gameObject;
		resourceHighlight.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0f);

		snapToPoint = new Vector3 (transform.position.x, transform.position.y - 0.1f, 0);
	}

	
	// Trigger Functions
	void OnTriggerEnter2D (Collider2D col) {
		if (colliderIsAcolyte (col)) {
			if (!goldIsBeingGathered) {
				col.GetComponent<Acolyte> ().GatheringResource ("Gold");
				StartCoroutine ("GoldIsBeingGathered");

				col.transform.position = snapToPoint;
				col.GetComponent<Acolyte> ().moveToPosition = snapToPoint;
				col.GetComponent<Acolyte> ().ShuffleAcolyteBehind (5);
			}
		}
	}


	// Mouse Functions
	void OnMouseEnter() {
		if (ScriptHub.station.gameControlScript.acolyteSelected) {
			resourceHighlight.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}
	}
	void OnMouseExit() {
		resourceHighlight.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0f);
	}


	// CoRoutines
	IEnumerator GoldIsBeingGathered () {
		yield return new WaitForSeconds (ScriptHub.station.gameControlScript.GoldGatherTime);
	}


	// Functions
	bool colliderIsAcolyte (Collider2D col) {
		return col.gameObject.tag == "Acolyte";
	}
}
