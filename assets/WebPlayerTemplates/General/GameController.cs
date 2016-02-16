using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] Acolytes;
	public GameObject selectedAcolyte;

	public int BloodAmount = 0;
	public int GoldAmount = 0;
	public int IncenseAmount = 0;

	public float ritualTimeInterval;
	public float BloodGatherTime;
	public float GoldGatherTime;
	public float IncenseGatherTime;
	public float doublePressTime = 0.25f;

	public bool acolyteSelected = false;
	public bool RitualGameRunning = false;

	private float[] lastKeyTime = new float[5];
	private float lastSpaceTime;

	// Startup
	void Awake () {
		for (int i=0;i<lastKeyTime.Length;i++) {
			lastKeyTime[i] = 0.0f;
		}
		lastSpaceTime = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		if (!RitualGameRunning) {
			
			if (Input.anyKeyDown) {
				string keyDown = Input.inputString;
				if (keyDown.Length > 0) {
					int keyIndex = keyDown[0] - ((int)'1');
					if (keyIndex>=0 && keyIndex<=4) {
						if (Time.time - lastKeyTime[keyIndex] > doublePressTime) {
							for (int i=0;i<Acolytes.Length;i++) {
								if (i==keyIndex) {
									Acolytes[i].GetComponent<Acolyte>().SelectAcolyte();
								}
								else {
									Acolytes[i].GetComponent<Acolyte>().DeSelectAcolyte();
								}
							}
						}
						if (Time.time - lastKeyTime[keyIndex] <= doublePressTime) {
							// key double pressed
							ScriptHub.station.cameraControlScript.CenterOnAcolyte (Acolytes [keyIndex].transform.position);
						}
						lastKeyTime[keyIndex] = Time.time;
					}
				}
			}
		
			if (Input.GetButtonDown ("Submit")) {
				if (Time.time - lastSpaceTime <= doublePressTime) {
					ScriptHub.station.cameraControlScript.CenterOnBase ();
				}
				lastSpaceTime = Time.time;
			}
		
		}
		
	}
}
