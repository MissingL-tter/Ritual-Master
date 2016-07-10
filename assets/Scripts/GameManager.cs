﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	// public variables
	// doesn't need to be serialized because the sound manager gives this script a hook at runtime
	[System.NonSerialized]
	public SoundController soundController;
	
	// vars to set in inspector
	public int numSlots = 5;
	public int numTypes = 3;
	[System.NonSerialized]
	public ItemSlot[] slots;
	[System.NonSerialized]
	public ItemPool[] pools;
	public Sprite[] itemSprites;
	// keep items in slots so you don't have to fill slots each time
	public bool removeItemsOnAttempt = true;
	
	// pointers to other objects
	private CodeManager codeManager;
	public HistoryManager historyManager;
	public SceneGenerator sceneGenerator;
	
	// tracks the type of item that's being dragged. -1 if nothing, [0..ItemPool.length-1] otherwise
	[System.NonSerialized]
	public int currentDraggedType = -1;
	
	// pointer to currently dragged item
	private DraggedItem draggedItem;
	
	// generate code and create the scene
	public void Start () {
		codeManager = new CodeManager();
		codeManager.GenerateCode();
		sceneGenerator.LoadAll(numSlots,numTypes);
		slots = sceneGenerator.slots;
		pools = sceneGenerator.pools;
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
		
		// short circuit if not all slots are full
		foreach (ItemSlot s in slots) {
			if (s.IsEmpty()) {
				Debug.Log("Not all slots filled!");
				// inform the user that all slots must be filled somehow
				return;
			}
			// we already checked if slot was empty, so now we can empty it
			if (removeItemsOnAttempt) s.RemoveSprite();
		}
		
		int[] result = codeManager.EvaluateGuess(guess);
		// result[0] is full, result[1] is partial
		Debug.Log(""+ result[0] + " full and " + result[1] + " partial");
		if (result[0] < slots.Length) {
			// play failure sound
			soundController.PlayPuzzleFailureSound();
			//CreateHistoryItem();
			historyManager.AddHistoryItem();
		}
		else {
			// play success sound
		}
	}
	
	// overload for calling ItemPickedUp without a slot id
	public void ItemPickedUp (int itemType) {
		ItemPickedUp(itemType,-1);
	}
	
	// called when an item pool is clicked
	// make sure to handle itemType = -1 (which means something was set up wrong)
	// can be called directly by an ItemSlot or indirectly by everything else via ItemPickedUp(itemType)
	// returns the DraggedItem that was created
	public void ItemPickedUp (int itemType, int slotID) {
		if (itemType < 0) {
			Debug.Log("ItemPickedUp() got a number less than 0! Not a valid resource");
			return;
		}
		currentDraggedType = itemType;
		
		// create a new game object with DraggedItem
		GameObject draggable = new GameObject("DraggedItem");
		draggable.AddComponent<SpriteRenderer>().sprite = GetSprite(itemType);
		draggedItem = draggable.AddComponent<DraggedItem>();
		draggedItem.originalSlot = slotID;
		
		// play picked up sound
		soundController.PlayPickUpResourceSound();
	}
	
	// slots will call this when they get a MouseUp=
	public void SetItemToSlot (int slot) {
		if (currentDraggedType == -1) {
			Debug.Log("SetItemToSlot was called with currentDraggedType = -1!");
			return;
		}
		if (draggedItem && draggedItem.originalSlot != -1) {
			slots[draggedItem.originalSlot].UpdateSprite(slots[slot].itemType);
		}
		slots[slot].UpdateSprite(currentDraggedType);
		
		currentDraggedType = -1;
		
		// play set sound
		soundController.PlayPlaceResourceSound();
		
		Destroy(draggedItem.gameObject);
		draggedItem = null;
	}
	
	// return the sprite for passed type
	public Sprite GetSprite (int type) {
		return itemSprites[type];
	}
}
