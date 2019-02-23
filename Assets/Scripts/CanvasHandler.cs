using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour {

    // The display text
    Text displayText;

    // The camera
    //private new readonly GameObject camera = gameObject.Find("MixedRealityCameraParent");

    // Use this for initialization
    void Start () {
        GameObject newGameObject = new GameObject("Text");
        newGameObject.transform.SetParent(this.transform);

        gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("/MixedRealityCameraParent/MixedRealityCamera").GetComponent<Camera>();

        //Text myText = newGameObject.AddComponent<Text>();
        //myText.text = "Ta-dah!";
        //myText.color = Color.black;
        //myText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        //myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(350, -280, 0);

        displayText = newGameObject.AddComponent<Text>();
        displayText.text = "Hello World";
        displayText.color = Color.black;
        displayText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        //displayText.GetComponent<RectTransform>().anchoredPosition = new Vector3(320, -280, 0);
        displayText.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        //displayText.GetComponent<Transform>().= new Vector3(0, 0, 0);
        displayText.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 20);
        displayText.GetComponent<RectTransform>().localScale = new Vector3(0.02f, 0.02f, 0.02f);

        //var rect = gameObject.GetComponent<RectTransform>();
        ////rect.anchoredPosition = new Vector3(0, 0, 3);
        //rect.sizeDelta = new Vector2(126.8f, 72);
        //rect.localScale = new Vector3(0.01245f, 0.01245f, 0.01245f);
    }
	
	// Update is called once per frame
	void Update () {
        var camera = GameObject.Find("/MixedRealityCameraParent/MixedRealityCamera");
        displayText.text = camera.transform.position.ToString();
        displayText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        //displayText.text = "Goodbye World";
    }
}
