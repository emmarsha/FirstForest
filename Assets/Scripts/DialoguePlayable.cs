using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Timeline;


[System.Serializable]
public class DialoguePlayable : BasicPlayableBehaviour
{

    public ExposedReference<GameObject> testObject;
    private GameObject _testObject;

    public ExposedReference<GameObject> dialogueBoxDisplay;
    private GameObject _dialogueBoxDisplay;

    public ExposedReference<Text> dialogueText;
    private Text _dialogueText;

    public string displayText;

    public override void OnGraphStart(Playable playable)
    {
        Debug.Log("am I even getting here?");
        _testObject = testObject.Resolve(playable.GetGraph().GetResolver());
        _dialogueBoxDisplay = dialogueBoxDisplay.Resolve(playable.GetGraph().GetResolver());
        _dialogueText = dialogueText.Resolve(playable.GetGraph().GetResolver());
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Debug.Log("setting the chest to active");
        _testObject.SetActive(true);

        Debug.Log("now trying to get the dialogue to show up");
        _dialogueBoxDisplay.SetActive(true);
        _dialogueText.text = displayText;

    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("setting to inactive");
        _testObject.SetActive(false);
        //_dialogueBoxDisplay.SetActive(false);
    }

    /*[Header("Conversation Canvas")]
    public ExposedReference<GameObject> canvasObject;
    private GameObject _canvasObject;

    [Header("Dialogue Speech")]
   // public ExposedReference<TextMeshProUGUI> dialogueTextDisplay;
    //private TextMeshProUGUI _dialogueTextDisplay;
    [Multiline(3)]
    public string dialogueString;
    public ExposedReference<Text> dialogueText;
    private Text _dialogueText;

    [Header("Dialogue Box")]
    public ExposedReference<Image> dialogueBoxDisplay;
    private Image _dialogueBoxDisplay;

    void Awake()
    {
        _canvasObject = FindObjectOfType<GameObject>();
        _dialogueText = FindObjectOfType<Text>();
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Debug.Log("On behavior play");
        _canvasObject.SetActive(true);
        _dialogueText.text = dialogueString;

    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("on behavior pause");
        _canvasObject.SetActive(false);
    } */



}


