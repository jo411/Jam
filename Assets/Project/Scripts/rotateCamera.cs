using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCamera : MonoBehaviour {

    public PlayerController pc;

    public bool rotate;
    public int angle;//1 for positive rotation -1 for negative
    public float rotateSpeed=3f;

// Use this for initialization
void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (pc == null)
        {
            Debug.LogError("No playercontroller found in player.:" + this.name);
        }
    }

    public void startRotate(int direction)
    {
        angle += direction;      

    }

    void Update()
    {       
       
            Quaternion target = Quaternion.Euler(0, 90 * angle, 0);
            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotateSpeed);
        ;
        //Debug.Log(Mathf.Abs(Quaternion.Dot(transform.rotation, target)));
        if (Mathf.Abs(Quaternion.Dot(transform.rotation, target)) > 0.9999f)
        {
            //Debug.Log("DONE");
            pc.actionLock = false;
            //angle = 0;
        }

    }

}
