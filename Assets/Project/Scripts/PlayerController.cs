using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public Vector3 look;
    public float lastLook;
    public float vel = .1f;

    [HideInInspector]
    public bool isMoving;
    public GameObject mainCamera;


	void Start () {
        look = transform.forward;
        lastLook = transform.forward.x + transform.forward.z;      
        mainCamera = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        doMove();
        
	}

    public void doMove()
    {
        
        float horiz = Input.GetAxis("Horizontal");

       float direction= Mathf.Sign(horiz) * Mathf.Ceil(Mathf.Abs(horiz));

        if (direction != lastLook)
        {
            if (direction != 0 )
            {
                // Debug.Log(lastLook);
                transform.LookAt(transform.position + (mainCamera.transform.right*direction));
                lastLook = direction;
            }
          
        }       

        if (horiz == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;                      
        }

        transform.position += mainCamera.transform.right * horiz * vel * Time.deltaTime;
    }
}
