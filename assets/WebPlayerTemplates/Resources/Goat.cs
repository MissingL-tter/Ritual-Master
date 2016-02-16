using UnityEngine;
using System.Collections;

public class Goat : MonoBehaviour {

	private GameObject goatSprite;
	private GameObject resourceHighlight;
	private Vector3 snapToPoint;

	public float RespawnTime;

	bool goatIsBeingGathered = false;
	GameObject goatIsGatherableBy;
	
	bool goatIsInactive = false;
	

    private AudioSource audioSource;
    public AudioClip goatBleatSound;

    // Use this for initialization
    void Start () {
		goatSprite = transform.Find ("Goat_Sprite").gameObject;

		resourceHighlight = transform.Find ("Resource_Highlight").gameObject;
		resourceHighlight.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0f);

		snapToPoint = new Vector3 (transform.position.x, transform.position.y - 0.1f, 0);
        audioSource = GetComponent<AudioSource>();
    }

	
	// Trigger Functions
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject == goatIsGatherableBy) {
			if (!goatIsBeingGathered && !goatIsInactive) {
				col.GetComponent<Acolyte> ().GatheringResource ("GoatBlood");
				StartCoroutine ("GoatIsBeingGathered");

				col.transform.position = snapToPoint;
				col.GetComponent<Acolyte> ().moveToPosition = snapToPoint;
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
		if (ScriptHub.station.gameControlScript.acolyteSelected) {	
			if (Input.GetMouseButtonDown (1)) {
				goatIsGatherableBy = ScriptHub.station.gameControlScript.selectedAcolyte;
			}
		}
	}
	void OnMouseExit() {
		resourceHighlight.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0f);
	}


	// CoRoutines
	IEnumerator GoatIsBeingGathered () {
		goatIsBeingGathered = true;

		yield return new WaitForSeconds (ScriptHub.station.gameControlScript.BloodGatherTime);

		goatSprite.SetActive (false);
		goatIsInactive = true;
		goatIsBeingGathered = false;

		StartCoroutine ("GoatRespawnTimer");

        audioSource.PlayOneShot(goatBleatSound, 0.65f);
    }
	IEnumerator GoatRespawnTimer () {
		yield return new WaitForSeconds (RespawnTime);

		goatSprite.SetActive (true);
		goatIsInactive = false;
	}


	// Functions
	bool colliderIsAcolyte (Collider2D col) {
		return col.gameObject.tag == "Acolyte";
	}
}
