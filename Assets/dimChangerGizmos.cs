using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dimChangerGizmos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = new Color(.8f, 0f, .8f);
        UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, transform.right, -Vector3.Angle(transform.right,transform.forward), 1f);
        
    }
}
