using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualGenerator : MonoBehaviour {

	public GameObject socket;
	public GameObject line;
	public GameObject[] ritualSockets;
	public int numSockets = 5;

	// Use this for initialization
	void Start () {

		ritualSockets = new GameObject[numSockets];
		
		// Place sockets around the circle
		Vector2 pos;
		Vector2 center = transform.position;
		float angle;
		float radius = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
		for (int i = 0; i < numSockets; i++) {
			angle = (360 / numSockets) * i;
			pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
			pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);

			ritualSockets[i] = Instantiate(socket, pos, Quaternion.identity);
			ritualSockets[i].transform.parent = transform;
		}

		// Places lines between sockets to construct a star
		Vector2 dir = new Vector2();
		int slotMod = (int)Mathf.Round(Mathf.Sqrt(numSockets));
		for (int i = 0; i < numSockets; i++) {
			pos.x = (ritualSockets[i].transform.position.x + ritualSockets[(i + slotMod) % numSockets].transform.position.x) / 2;
			pos.y = (ritualSockets[i].transform.position.y + ritualSockets[(i + slotMod) % numSockets].transform.position.y) / 2;
			dir = (ritualSockets[(i + slotMod) % numSockets].transform.position - ritualSockets[i].transform.position).normalized;
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

			Instantiate(line, pos, Quaternion.Euler(0, 0,angle)).transform.parent = gameObject.transform;
		}

		gameObject.GetComponent<RitualManager>().ritualSockets = ritualSockets;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
