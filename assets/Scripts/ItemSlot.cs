using UnityEngine;
using System.Collections;

public class ItemSlot : MonoBehaviour {
	
	public int itemType = -1;
	
	private SpriteRenderer currentSpriteRenderer;
	
	// create a child object with a sprite renderer, and assign currentSpriteRenderer
	public void Start () {
		
	}
	
	// place a new sprite corresponding to resourceType. if resourceType is -1, make the sprite invisible
	public void UpdateSprite () {
		
	}
	
	// same as above, but overloaded with an arg for the new resource type
	public void UpdateSprite (int newType) {
		
	}
	
	// tell game manager that this slot just had the mouse released over it
	// via int : SetItemToSlot (int slot)
	public void OnMouseUp () {
		
	}
	
	// tell game manager that this slot was clicked
	// and stop displaying the sprite
	public void OnMouseDown () {
		
	}
}
