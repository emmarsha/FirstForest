using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource[] musicTracks;
    public bool musicCanPlay;
    public bool fadeMusic = false;
    public int currentTrack;

    private static bool musicManExists;
    private float volume = 1.0f;

    // Use this for initialization
    void Start () {
        if (!musicManExists)
        {
            musicManExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(musicCanPlay)
        {
            if (!musicTracks[currentTrack].isPlaying)
            {
                musicTracks[currentTrack].Play();
            }
        } else
        {
            musicTracks[currentTrack].Stop();
        }

        if(fadeMusic)
        {
            fadeAudioOut();
        }
    }

    public void fadeAudioOut()
    {
        if (musicTracks[currentTrack].volume > 0.0)
        {
            volume -= 0.1f * Time.deltaTime * 3.5f;
            musicTracks[currentTrack].volume = volume;
        } else
        {
            Debug.Log("complete");
            fadeMusic = false;
            musicCanPlay = false;
        }
    }

    public void switchTrack(int newTrack)
    {
        musicCanPlay = true;

        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].volume = 1;
        musicTracks[currentTrack].Play();
    }
}
