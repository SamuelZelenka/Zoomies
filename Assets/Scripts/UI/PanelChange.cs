using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelChange : MonoBehaviour
{

    [SerializeField] private GameObject ThisPanel;
    [SerializeField] private GameObject NextPanel;

    public void ChangePanel()
    {
        ThisPanel.SetActive(false);
        NextPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
