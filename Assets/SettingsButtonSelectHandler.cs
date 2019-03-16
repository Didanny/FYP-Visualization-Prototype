using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingsButtonSelectHandler : MonoBehaviour, IInputClickHandler
{

    // GameObject fields
    [SerializeField] UnityEvent ShowSettings;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        ShowSettings.Invoke();
    }

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
