using UnityEngine;
using System.Collections;

public class HistoryElement : MonoBehaviour {
	
	public float starRadius = 0.5f;
	public Sprite[] charSprites;
	public Sprite[] resourceSprites;
	public Transform numFullText;
	public Transform numPartialText;
	public Transform fullText;
	public Transform partialText;
	public Transform starCenter;
	
	public int[] guess;
	public int numFullCorrect;
	public int numPartialCorrect;
	
	void Awake () {
		//BuildHistoryBlock();
	}

	public void BuildHistoryBlock (int[] g, int full, int partial) {
		guess = g;
		numFullCorrect = full;
		numPartialCorrect = partial;
		BuildHistoryBlock();
	}
	
	void BuildHistoryBlock () {
		numFullText.GetComponent<SpriteRenderer>().sprite = charSprites[numFullCorrect];
		numPartialText.GetComponent<SpriteRenderer>().sprite = charSprites[numPartialCorrect];
		
		float zTemp = transform.position.z;
		Vector3 center = starCenter.localPosition;
		center.z = zTemp;
		float delta = -Mathf.PI * 0.4f; // one-fifth of full circle in rads
		for (int i=0;i<5;i++) {
			float rad = (delta*i) - (0.5f*Mathf.PI);
			Vector3 instPosition = new Vector3(Mathf.Cos(rad),Mathf.Sin(rad),0) * starRadius;
			// create new empty gameObject at instPosition
			// add sprite renderer
			// add appropriate sprite based on guess[i]
			GameObject go = new GameObject("HistoryStarElement");
			SpriteRenderer spriteTemp = go.AddComponent<SpriteRenderer>();
			spriteTemp.sprite = resourceSprites[guess[i]];
			go.layer = 11;
			go.transform.position = starCenter.position + instPosition;
			go.transform.localScale = Vector3.one * 0.5f;
			go.transform.parent = transform;
		}
	}
}
