using UnityEngine;
using System.Collections;

public class ItemPool : MonoBehaviour {
	
	public int itemType = -1;
	
	private GameManager gm;
	
	// create a child with a sprite corresponding to the itemType
	public void Start () {
		gm = Hub.central.gm;
		
		GameObject childSprite = new GameObject("PoolSprite");
		childSprite.transform.SetParent(transform);
		childSprite.transform.position = transform.position;
		SpriteRenderer image = childSprite.AddComponent<SpriteRenderer>();
		image.sprite = gm.GetSprite(itemType);
		image.sortingLayerID = SortingLayer.NameToID("Props-foreground");
	}
	
	// tell game manager that this pool was clicked
	// via ItemPickedUp(int type)
	public void OnMouseDown () {
		Hub.central.gm.ItemPickedUp(itemType);
	}
}
