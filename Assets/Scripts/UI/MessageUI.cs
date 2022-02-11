using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    [SerializeField] private int _messageTime = 1;
    [SerializeField] private LevelCheckpoints _levelCheckpoints;

    private TMP_Text _messageBox;

    private void Start()
    {
        _messageBox = GetComponent<TMP_Text>();
        _messageBox.text = "Get Ready!";
        
    }

    private void OnEnable()
    {
        LevelCheckpoints.OnCheckPointEnter += ShowMessage;
    }

    private void OnDisable()
    {
        LevelCheckpoints.OnCheckPointEnter -= ShowMessage;
    }

    public void ClearMessage()
    {
        _messageBox.text = "";
    }

    public void ShowMessage(string message)
    {
        StartCoroutine(PostMessage(message));
    }
    
    public void ShowMessageGo()
    {
        StartCoroutine(PostMessage("GO!"));
    }
    
    public void ShowMessageNewBestTime()
    {
        StartCoroutine(PostMessage("New Best Time!"));
    }
    
    public void ShowMessageChoreDone()
    {
        StopCoroutine(PostMessage(""));
        StartCoroutine(PostMessage(_levelCheckpoints.lastClearedCheckpointName + " done!"));
    }
    
    public void ShowMessageAllDone()
    {
        StopCoroutine(PostMessage(""));
        StartCoroutine(PostMessage("All chores done!"));
    }
    
    public IEnumerator PostMessage(string message)
    {
        _messageBox.text = message;
        var timer = _messageTime;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        ClearMessage();
    }
}
