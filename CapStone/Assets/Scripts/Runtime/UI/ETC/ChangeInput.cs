using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeInput : MonoBehaviour
{
    private EventSystem _system;
    
    public Selectable firstInput;

    private void Start()
    {
        _system = EventSystem.current;
        
        firstInput.Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_system.currentSelectedGameObject != null)
            {
                Button btn = _system.currentSelectedGameObject.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke();
                }
            }
        }
    }
}
