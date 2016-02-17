using UnityEngine;
using System.Collections;

public class RightClickGraphic : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Functions
	public void ActivateRightClick() {
		animator.SetBool ("RightClickActivated", true);
	}

	void DeActivateRightClick() {
		animator.SetBool ("RightClickActivated", false);
	}
}
