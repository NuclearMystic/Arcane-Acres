using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject inventoryPanel;

    private void Update()
    {
        if (Input.GetButtonDown("PlayerMenu"))
        {
            ToggleInventoryPanel();
            GameManager.instance.ToggleCursorLock();
        }
    }

    private void ToggleInventoryPanel()
    {
        if(inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
        }
    }
}
