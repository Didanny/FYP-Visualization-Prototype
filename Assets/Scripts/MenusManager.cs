using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : MonoBehaviour {

    // Menu fields
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SettingsMenu;

	// Use this for initialization
	void Start () {
        Invoke("HideMain", 5);
        Invoke("ShowMain", 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Show MainMenu
    public void ShowMain()
    {
        MainMenu.SetActive(true);
    }

    // Hide MainMenu
    public void HideMain()
    {
        MainMenu.SetActive(false);
    }

    // Show SettingsMenu
    public void ShowSettings()
    {
        SettingsMenu.SetActive(true);
    }

    // Hide SettingsMenu
    public void HideSettings()
    {
        SettingsMenu.SetActive(false);
    }
}
