using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MastermindSwitchButton : MonoBehaviour {
	
	public bool isReturnButton;

	void OnMouseDown () {
		if (isReturnButton) {
			ScriptHub.station.GUIControlScript.SyncResources();
			ScriptHub.station.cameraControlScript.EndRitualGame ();
		}
		else {
			ScriptHub.station.cameraControlScript.StartRitualGame ();
		}
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			SceneManager.LoadScene(0);
		}
	}
}
