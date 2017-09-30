using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour {

    public List<PlayableDirector> playablesList;
    public List<TimelineAsset> timelines;

    public void Play()
    {
        foreach (PlayableDirector playableDirector in playablesList) {
            playableDirector.Play();
        }
    }

    public void playFromTimeline(int index)
    {
        TimelineAsset selectedAsset;
        if (timelines.Count <= index)
        {
            selectedAsset = timelines[timelines.Count - 1];
        } else
        {
            selectedAsset = timelines[index];
        }
        playablesList[0].Play(selectedAsset);
    }
}
