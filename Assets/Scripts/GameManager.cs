using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isDaytime;
    public bool playerIsOutside;

    private SFXManager sfxManager;
    private MusicManager musicManager;
    //private PlayerController player;

    private static bool managerExists;

    static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject manager = new GameObject("[GameManager]");
                _instance = manager.AddComponent<GameManager>();
                DontDestroyOnLoad(manager);
            }
            return _instance;
        }
    }

    // Use this for initialization
    void Awake () {
        sfxManager = FindObjectOfType<SFXManager>();
        musicManager = FindObjectOfType<MusicManager>();
        //player = FindObjectOfType<PlayerController>();
        //playableControl = FindObjectOfType<TimelineController>();
    }

    void Start()
    {
        isDaytime = true;
        playerIsOutside = true;
    }

    public void setDaytimeSettings()
    {
        StartCoroutine(daytimeRoutine());
    }

    public void setNighttimeSettings()
    {
        StartCoroutine(nighttimeRoutine());
    }

    public void updateOnSceneTransition()
    {
        Debug.Log("updating scene");
        // Manage camera and music on scene change
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "One Perfect Village House")
        {
            musicManager.switchTrack(1);
            playerIsOutside = false;
        }
        else
        {
            // Overworld stuff here
            if (isDaytime)
            {
                musicManager.switchTrack(3);
            }
            else
            {
                musicManager.switchTrack(2);
            }
            playerIsOutside = true;
        }
        Debug.Log("isDaytime: " + isDaytime);
        Debug.Log("isOutside: " + playerIsOutside);
    }

    IEnumerator nighttimeRoutine()
    {
        Debug.Log("setting nighttime routine");
        isDaytime = false;
        musicManager.fadeMusic = true;
        yield return new WaitForSeconds(4f);
        sfxManager.wolfHowl.Play();
        yield return new WaitForSeconds(2f);
        musicManager.switchTrack(2);
    }

    IEnumerator daytimeRoutine()
    {
        Debug.Log("setting daytime routine");
        isDaytime = true;
        musicManager.fadeMusic = true;
        yield return new WaitForSeconds(2f);
        sfxManager.rooster.Play();
        yield return new WaitForSeconds(1f);
        musicManager.switchTrack(3);
    }
}
