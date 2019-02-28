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
        Destroy(gameObject.transform.parent);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
