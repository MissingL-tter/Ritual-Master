﻿using UnityEngine;
using System.Collections;

public class ItemSlot : MonoBehaviour {
	
	public int itemType = -1;
	public int slotID = -1;
	
	private SpriteRenderer currentSpriteRenderer;
	private GameManager gm;
	
	public void Awake () {
		gm = Hub.central.gm;
	}
	
	// create a child object with a sprite renderer, and assign currentSpriteRenderer
	public void Start () {
		GameObject childSprite = new GameObject("slotSprite");
		currentSpriteRenderer = childSprite.AddComponent<SpriteRenderer>();
		childSprite.transform.SetParent(transform);
		childSprite.transform.position = transform.position;
	}
	
	// place a new sprite corresponding to resourceType. if resourceType is -1, make the sprite invisible
	public void UpdateSprite () {
		if (itemType >= 0) {
			currentSpriteRenderer.enabled = true;
			currentSpriteRenderer.sprite = gm.GetSprite(itemType);
		}
		else {
			currentSpriteRenderer.enabled = false;
		}
	}
	
	// same as above, but overloaded with an arg for the new resource type
	public void UpdateSprite (int newType) {
		itemType = newType;
		UpdateSprite();
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
