using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public AudioSource backgroundAudio;
    public AudioSource pickupAudio;
    public AudioSource putdownAudio;
    
    public void PlayPickUpPiece () {
        pickupAudio.Play();
    }

    public void PlayPutdownPiece () {
        putdownAudio.Play();
    }
    
}