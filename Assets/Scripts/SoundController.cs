using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip pickupPiece;
	public AudioClip putdownPiece;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayPickUpPiece () {
        audioSource.PlayOneShot(pickupPiece, 0.55f);
    }

    public void PlayPutdownPiece () {
        audioSource.PlayOneShot(putdownPiece, 0.55f);
    }
}