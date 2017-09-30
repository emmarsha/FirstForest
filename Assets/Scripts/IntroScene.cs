using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour {

    public float moveSpeed = 2;

    private PlayerController player;

    // Transforms manage moving the player to the starting position
    public Transform startingPos;
    private Transform currentPos;

    private SFXManager sfxManager;
    private MusicManager musicManager;
    private DialogueManager dialogueController;
    private CameraController cameraController;

    // True once welcome message begins playing
    private bool isShowingMessage = false;

    // Destroy this game object if the intro has already been run once
    private static bool alreadyPlayedIntro = false;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();

        // Prevent user from moving character during intro scene
        player.allowUserInput = false;
        player.allowPlayerAnimation(true);

        sfxManager = FindObjectOfType<SFXManager>();
        musicManager = FindObjectOfType<MusicManager>();
        dialogueController = FindObjectOfType<DialogueManager>();
        cameraController = FindObjectOfType<CameraController>();
    }

    void Start () {
        if (alreadyPlayedIntro)
        {
            Destroy(gameObject);
            return;
        }

        alreadyPlayedIntro = true;
        musicManager.musicCanPlay = false;
        currentPos = startingPos;
    }

    // Update is called once per frame
    void Update () {

        // skip intro scene and go straight to gameplay
        if (Input.GetKey("k"))
        {
            player.playFootsteps = false;
            sfxManager.wade.Stop();

            player.transform.position = startingPos.position;

            player.allowUserInput = true;
            // Fix magic numbers at some point
            musicManager.switchTrack(3);
            cameraController.shouldFollowPlayer = true;
            Destroy(gameObject);
            return;
        }

        // Get characters current position
        Transform pos = player.getPlayerPosition();
        if (pos.position == startingPos.position)
        {
            StartCoroutine(IntroSceneDialogueAndSetup());
            return;
        }

        // Move player towards the starting point
        player.transform.position = Vector3.MoveTowards(player.transform.position, currentPos.position, Time.deltaTime * moveSpeed);
        player.setVerticalDirection(transform.position.y);

    }

    IEnumerator IntroSceneDialogueAndSetup()
    {
        // Stop footstep sound effect
        player.playFootsteps = false;
        sfxManager.wade.Stop();

        // Stop player walking animation
        player.allowPlayerAnimation(false);

        // Slight pause before showing the dialogue
        yield return new WaitForSeconds(.7f);

        if (!isShowingMessage)
        {
            dialogueController.ShowBox("Welcome, traveler!\nYour adventure awaits, as soon as Erin figures out how to code it...!");
            // Don't restart the message if it is alrady playing
            isShowingMessage = true;
        }

        // Once intro message is complete, start the overworld music and disable this script
        if (Input.GetKey("z") && dialogueController.dialogueComplete)
        {
            player.allowUserInput = true;
            yield return new WaitForSeconds(.6f);
            musicManager.switchTrack(3);
            cameraController.shouldFollowPlayer = true;
            Destroy(gameObject);
        }
    }
}
