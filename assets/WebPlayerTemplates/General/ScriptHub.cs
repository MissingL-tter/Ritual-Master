using UnityEngine;
using System.Collections;

public class ScriptHub : MonoBehaviour {
	public static ScriptHub station;

	public GameController gameControlScript;
	public GUIController GUIControlScript;
	public CameraControl cameraControlScript;
	public TryComboButton ritualButtonScript;
    public SoundController soundControlScript;

	void Awake () {
		if (station == null) {
			//DontDestroyOnLoad (gameObject);
			station = this;
		} else if (station != this) {
			Destroy (gameObject);
		}
	}
}
