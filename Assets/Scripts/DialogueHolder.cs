using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {

    public string text;
    private DialogueManager dMan;

    private GameObject[] characterHolder = new GameObject[1];
    private bool isShowingMessage = false;

	// Use this for initialization
	void Start () {
        dMan = FindObjectOfType<DialogueManager>();
	}

    // Update is called once per frame
    void Update()
    {

        // hackety hack hack
        if (characterHolder[0] != null)
        {
            if (Input.GetKeyDown("z"))
            {
                if (!isShowingMessage)
                {
                    dMan.ShowBox(text);
                    isShowingMessage = true;
                }

                // hackety hack hack
                if (dMan.dialogueComplete == true)
                {
                    isShowingMessage = false;
                }
            }
        }
    }

    // this whole file is pretty much a giant hackathon
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Link")
        {
            if (characterHolder[0] == null)
            {
                characterHolder[0] = other.gameObject;
            }
        }
    }

    // more hacking
    void OnTriggerExit2D(Collider2D other)
    {
        characterHolder[0] = null;
    }
}
