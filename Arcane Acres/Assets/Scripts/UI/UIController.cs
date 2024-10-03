using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject playerMenu;

    private void Update()
    {
        if (Input.GetButtonDown("PlayerMenu"))
        {
            TogglePlayerMenu();
            GameManager.instance.ToggleCursorLock();
        }
    }

    private void TogglePlayerMenu()
    {
        if(playerMenu.activeSelf)
        {
            playerMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            playerMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
