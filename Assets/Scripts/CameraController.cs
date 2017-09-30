using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed = 5f;

    public bool shouldFollowPlayer;

    public BoxCollider2D boundsBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    private static bool cameraExists;

    // Use this for initialization
    void Start () {
    
        if(!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        minBounds = boundsBox.bounds.min;
        maxBounds = boundsBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (shouldFollowPlayer)
        {
            targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float clampX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            transform.position = new Vector3(clampX, clampY, transform.position.z);
        }
    }
}
