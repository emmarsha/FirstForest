using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStartPoint : MonoBehaviour {

    public Vector2 startDirection;
    public string pointName;

    private PlayerController player;
    private CameraController theCamera;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();

        if (player.startPoint == pointName)
        {
            player.transform.position = transform.position;
            GameManager.instance.updateOnSceneTransition();

           theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
           theCamera.shouldFollowPlayer = true;
        }
    }
}
