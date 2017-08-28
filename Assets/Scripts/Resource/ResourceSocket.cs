using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSocket : Socket {

    GameManager gameManager;
    Player player;

    public int resourceId = -1;

    void Start () {

        gameManager = GameManager.instance;
        player = Player.instance;

        // Load a resource into the socket
        resource = Instantiate(gameManager.GetResource(resourceId));
        resource.transform.position = transform.position;
        resource.transform.parent = transform;
    }

    void Update () {

        // If this socket has no resource and we are not holding one, create a new resource
        if (resource == null && player.heldResource == null) {
            resource = Instantiate(gameManager.GetResource(resourceId));
            resource.transform.position = transform.position;
            resource.transform.parent = transform;
        }
    }
}
