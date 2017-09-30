using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McChickenMovement : MonoBehaviour {

    public float moveSpeed;
    public bool isWalking;
    public bool isPissed = false;
    public bool isSleeping = false;

    public Collider2D walkBoundary;
    public GameObject emoteBox;

    public float walkTime;
    public float waitTime;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private float walkCounter;
    private float waitCounter;
    private int walkDirection;
    private Vector2 maxWalkPoint;
    private Vector2 minWalkPoint;
    private Vector2 lastDirection;
    private Vector2 currentDirection;
    private bool hasWalkBoundary = false;
    private SFXManager sfxManager;

    // Use this for initialization
    void Start () {

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        sfxManager = FindObjectOfType<SFXManager>();

        walkCounter = walkTime;
        waitCounter = waitTime;
        chooseDirection();

        if (walkBoundary != null)
        {
            hasWalkBoundary = true;
            minWalkPoint = walkBoundary.bounds.min;
            maxWalkPoint = walkBoundary.bounds.max;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isSleeping = false;

        if (GameManager.instance.isDaytime == false)
        {
            isSleeping = true;
            myAnimator.SetBool("IsSleeping", isSleeping);
            myAnimator.SetBool("IsWalking", false);
            return;
        }

        if (isPissed == false)
        {
            if (isWalking)
            {
                walkCounter -= Time.deltaTime;
                if (walkCounter < 0)
                {

                   /* if (isPissed)
                    {
                        emoteBox.SetActive(false);
                        moveSpeed = 2;
                        isPissed = false;
                    }*/

                    isWalking = false;
                    waitCounter = waitTime;
                }

                // Choose a new direction for McChicken to walk
                switch (walkDirection)
                {
                    case 0:
                        // move up
                        transform.Translate(new Vector3(0f, moveSpeed * Time.deltaTime, 0f));
                        lastDirection = currentDirection;
                        currentDirection = new Vector2(0f, 1);
                        if (hasWalkBoundary && transform.position.y > maxWalkPoint.y)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                        }
                        break;
                    case 1:
                        // move right
                        transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));
                        lastDirection = currentDirection;
                        currentDirection = new Vector2(1, 0f);
                        if (hasWalkBoundary && transform.position.x > maxWalkPoint.x)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                        }
                        break;
                    case 2:
                        // move down
                        transform.Translate(new Vector3(0f, -moveSpeed * Time.deltaTime, 0f));
                        lastDirection = currentDirection;
                        currentDirection = new Vector2(0f, -1);
                        if (hasWalkBoundary && transform.position.y < minWalkPoint.y)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                        }
                        break;
                    case 3:
                        // move left
                        transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f));
                        lastDirection = currentDirection;
                        currentDirection = new Vector2(-1, 0f);
                        if (hasWalkBoundary && transform.position.x < minWalkPoint.x)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                        }
                        break;
                }
            }
            else
            {
                waitCounter -= Time.deltaTime;
                // ensure McChicken not moving
                myRigidBody.velocity = Vector2.zero;

                if (waitCounter < 0)
                    chooseDirection();
            }
        }

        myAnimator.SetFloat("LastX", lastDirection.x);
        myAnimator.SetFloat("LastY", lastDirection.y);
        myAnimator.SetFloat("XSpeed", currentDirection.x);
        myAnimator.SetFloat("YSpeed", currentDirection.y);
        myAnimator.SetBool("IsWalking", isWalking);
        myAnimator.SetBool("IsPissed", isPissed);
        myAnimator.SetBool("IsSleeping", isSleeping);
    }

    public void chooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && GameManager.instance.isDaytime)
        {
            isPissed = true;
            isWalking = false;
            emoteBox.SetActive(true);
            sfxManager.chickenCluck.Play();
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isPissed = false;
            emoteBox.SetActive(false);
            
            // RUN!
            //moveSpeed = 4;
            //chooseDirection();
        }
    }
}
