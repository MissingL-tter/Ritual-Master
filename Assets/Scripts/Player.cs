using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;
    public GameObject heldResource;
    public SoundController AudioPlayer;

    //Awake is always called before any Start functions
    void Awake () {

        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0)) {
            // Remove the resource from its socket
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, LayerMask.GetMask("Resource"));
            if (hit) {
                TakeResource(hit.collider.gameObject);
                AudioPlayer.PlayPickUpPiece();
            }
        }

        if (heldResource != null) {
            // Move the resource with the pointer
            heldResource.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector2(-.25f, .25f);
        }

        if (Input.GetMouseButtonUp(0) && heldResource != null) {
            // Raycast only for RitualSockets, if we find a socket then give the this resource to that socket
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, LayerMask.GetMask("RitualSocket"));
            if (hit) {
                DropResourceInto(hit.collider.GetComponent<Socket>());
                AudioPlayer.PlayPutdownPiece();
            } else {
                Destroy(heldResource);
                heldResource = null;
            }
        }

    }

    void TakeResource (GameObject resource) {
        heldResource = resource;
        heldResource.transform.position = Input.mousePosition;
        heldResource.transform.parent.GetComponent<Socket>().resource = null;
        heldResource.transform.parent = null;
    }

    void DropResourceInto (Socket socket) {
        if (socket.resource != null) {
            Destroy(socket.resource);
        }
        socket.resource = heldResource;
        heldResource.transform.position = socket.transform.position;
        heldResource.transform.parent = socket.transform;
        heldResource = null;
    }
}
