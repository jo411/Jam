using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerCasey : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;

    public float forwardMoveOffest = 0.1f;

    float cameraVel = 6f;

    public PlayerController1 pc;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.LogError("No player tag found in scene.:" + this.name);
        }

        pc = player.GetComponent<PlayerController1>();

        if(pc == null)
        {
            Debug.LogError("No PlayerController found on the player");
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {

        //transform.position = player.transform.position + offset;

        Vector3 slerpOffset = transform.position;

        if (pc.isMoving)
        {
            slerpOffset += pc.look * forwardMoveOffest;
        }

        transform.position = Vector3.Slerp(slerpOffset, player.transform.position + offset,Time.deltaTime * cameraVel);
	}
}
