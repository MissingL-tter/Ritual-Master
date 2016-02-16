using UnityEngine;
using System.Collections;

public class TryComboButton : MonoBehaviour {

	public CodeBreaker codeBreaker;
	
	private HistoryManager histManager;
	
	public ResourceSlot[] slot;

	public bool attemptUsed = false;

	public Sprite performText;
	public Sprite returnText;
	
	void Start () {
		histManager = GameObject.FindWithTag("HistoryManager").GetComponent<HistoryManager>();
	}

	void OnMouseDown () {
		int[] guesses = new int[] {
			slot [0].assignedResource,
			slot [1].assignedResource,
			slot [2].assignedResource,
			slot [3].assignedResource,
			slot [4].assignedResource
		};
		foreach (int g in guesses) {
			if (g == -1) {
				print("Attempting ritual with empty slots; aborted");
				return;
			}
		}
		codeBreaker.currentGuess = guesses;
		codeBreaker.crackTheCode ();
		int fullMatches = codeBreaker.fullMatches;
		int partialMatches = codeBreaker.partialMatches;
		histManager.AddHistoryItem (guesses, fullMatches, partialMatches);
		if (fullMatches != 5)
		{
			ScriptHub.station.soundControlScript.playPuzzleFailureSound();
		}
		WipeBoard();
	}
	
	public void WipeBoard () {
		// wipe board clean
		ScriptHub.station.GUIControlScript.SyncResources();
		for (int i=0;i<5;i++) {
			slot[i].assignedResource = -1;
			slot[i].transform.GetComponent<SpriteRenderer>().sprite = null;
		}
	}
	
	public int[] GetNumResourcesUsed () {
		int[] retval = new int[3];
		int resTypeTemp;
		for (int i=0;i<5;i++) {
			resTypeTemp = slot[i].assignedResource;
			if (resTypeTemp >= 0) {
				retval[resTypeTemp]++;
			}
		}
		return retval;
	}
}
