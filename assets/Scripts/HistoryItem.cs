using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HistoryItem : MonoBehaviour {

	public GameManager gm;
	public Transform graphicCenter;
	public Text responseText;
	public float starRadius = 0.5f;
	public float elementSize = 0.5f;
	
	void Start () {
		if (!gm) {
			gm = Hub.central.gm;
		}
		
		// test
		Init(new int[] {0,1,2,1,0},3,2);
	}
	
	// create ring of icons representing a guess
	public void Init (int[] guess, int full, int partial) {
		Vector3 center = graphicCenter.localPosition;
		float delta = -Mathf.PI * 0.4f; // one-fifth of full circle in rads
		for (int i=0;i<5;i++) {
			float rad = (delta*i) + (0.5f*Mathf.PI);
			Vector3 instPosition = new Vector3(Mathf.Cos(rad),Mathf.Sin(rad),0) * starRadius;
			// create new empty gameObject at instPosition
			// add sprite renderer
			// add appropriate sprite based on guess[i]
			GameObject go = new GameObject("HistoryGraphicElement");
			SpriteRenderer spriteTemp = go.AddComponent<SpriteRenderer>();
			spriteTemp.sprite = gm.GetSprite(guess[i]);
			spriteTemp.sortingLayerID = SortingLayer.NameToID("Props-foreground");
			go.transform.position = graphicCenter.position + instPosition;
			go.transform.localScale = Vector3.one * elementSize;
			go.transform.SetParent(transform);
		}
		
		responseText.text = full + " Full Matches\n" + partial + " Partial Matches";
	}

}
