using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	// public variables
	public SoundController soundController;
	
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
		int[] guess = new int[] {
			slots[0].itemType,
			slots[1].itemType,
			slots[2].itemType,
			slots[3].itemType,
			slots[4].itemType
		};
		int[] result = codeManager.EvaluateGuess(guess);
		// result[0] is full, result[1] is partial
		Debug.Log(""+ result[0] + " full and " + result[1] + " partial");
		if (result[0] < slots.Length) {
			// play failure sound
			soundController.PlayPuzzleFailureSound();
		}
		else {
			// play success sound
		}
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
		
		// play picked up sound
		soundController.PlayPickUpResourceSound();
	}
	
	// slots will call this when they get a MouseUp
	// return int type : type of resource dropped
	public void SetItemToSlot (int slot) {
		if (currentDraggedType == -1) {
			return;
		}
		slots[slot].UpdateSprite(currentDraggedType);
		
		currentDraggedType = -1;
		
		// play set sound
		soundController.PlayPlaceResourceSound();
	}
	
	// return the sprite with passed type
	public Sprite GetSprite (int type) {
		return itemSprites[type];
	}
}
