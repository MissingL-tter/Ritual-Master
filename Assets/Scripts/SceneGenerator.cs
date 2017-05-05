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
	public Sprite pentaSprite;
	public Sprite historyBoxBG;
	public Sprite temp;
	
	// prefabs
	
	// references
	GameObject histBox;
	GameObject puzzleCircle;
	GameObject[] itemPool;
	public ItemSlot[] slots;
	public ItemPool[] pools;

	public GameObject lineFab;
	
	// private
	private float lastAspect;
	
	void Start () {
		//LoadAll(3,5);
	}

	// set up all objects
	public void LoadAll (int numSlots, int numTypes) {
		// values to use throughout the positioning process
		Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 1));
		Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 1));
		float w = topRight.x - bottomLeft.x;
		float h = topRight.y - bottomLeft.y;
		Vector3 center = (topRight + bottomLeft) * 0.5f;
		slots = new ItemSlot[numSlots];
		pools = new ItemPool[numTypes];
		
		// create history box placeholder, and set position and scale
		GameObject histBox = new GameObject("HistoryWindow");
		histBox.AddComponent<SpriteRenderer>().sprite = historyBoxBG;
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
		HistoryV2 histv2 = histBox.AddComponent<HistoryV2> ();
		histv2.Setup ();
		// give the game manager a reference to the history manager
		Hub.central.gm.historyManager = histv2;
		
		// create the outline of the puzzle, set position and scale
		// sorting layer: semi-background
		GameObject puzzleCircle = new GameObject("Puzzle Circle");
		puzzleCircle.AddComponent<SpriteRenderer>().sprite = pentaSprite;
		puzzleCircle.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Semi-background");
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
		
		// put slots around the circumference of the puzzle circle
		// sorting layer: props
		float delta = -Mathf.PI * 2.0f / numSlots;
		float rad;
		GameObject slotGo;
		for (int i=0; i<numSlots; i++) {
			rad = (delta*i) + (0.5f*Mathf.PI);
			newPos = new Vector3(Mathf.Cos(rad),Mathf.Sin(rad),1) * (puzzleRadiusPercent*h);
			newPos += puzzleCircle.transform.position;
			slotGo = new GameObject("Slot"+i);
			slotGo.AddComponent<SpriteRenderer>().sprite = itemSlotSprite;
			slotGo.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Props");
			slotGo.AddComponent<CircleCollider2D>();
			slots[i] = slotGo.AddComponent<ItemSlot>();
			slots[i].slotID = i;
			slotGo.transform.position = newPos;
		}

		//create star pattern
		Vector2 location = new Vector2();
		Vector2 dir = new Vector2();
		GameObject line = new GameObject();
		int slotMod = (int)Mathf.Round(Mathf.Sqrt(numSlots));
		for (int i=0; i<numSlots; i++) {
			location.x = (slots[i].transform.position.x+slots[(i+slotMod)%numSlots].transform.position.x)/2;
			location.y = (slots[i].transform.position.y+slots[(i+slotMod)%numSlots].transform.position.y)/2;

			dir = (slots[(i+slotMod)%numSlots].transform.position - slots[i].transform.position).normalized;

			float rot = Mathf.Atan2(dir.y, dir.x);

			line = (GameObject)Instantiate(lineFab, location, Quaternion.identity);

			line.transform.Rotate(Vector3.forward, rot * Mathf.Rad2Deg);

			line.transform.localScale = new Vector3(.6f, .6f, .6f);

			//line.transform.rotation = rot;

			//line.transform.

			

			

			//Debug.DrawLine(slots[i].transform.position, slots[(i+2)%numSlots].transform.position, Color.red,1000,false); 

		}
		
		// create and set pool items below puzzle circle
		// sorting layer: props (slots)
		itemPool = new GameObject[numTypes];
		float totalHorizSpacing = w*(1-leftSideHorizontalPercentage) - (numTypes*h*poolItemRadius*2);
		float inspace = totalHorizSpacing / (numTypes+3);
		// x pos: 2*s + 2*r*i + s*i + r
		for (int i=0; i<numTypes; i++) {
			itemPool[i] = new GameObject("Pool"+i);
			itemPool[i].AddComponent<SpriteRenderer>().sprite = itemSlotSprite;
			itemPool[i].GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Props");
			itemPool[i].AddComponent<CircleCollider2D>();
			pools[i] = itemPool[i].AddComponent<ItemPool>();
			pools[i].itemType = i;
			newPos = new Vector3(
				center.x + (2*inspace) + (2*poolItemRadius*h*i) + (inspace*i) + (poolItemRadius*h),
				bottomLeft.y + poolBottomPadding*h + poolItemRadius*h,
				1);
			itemPool[i].transform.position = newPos;
		}
		
		lastAspect = Camera.main.aspect;
		
	}
	
	// arrange all objects on screen dynamically
	// depends on current screen orientation and resolution
	public void AutoArrange () {
		if (lastAspect == Camera.main.aspect) return;
		
		lastAspect = Camera.main.aspect;
	}
}
