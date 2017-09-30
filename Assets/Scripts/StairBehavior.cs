using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBehavior : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Slow the speed of the player 
            player.moveSpeed = 1.5f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Reset the speed of the player 
            player.moveSpeed = 4;
        }
    }
}
