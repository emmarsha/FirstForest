using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour {

    // Tell the play what layers to ignore when detecting collisions
    public LayerMask layerMask;
    public float moveSpeed = 4;
    public string startPoint;
    public bool allowUserInput = true;
    public bool playFootsteps = true;

    private SFXManager sfxManager;
    private Animator animator;
    private Vector2 lastDirection;
    private Rigidbody2D RGB;

    private bool isMoving;
    private bool isFacingObject;

    private static bool playerExists;

    void Awake()
    {
        animator = GetComponent<Animator>();
        RGB = GetComponent<Rigidbody2D>();
        sfxManager = FindObjectOfType<SFXManager>();
    }

    // Use this for initialization
    void Start()
    {
        RGB.gravityScale = 0;
        RGB.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = false;
        RGB.velocity = Vector2.zero;

        if (!allowUserInput)
        {
            return;
        }

        // CheckInput left/right
        if (Input.GetAxisRaw("Horizontal") > 0.0f || Input.GetAxisRaw("Horizontal") < -0.0f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            isMoving = true;
            lastDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        // CheckInput up/down
        if (Input.GetAxisRaw("Vertical") > 0.0f || Input.GetAxisRaw("Vertical") < -0.0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            isMoving = true;
            lastDirection = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetKeyUp("up") || Input.GetKeyUp("down") || Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            isFacingObject = false;
        }

        // Animate the player based on user input
        animator.SetFloat("LastX", lastDirection.x);
        animator.SetFloat("LastY", lastDirection.y);
        animator.SetFloat("XSpeed", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("YSpeed", Input.GetAxisRaw("Vertical"));
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsCollision", isFacingObject);
    }

    // Stop any player animation when facing signs etc.
    public void stopPlayerAnimation()
    {
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsCollision", false);
    }

    public void allowPlayerAnimation(bool canMove)
    {
        animator.SetBool("IsMoving", canMove);
    }

    public Transform getPlayerPosition()
    {
        return transform;
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        Vector2 collisonDir = lastDirection;
        switch (getPlayerDir())
        {
            case "NORTH":
                if (Input.GetKey("up") && playerIsFacingObject("NORTH"))
                {
                    //Debug.Log("north");
                    collisonDir = new Vector2(0f, 1);
                    isFacingObject = true;
                }
                break;
            case "SOUTH":
                if (Input.GetKey("down") && playerIsFacingObject("SOUTH"))
                {
                    //Debug.Log("south");
                    collisonDir = new Vector2(0f, -1);
                    isFacingObject = true;
                }
                break;
            case "EAST":
                if (Input.GetKey("right") && playerIsFacingObject("EAST"))
                {
                    //Debug.Log("east");
                    collisonDir = new Vector2(1, 0f);
                    isFacingObject = true;
                }
                break;
            case "WEST":
                if (Input.GetKey("left") && playerIsFacingObject("WEST"))
                {
                    //Debug.Log("west");
                    collisonDir = new Vector2(-1, 0f);
                    isFacingObject = true;
                }
                break;
        }

        animator.SetBool("IsCollision", true);
        animator.SetFloat("XCollision", collisonDir.x);
        animator.SetFloat("YCollision", collisonDir.y);

    }
    void OnCollisionExit2D(Collision2D coll)
    {
        isFacingObject = false;
    }

    // The function is primarily used right now to play the footstep sound effect when the player walks into the boundary
    // Probably a much better way to do this and I think it more likely belongs in the intro scene script
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Bounds")
        {
            StartCoroutine(PlayFootsteps());
        }
    }

    IEnumerator PlayFootsteps()
    {
        while (playFootsteps)
        {
            sfxManager.wade.Play();
            yield return new WaitForSeconds(.3f);
        }
    }


    private bool playerIsFacingObject(string direction)
    {
        bool playerIsFacingObject = false;
        RaycastHit2D hit;

        switch (direction)
        {
            case "NORTH":
                hit = Physics2D.Raycast(transform.position, Vector2.up, .7f, layerMask);
                if (hit.collider != null)
                {
                    //float distance = Mathf.Abs(hit.point.y - transform.position.y);
                    return true;
                }
                break;
            case "SOUTH":
                hit = Physics2D.Raycast(transform.position, -Vector2.up, .7f, layerMask);
                if (hit.collider != null)
                {
                    //float distance = Mathf.Abs(hit.point.y - transform.position.y);
                    return true;
                }
                break;
            case "EAST":
                hit = Physics2D.Raycast(transform.position, Vector2.right, .7f, layerMask);
                if (hit.collider != null)
                {
                    //float distance = Mathf.Abs(hit.point.y - transform.position.y);
                    return true;
                }
                break;
            case "WEST":
                hit = Physics2D.Raycast(transform.position, -Vector2.right, .7f, layerMask);
                if (hit.collider != null)
                {
                    //float distance = Mathf.Abs(hit.point.y - transform.position.y);
                    return true;
                }
                break;
        }
        return playerIsFacingObject;
    }

    private string getPlayerDir()
    {
        string playerDir = "";
        if (Input.GetAxisRaw("Vertical") > 0.0f)
        {
            playerDir = "NORTH";
        }
        if (Input.GetAxisRaw("Vertical") < -0.0f)
        {
            playerDir = "SOUTH";
        }
        if (Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            playerDir = "EAST";
        }
        if (Input.GetAxisRaw("Horizontal") < -0.0f)
        {
            playerDir = "WEST";
        }

        return playerDir;
    }

    private bool isPressingDirKey()
    {
        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
        {
            return true;
        }
        return false;
    }


    /* Helper functions for coordinating in other scripts */
    public void setVerticalDirection(float position)
    {
        animator.SetFloat("YSpeed", position);
    }

    public void setMoveDirection(Vector2 position)
    {
        animator.SetFloat("XSpeed", position.x);
        animator.SetFloat("YSpeed", position.y);
    }

    public void setLastDirection(Vector2 position)
    {
        animator.SetFloat("LastX", position.x);
        animator.SetFloat("LastY", position.y);
    }
}
