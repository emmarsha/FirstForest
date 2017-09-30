using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIntoArea : MonoBehaviour {

    public float moveSpeed = 2;
    
    // the starting position for the character when entering a new area
    public GameObject portalExit;
    // the direction the player should be facing while walk animation plays
    public Vector2 moveDirection;
    // the spot the character should walk to
    public Transform walkPosition;
    // 
    public string pointName;

    //public bool isOutside;

    private Transform currentPos;
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentPos = walkPosition;
        player.allowUserInput = false;
        //GameManager.Instance.playerIsOutside = isOutside;
    }

    // Update is called once per frame
    void Update () {
        if (player.startPoint == pointName)
        {
            // Get characters current position
            Transform pos = player.getPlayerPosition();
            if (pos.position == walkPosition.position)
            {
                player.allowUserInput = true;
                portalExit.SetActive(true);
                Destroy(this);
                return;
            }

            // Move player towards the starting point
            player.transform.position = Vector3.MoveTowards(player.transform.position, currentPos.position, Time.deltaTime * moveSpeed);
            player.setMoveDirection(moveDirection);
            player.allowPlayerAnimation(true);
        }
    }
}
