using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HistoryManager : MonoBehaviour {
	
	public Transform[] sprites;
	public RectTransform scrollContent;
	public Transform scrollView;
	public Transform historyItemPrefab;
	
	private Transform historyItem;
	private Vector3 childPosition;
	private Vector3 firstChildPosition;
	private Rect tempRect;
	
	// add a history item for a guess to the chat window
	public void Start () {
		scrollView.GetComponent<ScrollRect>().horizontal = false;
	}
	
	public void AddHistoryItem () {
		historyItem = Instantiate(historyItemPrefab);
		if (scrollContent.childCount > 0){
			childPosition = scrollContent.GetChild(scrollContent.childCount - 1).transform.position;
			historyItem.SetParent(scrollContent,false);
			historyItem.transform.position = new Vector3 (childPosition.x, childPosition.y - 1f, historyItem.transform.position.z);
		}
		else {
			historyItem.SetParent(scrollContent,false);
			// set contect rect to include all elements
			/*childPosition = scrollContent.GetChild(scrollContent.childCount - 1).transform.position;
			firstChildPosition = scrollContent.GetChild(0).transform.position;
			tempRect = scrollContent.rect;
			tempRect.height = Vector3.Distance(childPosition,firstChildPosition);
			scrollContent.sizeDelta = new Vector2(tempRect.width, tempRect.height);*/
		}
	}
	
}
