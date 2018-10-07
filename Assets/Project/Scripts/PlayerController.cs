using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public Vector3 look;
    public float lastLook;
    public float vel = .1f;
    public float jumpStr;
    public float rotateSpeed;
    public bool inAir;
    public bool jumpPressed;

    public bool actionLock;

    Rigidbody rb;

    [HideInInspector]
    public bool isMoving;
    public GameObject mainCamera;

    public Animator anim;

    Transform currentTurnPoint;

    void Start () {
        look = transform.forward;
        lastLook = transform.forward.x + transform.forward.z;      
        mainCamera = Camera.main.gameObject;
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No animator found on player.:" + this.name);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(!actionLock)
        {
            doMove();
            checkHeight();
            rotateCheck();
        }

        
      
	}

    void rotateCheck()
    {
        if(Input.GetButtonDown("Vertical"))
        {
            if(currentTurnPoint!=null)
            {
                anim.SetFloat("Speed", 0);
                actionLock = true;
                transform.position = currentTurnPoint.position;
                float dotF = Mathf.Abs(Vector3.Dot(mainCamera.transform.right, currentTurnPoint.forward));
                float dotR = Mathf.Abs(Vector3.Dot(mainCamera.transform.right, currentTurnPoint.right));

                Debug.Log(dotF + " " + dotR);
                int angle = 0;

                if (dotF < .001)//turning from right to forward
                {
                    if (currentTurnPoint.gameObject.GetComponent<dimChangerDirection>().rightToForwardCCW)
                    {
                        angle = -1;
                    }
                    else
                    {
                        angle = 1;
                    }
                    
                }
                else if (dotR < .001)//turning forward to right
                {
                    if (currentTurnPoint.gameObject.GetComponent<dimChangerDirection>().forwardToRightCCW)
                    {
                        angle = -1;
                    }
                    else
                    {
                        angle = 1;
                    }
                }

               
                

                
               mainCamera.GetComponent<rotateCamera>().startRotate(angle);
            }
          
        }
    }

    public void checkHeight()
    {
        int layerMask = 1;

        RaycastHit hit;

        if(Physics.Raycast(transform.position,-transform.up,out hit,Mathf.Infinity,layerMask))
        {
            
            Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.red);
            if (hit.distance<.01)
            {
                inAir = false;
            }
            else
            {
                inAir = true;
            }
        }
        else
        {
            //inAir = true;
        }

        anim.SetBool("onGround", !inAir);
    }
    public void animFinished(string animName)
    {
        if(animName.Equals("Jump"))
        {
            anim.SetBool("Jump", false);
        }

      
    }


    public void doMove()
    {

        if(Input.GetAxis("Jump")>0&& !jumpPressed && !inAir&&rb.velocity.y<=0.01)
        {
            anim.SetBool("Jump", true);            
            rb.AddForce(0, jumpStr, 0);
            jumpPressed = true;
        }
        else
        {           
            jumpPressed = false;
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
        //transform.position += mainCamera.transform.right * horiz * vel * Time.deltaTime;
        rb.AddForce(mainCamera.transform.right * horiz * vel* Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("DimChange"))
        {
            currentTurnPoint = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("DimChange"))
        {
            currentTurnPoint = null;
        }
    }
}
