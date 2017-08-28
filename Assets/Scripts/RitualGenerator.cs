using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualGenerator : MonoBehaviour {

    GameManager gameManager;

    public GameObject socket;
    public GameObject line;
    public GameObject[] ritualSockets;
    public int numSockets = 5;
    public int[] solution;

    // Use this for initialization
    void Start () {

        gameManager = GameManager.instance;

        // Create Array of Sockets
        ritualSockets = new GameObject[numSockets];

        // Place sockets around the circle
        Vector2 pos;
        Vector2 center = transform.position;
        float angle;
        float radius = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
        for (int i = 0; i < numSockets; i++) {
            angle = (360f / numSockets) * i + 180f;
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

            Instantiate(line, pos, Quaternion.Euler(0, 0, angle)).transform.parent = gameObject.transform;
        }

        // If no solution exists then generate a new one
        if (solution.Length == 0) {
            solution = new int[numSockets];
            for (int i = 0; i < solution.Length; i++){
                solution[i] = Random.Range(0, gameManager.resourceTypes.Length);
            }
            GetComponent<RitualManager>().solution = solution;
        }

    }

    // Update is called once per frame
    void Update () {

    }
}
