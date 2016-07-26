using UnityEngine;
using System.Collections;

public class WindowScroller : MonoBehaviour {
	
	public Transform historyCamera;
	public Transform elementParent;
	
	private HistoryManager historyManager;
	private Vector3 lastMousePosition;
	private float maxCameraHeight;
	private float cameraHalfViewportHeight;
	
	void Start () {
		maxCameraHeight = historyCamera.position.y;
		Vector3 viewportBottom = historyCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f,0,0));
		Vector3 viewportTop = historyCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f,1,0));
		cameraHalfViewportHeight = Vector3.Distance(viewportTop,viewportBottom) * 0.5f;
		historyManager = historyCamera.GetComponent<HistoryManager>();
	}

	void OnMouseDown () {
		lastMousePosition = Input.mousePosition;
	}
	
	void OnMouseDrag () {
		Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition)
			- Camera.main.ScreenToWorldPoint(lastMousePosition);
		delta.x = 0;
		Vector3 newPosition = historyCamera.position;
		newPosition += -delta;
		
		Vector3 lowestPoint = historyManager.GetLowestPoint();
		float minCameraHeight = lowestPoint.y + cameraHalfViewportHeight;
		
		if (newPosition.y < minCameraHeight) {
			newPosition.y = minCameraHeight;
		}
		
		if (newPosition.y >= maxCameraHeight) {
			newPosition.y = maxCameraHeight;
		}
		
		historyCamera.position = newPosition;
		lastMousePosition = Input.mousePosition;
	}
	
	void OnMouseOver () {
		Vector3 newPosition = historyCamera.position;
		newPosition.y += Input.mouseScrollDelta.y;
		
		Vector3 lowestPoint = historyManager.GetLowestPoint();
		float minCameraHeight = lowestPoint.y + cameraHalfViewportHeight;
		
		if (newPosition.y < minCameraHeight) {
			newPosition.y = minCameraHeight;
		}
		
		if (newPosition.y >= maxCameraHeight) {
			newPosition.y = maxCameraHeight;
		}
		historyCamera.position = newPosition;
	}
}
