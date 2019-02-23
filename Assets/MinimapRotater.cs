using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRotater : MonoBehaviour
{

    [SerializeField] GameObject player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Quaternion newRot = player.transform.rotation;
        newRot.x = 0;
        newRot.z = newRot.y;
        newRot.y = 0;

        gameObject.GetComponent<Transform>().localRotation = newRot;
    }
}
