using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualGenerator : MonoBehaviour {

    GameManager gameManager;

    public GameObject ritualSocketPrefab;
    public GameObject linePrefab;
    public GameObject[] ritualSockets;
    public int numSockets;
    public int[] solution;

    void Start () {

        gameManager = GameManager.instance;
        numSockets = gameManager.levelData.numSockets[gameManager.currentLevel];

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

            ritualSockets[i] = Instantiate(ritualSocketPrefab, pos, Quaternion.identity);
            ritualSockets[i].transform.parent = transform;
        }

        // Places lines between sockets to construct a star
        // Dynamically scales and rotates the lines for correct placement
        Vector2 dir;
        float lineLength = linePrefab.GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2;
        float x1, x2, y1, y2, dist;
        int slotMod = (int)Mathf.Round(Mathf.Sqrt(numSockets));
        for (int i = 0; i < numSockets; i++) {

            x1 = ritualSockets[i].transform.position.x;
            x2 = ritualSockets[(i + slotMod) % numSockets].transform.position.x;
            y1 = ritualSockets[i].transform.position.y;
            y2 = ritualSockets[(i + slotMod) % numSockets].transform.position.y;

            pos.x = (x1 + x2) / 2;
            pos.y = (y1 + y2) / 2;

            dist = Mathf.Sqrt(Mathf.Pow((x1 - x2), 2) + Mathf.Pow((y1 - y2), 2));
            dir = (ritualSockets[(i + slotMod) % numSockets].transform.position - ritualSockets[i].transform.position).normalized;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            GameObject line = Instantiate(linePrefab, pos, Quaternion.Euler(0, 0, angle));
            line.transform.localScale = new Vector3(dist/lineLength, 1, 1);
            line.transform.parent = gameObject.transform;
        }

        // If no solution exists then generate a new one
        if (solution.Length == 0) {
            solution = new int[numSockets];
            for (int i = 0; i < solution.Length; i++){
                solution[i] = Random.Range(0, gameManager.resourceTypes.Length);
            }
        }
    }

    void Update () {

    }

}
