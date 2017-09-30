using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class TimelineDialogueController : MonoBehaviour {

    public string text;
    public GameObject dBox;
    public Text dText;

    private SFXManager sfxManager;
    private TimelineController timelineController;

    private float letterPause = 0.05f;

    public TimelineAsset timeline;


    // Use this for initialization
    void Start()
    {
        dBox = FindObjectOfType<GameObject>();
        sfxManager = FindObjectOfType<SFXManager>();
        timelineController = FindObjectOfType<TimelineController>();

        dText.text = "";
        StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string dialogue)
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            dText.text += letter;
            sfxManager.textRead.Play();
            yield return new WaitForSeconds(letterPause);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Debug.Log("input z");
            timelineController.playFromTimeline(1);
        }
           
    }
}
