using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGazeHandler : MonoBehaviour, IFocusable
{
    Color originalColor;

    public void OnFocusEnter()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void OnFocusExit()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
    }

    // Use this for initialization
    void Start () {
        // Save the current color
        originalColor = gameObject.GetComponent<MeshRenderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
