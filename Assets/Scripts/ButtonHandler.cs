using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour, IInputClickHandler
{
    [SerializeField] UnityEvent response;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        response.Invoke();
        Debug.Log( gameObject.transform.parent.name );
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Parent Name:");
        Debug.Log(gameObject.transform.parent.name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
