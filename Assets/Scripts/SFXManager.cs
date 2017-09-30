using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public AudioSource chest;
    public AudioSource textRead;
    public AudioSource textFinished;
    public AudioSource keyBounce;
    public AudioSource collision;
    public AudioSource walk;
    public AudioSource wade;
    public AudioSource openDoor;
    public AudioSource chickenCluck;
    public AudioSource rooster;
    public AudioSource wolfHowl;

    private static bool sfxManExists;

	// Use this for initialization
	void Start () {
		if(!sfxManExists)
        {
            sfxManExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
