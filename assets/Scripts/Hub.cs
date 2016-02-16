using UnityEngine;
using System.Collections;

public class Hub : MonoBehaviour {
	
	public GameManager gm;
	
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
				Debug.Log("Hub found no game manager!");
			}
		}
	}
}
