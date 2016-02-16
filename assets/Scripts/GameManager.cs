using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	// public variables
	
	// vars to set in inspector
	public ItemSlot[] slots;
	public ItemPool[] pools;
	public Sprite[] itemSprites;
	
	private CodeManager codeManager;
	public int currentDraggedType = -1;

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
		if (itemType == -1) {
			Debug.Log("ItemPickedUp() got -1! An item type is set up wrong");
			return;
		}
		currentDraggedType = itemType;
		
		// create a new game object with DraggedItem
		GameObject draggable = new GameObject("DraggableItem");
		draggable.AddComponent<SpriteRenderer>().sprite = GetSprite(itemType);
		draggable.AddComponent<DraggedItem>();
	}
	
	// slots will call this when they get a MouseUp
	// return int type : type of resource dropped
	public void SetItemToSlot (int slot) {
		if (currentDraggedType == -1) {
			return;
		}
		slots[slot].UpdateSprite(currentDraggedType);
		
		currentDraggedType = -1;
	}
	
	// return the sprite with passed type
	public Sprite GetSprite (int type) {
		return itemSprites[type];
	}
}
