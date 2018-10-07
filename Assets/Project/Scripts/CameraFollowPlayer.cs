using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;

    public float forwardMoveOffset = 0.1f;

    public float cameraVel = 7f;

    public PlayerController pc;


   
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("No player tag found in scene.:" + this.name);
        }

        pc = player.GetComponent<PlayerController>();

        if (pc == null)
        {
            Debug.LogError("No playercontroller found in player.:" + this.name);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.position = player.transform.position+offset;

        Vector3 slerpOffset = transform.position;

       if(pc.isMoving)
        {
           slerpOffset += pc.look * forwardMoveOffset;
        }
        Vector3 offsetRight=new Vector3(offset.z, offset.z, offset.z);
        Vector3 offsetUp= new Vector3(offset.y, offset.y, offset.y);
        Vector3 offsetForward=new Vector3(offset.x, offset.x, offset.x);

        Vector3 cameraTarget = transform.right * offset.z + transform.up * offset.y + transform.forward * offset.x;

        transform.position = Vector3.Slerp(slerpOffset, player.transform.position+ cameraTarget, Time.deltaTime*cameraVel);

	}

 
}
