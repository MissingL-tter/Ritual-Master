using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	// public variables
	
	// vars to set in inspector
	public ItemSlot[] slots;
	public ItemPool[] pools;
	public Sprite[] itemSprites;
	
	private CodeManager codeManager;

	public void Start () {
		codeManager = new CodeManager();
		codeManager.GenerateCode();
		for (int i=0;i<slots.Length;i++) {
			slots[i].slotID = i;
		}
	}
	
	// called when the user attempts the ritual
	public void AttemptRitual () {
		
	}
	
	// called when an item pool is clicked
	// make sure to handle itemType = -1 (which means something was set up wrong)
	public void ItemPickedUp (int itemType) {
		
	}
	
	// slots will call this when they get a MouseUp
	// return int type : type of resource dropped
	public int SetItemToSlot (int slot) {
		return 0; // change this
	}
	
	// return the sprite with passed type
	public Sprite GetSprite (int type) {
		return null; // change this
	}
}
