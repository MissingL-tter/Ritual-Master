using UnityEngine;
using System.Collections;

public class Hub : MonoBehaviour {
	
	public GameManager gm;
	public Resources resources;
	
	public static Hub central;
	
	void Awake () {
		if (!central) {
			central = this;
		}
		else if (central != this) {
			Destroy(gameObject);
		}
	}
	
	public void Start () {
		// since Hub should be attached to the same object as GM...
		if (!gm) {
			gm = gameObject.GetComponent<GameManager>();
			if (!gm) {
				Trace.Msg("Hub found no game manager!");
			}
		}
		if (!resources) {
			resources = gameObject.GetComponent<Resources> ();
			if (!resources) {
				Trace.Msg ("Hub found no resource manager!");
			}
		}
	}
}
