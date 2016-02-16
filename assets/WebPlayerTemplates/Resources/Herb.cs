using UnityEngine;
using System.Collections;

public class Herb : MonoBehaviour {

	private GameObject herbSprite;
	private GameObject resourceHighlight;
	private Vector3 snapToPoint;

	public float RespawnTime;

	bool herbIsBeingGathered = false;
	bool herbIsInactive = false;
	bool herbIsFlaggedForGathering = false;

	// Use this for initialization
	void Start () {
		herbSprite = transform.Find ("Herb_Sprite").gameObject;

		resourceHighlight = transform.Find ("Resource_Highlight").gameObject;
		resourceHighlight.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0f);

		snapToPoint = new Vector3 (transform.position.x, transform.position.y - 0.1f, 0);
	}
	
	// Trigger Functions
	void OnTriggerEnter2D (Collider2D col) {
		if (colliderIsAcolyte (col)) {
			if (!herbIsBeingGathered && !herbIsInactive && herbIsFlaggedForGathering) {
				if (!col.GetComponent<Acolyte> ().acolyteIsCollectingResource) {
					col.GetComponent<Acolyte> ().GatheringResource ("Incense");
					StartCoroutine ("IncenseIsBeingGathered");

					col.transform.position = snapToPoint;
					col.GetComponent<Acolyte> ().moveToPosition = snapToPoint;
				}
			}
		}
	}

	// Mouse Functions
	void OnMouseEnter() {
		if (ScriptHub.station.gameControlScript.acolyteSelected) {
			resourceHighlight.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}
	}
	void OnMouseOver() {
		if (Input.GetMouseButtonDown (1)) {
			herbIsFlaggedForGathering = true;
		}
	}
	void OnMouseExit() {
		resourceHighlight.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0f);
	}

	// CoRoutines
	IEnumerator IncenseIsBeingGathered () {
		herbIsBeingGathered = true;

		yield return new WaitForSeconds (ScriptHub.station.gameControlScript.IncenseGatherTime);

		herbSprite.SetActive (false);
		herbIsInactive = true;
		herbIsBeingGathered = false;
		herbIsFlaggedForGathering = false;

		StartCoroutine ("HerbRespawnTimer");
	}
	IEnumerator HerbRespawnTimer () {
		yield return new WaitForSeconds (RespawnTime);

		herbSprite.SetActive (true);
		herbIsInactive = false;
	}

	// Functions
	bool colliderIsAcolyte (Collider2D col) {
		return col.gameObject.tag == "Acolyte";
	}
}
