using UnityEngine;
using System.Collections;

public class ItemSlot : MonoBehaviour {
	
	public int itemType = -1;
	public int slotID = -1;
	
	public GameObject childSprite;
	public SpriteRenderer childSpriteRenderer;
	
	private GameManager gm;
	
	// create a child object with a sprite renderer, and assign currentSpriteRenderer
	public void Start () {
		gm = Hub.central.gm;
		
		childSprite = new GameObject("slotSprite");
		childSpriteRenderer = childSprite.AddComponent<SpriteRenderer>();
		childSprite.transform.SetParent(transform);
		childSprite.transform.position = transform.position;
		childSpriteRenderer.sortingLayerID = SortingLayer.NameToID("Props-foreground");
	}
	
	// just a wrapper
	public void RemoveSprite () {
		UpdateSprite(-1);
	}
	
	// place a new sprite corresponding to resourceType. if resourceType is -1, make the sprite invisible
	public void UpdateSprite () {
		if (itemType >= 0) {
			childSpriteRenderer.enabled = true;
			childSpriteRenderer.sprite = gm.GetSprite(itemType);
		}
		else {
			childSprite.GetComponent<SpriteRenderer>().sprite = null;
			childSpriteRenderer.enabled = false;
		}
	}
	
	// same as above, but overloaded with an arg for the new resource type
	public void UpdateSprite (int newType) {
		itemType = newType;
		UpdateSprite();
	}
	
	// tell game manager that this slot just had the mouse released over it
	// via int : SetItemToSlot (int slot)
	public void OnMouseOver () {
		if (Input.GetMouseButtonUp(0)) {
			gm.SetItemToSlot (slotID);
		}
	}
	
	// tell game manager that this slot was clicked
	// and stop displaying the sprite
	public void OnMouseDown () {
		if (IsEmpty()) {
			Debug.Log("Can't pick up an item from an empty slot");
			return;
		}
		gm.ItemPickedUp(itemType,slotID);
		RemoveSprite();
	}
	
	// returns true if empty, false otherwise
	public bool IsEmpty () {
		if (!childSpriteRenderer.sprite) return true;
		else return false;
	}
	
}
