using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;
    public string loadPoint;

    private SFXManager sfxManager;
    private PlayerController player;

    // Use this for initialization
    void Start () {
        sfxManager = FindObjectOfType<SFXManager>();
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(gameObject.name == "Exit")
            {
                sfxManager.openDoor.Play();
            }

            player.startPoint = loadPoint;
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
