using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	
	private AudioSource audioSource;
	public AudioClip piecePlaceSound;
	public AudioClip piecePickUpSound;
	public AudioClip puzzleFailureSound;
	
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		Hub.central.gm.soundController = this;
	}
	
	void Update () {
	
	}
	
	// Picking up and Placing Pieces within MasterMind Puzzle
	public void PlayPickUpResourceSound () {
		audioSource.PlayOneShot(piecePickUpSound, 0.65f);
	}
	
	public void PlayPlaceResourceSound () {
        audioSource.PlayOneShot(piecePlaceSound, 0.65f);
    }
    
    // Fail/Win puzzle
    public void PlayPuzzleFailureSound () {
		audioSource.PlayOneShot(puzzleFailureSound, 0.6f);
	}
	
}

