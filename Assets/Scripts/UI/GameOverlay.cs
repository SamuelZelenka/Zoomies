using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverlay : MonoBehaviour
{
    [SerializeField] private InGameMenu _menu;

    private bool _hasStarted = false;

    public void StartRace()
    {
        _hasStarted = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _hasStarted)
        {
            _menu.TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _menu.RestartLevel();
        }
    }
    

    
    public void ShowOverlay(bool isShowing)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isShowing);
        }
    }
}
