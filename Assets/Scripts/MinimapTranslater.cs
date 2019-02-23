using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTranslater : MonoBehaviour
{

    [SerializeField] GameObject player;
    Vector3 currentPosition;

    // Use this for initialization
    void Start ()
    {
        currentPosition = player.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (player.transform.position != currentPosition)
        {
            var delta = player.transform.position - currentPosition;
            Vector3 newPos;
            newPos.x = -delta.x;
            newPos.z = 0;
            newPos.y = -delta.z;

            gameObject.transform.localPosition += newPos;
            currentPosition = player.transform.position;
        }
	}
}
