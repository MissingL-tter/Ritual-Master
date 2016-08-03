using UnityEngine;
using System.Collections;

public class Resources : MonoBehaviour {

	public Material maskMaterial;
	public Material maskedSpriteMaterial;
	public Sprite maskSprite;
	public GameObject maskPrefab; 				// this is the mask
	[System.NonSerialized]
	public GameObject[] historyElementPrefab; 	// always masked
	public GameObject checkPrefab; 				// always masked
	public GameObject halfCheckPrefab; 			// always masked

	// Use this for initialization
	void Start () {
		// create history element prefabs
		if (!Hub.central.gm) {
			Trace.Msg ("Resources script doesn't have gm reference!");
			return;
		} else {
			GameManager gm = Hub.central.gm;
			historyElementPrefab = new GameObject[gm.numTypes];
			for (int i = 0; i < gm.numTypes; i++) {
				historyElementPrefab [i] = new GameObject ("HistoryElement");
				SpriteRenderer sprender = historyElementPrefab [i].AddComponent<SpriteRenderer> ();
				sprender.sprite = gm.GetSprite (i);
				sprender.material = maskedSpriteMaterial;
				historyElementPrefab [i].SetActive (false); // deactivate prefab
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
