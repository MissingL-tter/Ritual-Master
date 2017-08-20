using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSockets : MonoBehaviour {

	public GameObject socket;
	public GameObject line;

	public int numSockets = 5;

	public GameObject[] ritualSockets;

	// Use this for initialization
	void Start () {

		ritualSockets = new GameObject[numSockets];
		
		// Place sockets around the circle
		Vector2 pos;
		Vector2 center = transform.position;
		float radius = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
		for (int i = 0; i < numSockets; i++) {
			float angle = (360 / numSockets) * i + 180;
			pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
			pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
			ritualSockets[i] = Instantiate(socket, pos, Quaternion.identity);
		}

		// Places lines between sockets to construct a star
		Vector2 dir = new Vector2();
		int slotMod = (int)Mathf.Round(Mathf.Sqrt(numSockets));
		for (int i = 0; i < numSockets; i++) {
			pos.x = (ritualSockets[i].transform.position.x + ritualSockets[(i + slotMod) % numSockets].transform.position.x) / 2;
			pos.y = (ritualSockets[i].transform.position.y + ritualSockets[(i + slotMod) % numSockets].transform.position.y) / 2;

			dir = (ritualSockets[(i + slotMod) % numSockets].transform.position - ritualSockets[i].transform.position).normalized;

			float rot = Mathf.Atan2(dir.y, dir.x);
			Instantiate(line, pos, Quaternion.Euler(0, 0, rot * Mathf.Rad2Deg));
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
