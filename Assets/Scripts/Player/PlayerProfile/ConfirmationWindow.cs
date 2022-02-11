using System;
using TMPro;
using UnityEngine;

public class ConfirmationWindow : MonoBehaviour
{
    public static ConfirmationWindow Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ConfirmationWindow>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    private Action OnCancel;
    private Action OnAccept;

    private static ConfirmationWindow _instance;

    [SerializeField] private TMP_Text _titleString;
    [SerializeField] private TMP_Text _confirmationMessage;

    public static void InitConfirmation(Action OnCancel,Action OnAccept, string title, string message)
    {
        Instance.gameObject.SetActive(true);
        Instance._titleString.text = title;
        Instance._confirmationMessage.text = message;
        Instance.OnCancel = OnCancel;
        Instance.OnAccept = OnAccept;
    }
    
    public void Cancel() => ButtonClick(OnCancel);
    public void Accept() => ButtonClick(OnAccept);

    private void ButtonClick(Action callback)
    {
        if (callback != null)
        {
            callback();
        }
        gameObject.SetActive(false);
    }
}
