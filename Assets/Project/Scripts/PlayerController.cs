using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public Vector3 look;
    public float lastLook;
    public float vel = .1f;
    public float rotateSpeed;

    [HideInInspector]
    public bool isMoving;
    public GameObject mainCamera;

    public Animator anim;

	void Start () {
        look = transform.forward;
        lastLook = transform.forward.x + transform.forward.z;      
        mainCamera = Camera.main.gameObject;


        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No animator found on player.:" + this.name);
        }
    }
	
	// Update is called once per frame
	void Update () {

        doMove();
        
	}


    public void doMove()
    {
        
        if(Input.GetAxis("Jump")>0)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }




        float horiz = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(horiz));

       float direction= Mathf.Sign(horiz) * Mathf.Ceil(Mathf.Abs(horiz));
       Vector3 lTargetDir;


        if (direction != lastLook)
        {
            if (direction != 0 )
            {
                // Debug.Log(lastLook);
                //transform.LookAt(transform.position + (mainCamera.transform.right*direction));
               
                //lTargetDir = lTargetDir.normalized;
                //lTargetDir.y = 0.0f;
                lastLook = direction;
            }
          
        }



        lTargetDir = mainCamera.transform.right * lastLook;

        if (horiz == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;           
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.deltaTime * rotateSpeed);      
        transform.position += mainCamera.transform.right * horiz * vel * Time.deltaTime;
    }
}
