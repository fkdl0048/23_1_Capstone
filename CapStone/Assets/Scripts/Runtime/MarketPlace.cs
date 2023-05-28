using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketPlace : MonoBehaviour
{
    [SerializeField] private GameObject eButton;

    private bool _isPlayer = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            eButton.SetActive(true);
            _isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eButton.SetActive(false);
            _isPlayer =false;
        }
    }

    private void Update()
    {
        if (_isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var popup = GameManager.UI.UINavigation.PopupPush("InventoryPopup") as InventoryPopup;
            }
        }
    }

}
