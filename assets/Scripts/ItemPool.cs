using UnityEngine;
using System.Collections;

public class ItemPool : MonoBehaviour {
	
	public int itemType = -1;
	
	// create a child with a sprite corresponding to the itemType
	public void Start () {
		
	}
	
	// tell game manager that this pool was clicked
	// via ItemPickedUp(int type)
	public void OnMouseDown () {
		Hub.central.gm.ItemPickedUp(itemType);
	}
}
