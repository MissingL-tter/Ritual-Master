using UnityEngine;
using System.Collections;

public class GameControllerOld : MonoBehaviour {

	/*public GameObject[] Acolytes;

	public int BloodAmount = 0;
	public int GoldAmount = 0;
	public int IncenseAmount = 0;

	public float ritualTimeInterval;
	public float BloodGatherTime;
	public float GoldGatherTime;
	public float IncenseGatherTime;

	public bool acolyteSelected = false;
	public bool RitualGameRunning = false;

	bool spaceDoubleTapTimerRunning = false;
	bool oneKeyDoubleTapTimerRunning = false;
	bool twoKeyDoubleTapTimerRunning = false;
	bool threeKeyDoubleTapTimerRunning = false;
	bool fourKeyDoubleTapTimerRunning = false;
	bool fiveKeyDoubleTapTimerRunning = false;



	// Startup
	void Awake () {
		StartCoroutine ("RitualTimer");
	}

	// Update is called once per frame
	void Update () {
		if (RitualGameRunning) {

		} else if (!RitualGameRunning) {
			if (Input.GetButtonDown ("Submit")) {
				if (!spaceDoubleTapTimerRunning) {
					StartCoroutine ("WaitForSpaceDoubleTap");
				} else if (spaceDoubleTapTimerRunning) {
					ScriptHub.station.cameraControlScript.CenterOnBase ();
				}
			} else if (Input.GetButtonDown ("Acolyte1")) {
				if (!oneKeyDoubleTapTimerRunning) {
					Acolytes [0].GetComponent<Acolyte> ().SelectAcolyte ();
					Acolytes [1].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [2].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [3].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [4].GetComponent<Acolyte> ().DeSelectAcolyte ();
					StartCoroutine ("WaitForOneKeyDoubleTap");
				} else if (oneKeyDoubleTapTimerRunning) {
					ScriptHub.station.cameraControlScript.CenterOnAcolyte (Acolytes [0].transform.position);
				}
			} else if (Input.GetButtonDown ("Acolyte2")) {
				if (!twoKeyDoubleTapTimerRunning) {
					Acolytes [0].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [1].GetComponent<Acolyte> ().SelectAcolyte ();
					Acolytes [2].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [3].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [4].GetComponent<Acolyte> ().DeSelectAcolyte ();

					StartCoroutine ("WaitForTwoKeyDoubleTap");
				} else if (twoKeyDoubleTapTimerRunning) {
					ScriptHub.station.cameraControlScript.CenterOnAcolyte (Acolytes [1].transform.position);
				}
			} else if (Input.GetButtonDown ("Acolyte3")) {
				if (!threeKeyDoubleTapTimerRunning) {
					Acolytes [0].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [1].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [2].GetComponent<Acolyte> ().SelectAcolyte ();
					Acolytes [3].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [4].GetComponent<Acolyte> ().DeSelectAcolyte ();

					StartCoroutine ("WaitForThreeKeyDoubleTap");
				} else if (threeKeyDoubleTapTimerRunning) {
					ScriptHub.station.cameraControlScript.CenterOnAcolyte (Acolytes [2].transform.position);
				}
			} else if (Input.GetButtonDown ("Acolyte4")) {
				if (!fourKeyDoubleTapTimerRunning) {
					Acolytes [0].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [1].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [2].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [3].GetComponent<Acolyte> ().SelectAcolyte ();
					Acolytes [4].GetComponent<Acolyte> ().DeSelectAcolyte ();

					StartCoroutine ("WaitForFourKeyDoubleTap");
				} else if (fourKeyDoubleTapTimerRunning) {
					ScriptHub.station.cameraControlScript.CenterOnAcolyte (Acolytes [3].transform.position);
				}
			} else if (Input.GetButtonDown ("Acolyte5")) {
				if (!fiveKeyDoubleTapTimerRunning) {
					Acolytes [0].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [1].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [2].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [3].GetComponent<Acolyte> ().DeSelectAcolyte ();
					Acolytes [4].GetComponent<Acolyte> ().SelectAcolyte ();

					StartCoroutine ("WaitForFiveKeyDoubleTap");
				} else if (fiveKeyDoubleTapTimerRunning) {
					ScriptHub.station.cameraControlScript.CenterOnAcolyte (Acolytes [4].transform.position);
				}
			}
		}
	}

	public void EndRitual () {
		RitualGameRunning = false;
		StartCoroutine ("RitualTimer");
	}

	// CoRoutines
	IEnumerator RitualTimer () {
		yield return new WaitForSeconds (ritualTimeInterval);

		RitualGameRunning = true;
		ScriptHub.station.cameraControlScript.StartRitualGame ();
	}

	IEnumerator WaitForSpaceDoubleTap () {
		spaceDoubleTapTimerRunning = true;

		yield return new WaitForSeconds (0.25f);

		spaceDoubleTapTimerRunning = false;
	}
	IEnumerator WaitForOneKeyDoubleTap () {
		oneKeyDoubleTapTimerRunning = true;

		yield return new WaitForSeconds (0.25f);

		oneKeyDoubleTapTimerRunning = false;
	}
	IEnumerator WaitForTwoKeyDoubleTap () {
		twoKeyDoubleTapTimerRunning = true;

		yield return new WaitForSeconds (0.25f);

		twoKeyDoubleTapTimerRunning = false;
	}
	IEnumerator WaitForThreeKeyDoubleTap () {
		threeKeyDoubleTapTimerRunning = true;

		yield return new WaitForSeconds (0.25f);

		threeKeyDoubleTapTimerRunning = false;
	}
	IEnumerator WaitForFourKeyDoubleTap () {
		fourKeyDoubleTapTimerRunning = true;

		yield return new WaitForSeconds (0.25f);

		fourKeyDoubleTapTimerRunning = false;
	}
	IEnumerator WaitForFiveKeyDoubleTap () {
		fiveKeyDoubleTapTimerRunning = true;

		yield return new WaitForSeconds (0.25f);

		fiveKeyDoubleTapTimerRunning = false;
	}*/
}
