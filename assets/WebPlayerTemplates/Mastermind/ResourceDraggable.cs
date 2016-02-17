using UnityEngine;
using System.Collections;

public class ResourceDraggable : MonoBehaviour {
	private bool isDragging;
	private bool canDrag = false;
	private Vector3 tempPosition;
	private MastermindManager manager;
    public int type;
    
	public int alreadyUsed = 0;
    private AudioSource audioSource;
    public AudioClip piecePickUpSound;
    public TryComboButton tryButton;
	
	void Start () {
		manager = GameObject.FindWithTag("MastermindManager").GetComponent<MastermindManager>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void CheckResources () {
		switch(type){
			case 0: if(ScriptHub.station.gameControlScript.BloodAmount - alreadyUsed > 0){canDrag = true;}else{canDrag=false;} break;
			
			case 1: if(ScriptHub.station.gameControlScript.IncenseAmount - alreadyUsed > 0){canDrag = true;}else{canDrag=false;} break;
			
			case 2: if(ScriptHub.station.gameControlScript.GoldAmount - alreadyUsed > 0){canDrag = true;}else{canDrag=false;} break;
			
			default: print("PLEASE FIX THE TYPE VARIABLE ON THE RESOURCE OBJECTS ASSHOLE"); break;
		}
	}
	
	void Update () {
		int[] used = tryButton.GetNumResourcesUsed();
		ScriptHub.station.GUIControlScript.tempSetResource(type,used[type]);
		alreadyUsed = used[type];
	}
	
	void OnMouseDown () {
		CheckResources();
		if (!canDrag) return;
		
		GameObject newDraggable = Instantiate (gameObject);
		newDraggable.name = gameObject.name;
		audioSource.PlayOneShot(piecePickUpSound, 0.65f);
            
	}
	
	void OnMouseDrag () {
		if (!canDrag) return;
		
		// get mouse position in screen coords and move sprite to it, retaining sprite's z position
		tempPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		tempPosition.z = transform.position.z;
		transform.position = tempPosition;
            
        
    }
	
	void OnMouseUp () {
        if(!canDrag) return;
        
        manager.currentResourceDraggable = type;
        // do raycast from mouse to see what was under it
        GetComponent<Collider2D> ().enabled = false;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D hit = Physics2D.GetRayIntersection (ray);
		if (hit.transform != null && hit.transform.tag == "ResourceSlot") {
			manager.currentResourceSlot = hit.transform.GetComponent<ResourceSlot> ().slotID;
			hit.transform.GetComponent<ResourceSlot> ().assignedResource = type;
			hit.transform.GetComponent<SpriteRenderer> ().sprite = manager.resourceSprites [type];
			ScriptHub.station.soundControlScript.playPlaceResourceSound();
        }
		
		Destroy (gameObject);
	}
}
