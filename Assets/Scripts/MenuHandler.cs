using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }
}
