using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var text = GameObject.Find("/MixedRealityCameraParent/MixedRealityCamera/Canvas/Text");
        gameObject.GetComponent<Text>().text = text.GetComponent<Text>().text;
    }
}
