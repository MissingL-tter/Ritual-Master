using UnityEngine;
using System.Collections;

public class HistoryDragger : MonoBehaviour {

	private HistoryPane pane;

	// Use this for initialization
	void Start () {
		pane = Hub.central.gm.histPane;
	}
	
	void OnMouseDown () {
		pane.GetMouseDown ();
	}

	void OnMouseDrag () {
		pane.GetMouseDrag ();
	}
}
