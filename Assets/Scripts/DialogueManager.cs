using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject dBox;
    public Text dText;

    public bool dialogueActive;
    public bool dialoguePrinting;
    public bool dialogueComplete;
    public float letterPause = 0.05f;

    private PlayerController player;
    private SFXManager sfxManager;


    // Use this for initialization
    void Start() {
        player = FindObjectOfType<PlayerController>();
        sfxManager = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update() {
        if (dialogueActive && Input.GetKeyUp(KeyCode.Z)) 
        {
           if (dialogueComplete)
            {
                sfxManager.textFinished.Play();
                dBox.SetActive(false);
                dialogueActive = false;

                player.allowUserInput = true;
           }
        }
	}

    public void ShowBox(string dialogue)
    {
      if (!dialoguePrinting)
      {
           player.allowUserInput = false;
           player.stopPlayerAnimation();

            dialogueActive = true;
            dBox.SetActive(true);

            dText.text = "";
            StartCoroutine(TypeText(dialogue));
       }
    }

    IEnumerator TypeText(string dialogue)
    {
        dialogueComplete = false;
        dialoguePrinting = true;
        foreach (char letter in dialogue.ToCharArray())
        {
            dText.text += letter;
            sfxManager.textRead.Play();
            yield return new WaitForSeconds(letterPause);
         }
         dialogueComplete = true;
         dialoguePrinting = false;
    }
}
