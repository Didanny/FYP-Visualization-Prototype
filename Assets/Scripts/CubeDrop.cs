using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDrop : MonoBehaviour {

    // Drops the cube
    public void DropCube()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            rigidBody.WakeUp();
        }
    }

    // Resets cube to original position
    public void ResetCube()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.Sleep();
        }
        gameObject.transform.position = new Vector3(0, 0, 2);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
