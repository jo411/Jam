using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;

    public float forwardMoveOffset = 0.1f;

    public float cameraVel = 6f;

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
	void LateUpdate () {
        //transform.position = player.transform.position+offset;

        Vector3 slerpOffset = transform.position;

       if(pc.isMoving)
        {
           slerpOffset += pc.look * forwardMoveOffset;
        }

        transform.position = Vector3.Slerp(slerpOffset, player.transform.position+offset, Time.deltaTime*cameraVel);

	}
}
