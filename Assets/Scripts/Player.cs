using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;
    GameManager gameManager;
    SoundController soundController;

    public GameObject heldResource;

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
        gameManager = GameManager.instance;
        soundController = gameManager.soundController;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit2D hitResource = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, LayerMask.GetMask("Resource"));

            // Remove the resource from its socket
            if (hitResource) {
                TakeResource(hitResource.collider.gameObject);
                soundController.PlayPickUpPiece();
            }
        }

        if (Input.GetMouseButtonUp(0)) {

            if (heldResource != null) {
                // Raycast only for RitualSockets, if we find a socket then give the this resource to that socket otherwise destroy it
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, LayerMask.GetMask("RitualSocket"));
                if (hit) {
                    DropResourceInto(hit.collider.GetComponent<Socket>());
                    soundController.PlayPutdownPiece();
                } else {
                    Destroy(heldResource);
                    heldResource = null;
                }
            }
        }

        // Move the resource with the pointer
        if (heldResource != null) {
            // Lerp will suffice for touch input with a drag and drop
            heldResource.transform.position = Vector2.Lerp((Vector2) heldResource.transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), .25f);
        }
    }

    void TakeResource (GameObject resource) {
        heldResource = resource;
        heldResource.GetComponent<SpriteRenderer>().sortingOrder = 1;
        heldResource.transform.parent.GetComponent<Socket>().resource = null;
        heldResource.transform.parent = transform;
    }

    void DropResourceInto (Socket socket) {
        if (socket.resource != null) {
            Destroy(socket.resource);
        }
        socket.resource = heldResource;
        socket.resource.GetComponent<SpriteRenderer>().sortingOrder = 0;
        heldResource.transform.position = socket.transform.position;
        heldResource.transform.parent = socket.transform;
        heldResource = null;
    }
    
}
