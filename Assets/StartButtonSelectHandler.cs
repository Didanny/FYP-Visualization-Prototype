using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartButtonSelectHandler : MonoBehaviour, IInputClickHandler
{
    [SerializeField] UnityEvent miniPath;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        miniPath.Invoke();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
