using UnityEngine;
using System.Collections;

public class SceneGenerator : MonoBehaviour {
	
	// layout settings
	// left side
	public float leftSideHorizontalPercentage = 0.5f; // * width
	public float historyBoxSidePadding = 0.05f; // * width
	public float historyBoxTopPadding = 0.1f; // * height
	public float historyBoxBottomPadding = 0.35f; // * height
	// right side
	public float puzzleRadiusPercent = 0.25f; // * height
	public float puzzleTopPadding = 0.1f; // * height
	public float puzzleBottomPadding = 0.4f; // * height
	public float poolTopPadding = 0.125f; // * height
	public float poolBottomPadding = 0.125f; // * height
	public float poolItemRadius = 0.075f; // * height
	
	// textures and sprites
	public Sprite itemSlotSprite;
	public Sprite temp;
	
	// prefabs
	
	// in-scene references
	GameObject histBox;
	GameObject puzzleCircle;
	GameObject[] itemPool;
	
	void Start () {
		LoadAll(5);
	}

	// set up all objects
	void LoadAll (int numTypes) {
		// values to use throughout the positioning process
		Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 1));
		Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 1));
		float w = topRight.x - bottomLeft.x;
		float h = topRight.y - bottomLeft.y;
		Vector3 center = (topRight + bottomLeft) * 0.5f;
		
		// create history box placeholder, and set position and scale
		GameObject histBox = new GameObject("History Box");
		histBox.AddComponent<SpriteRenderer>().sprite = temp;
		Vector3 newScale = new Vector3(
			(leftSideHorizontalPercentage - (2*historyBoxSidePadding)) * w,
			(1 - historyBoxTopPadding - historyBoxBottomPadding) * h,
			1);
		Vector3 newPos = new Vector3(
			bottomLeft.x + (historyBoxSidePadding*w) + (newScale.x*0.5f),
			bottomLeft.y + (historyBoxBottomPadding*h) + (newScale.y*0.5f),
			1);
		histBox.transform.position = newPos;
		histBox.transform.localScale = newScale;
		
		// create the outline of the puzzle, set position and scale
		GameObject puzzleCircle = new GameObject("Puzzle Circle");
		puzzleCircle.AddComponent<SpriteRenderer>().sprite = temp;
		newScale = new Vector3 (
			puzzleRadiusPercent * 2 * h,
			puzzleRadiusPercent * 2 * h,
			1);
		newPos = new Vector3 (
			topRight.x - (((1-leftSideHorizontalPercentage)*w)/2.0f),
			topRight.y - ((puzzleTopPadding*h)+(newScale.y*0.5f)),
			1);
		puzzleCircle.transform.position = newPos;
		puzzleCircle.transform.localScale = newScale;
		
		// create and set pool items below puzzle circle
		itemPool = new GameObject[numTypes];
		float totalHorizSpacing = w*(1-leftSideHorizontalPercentage) - (numTypes*h*poolItemRadius*2);
		float inspace = totalHorizSpacing / (numTypes+3);
		// x pos: 2*s + 2*r*i + s*i + r
		for (int i=0; i<numTypes; i++) {
			itemPool[i] = new GameObject("Pool"+i);
			itemPool[i].AddComponent<SpriteRenderer>().sprite = itemSlotSprite;
			itemPool[i].AddComponent<CircleCollider2D>();
			//itemPool[i].AddComponent<ItemPool>().itemType = i;
			newPos = new Vector3(
				center.x + (2*inspace) + (2*poolItemRadius*h*i) + (inspace*i) + (poolItemRadius*h),
				bottomLeft.y + poolBottomPadding*h + poolItemRadius*h,
				1);
			itemPool[i].transform.position = newPos;
		}
		
	}
	
	// arrange all objects on screen dynamically
	// depends on current screen orientation and resolution
	void AutoArrange () {
		
	}
}
