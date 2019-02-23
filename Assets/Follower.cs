using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var camera = GameObject.Find("/MixedRealityCameraParent/MixedRealityCamera");

        Vector3 newPos = camera.transform.position;
        newPos.y = 15;
        gameObject.transform.position = newPos;

        Quaternion newRot = camera.transform.rotation;
        float x = newRot.x;
        float y = newRot.y;
        float z = newRot.z;

        newRot.z = 0;
        newRot.y = y;
        newRot.x = 0;

        gameObject.transform.rotation = newRot;
    }
}
