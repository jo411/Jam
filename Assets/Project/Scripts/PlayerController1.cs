using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour {

    // Use this for initialization

    public Vector3 look;
    public float velocity;

    public float lastLook;

    public GameObject mainCamera;

    [HideInInspector]
    public bool isMoving;  

	void Start () {
        look = transform.forward;
        lastLook = transform.forward.x + transform.forward.z;
        mainCamera = Camera.main.gameObject;
	}

    // Update is called once per frame
    private void Update()
    {
        doMove();
    }

    void doMove() {

        float horiz = Input.GetAxis("Horizontal");

        float direction = Mathf.Sign(horiz) * Mathf.Ceil(Mathf.Abs(horiz));

        if (direction != lastLook)
        {
            if (direction != 0)
            {
                transform.LookAt(transform.position+(mainCamera.transform.right * direction));
                lastLook = direction;
            }
        }

        if(horiz == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        transform.position += mainCamera.transform.right * horiz * velocity * Time.deltaTime;
	}
}
