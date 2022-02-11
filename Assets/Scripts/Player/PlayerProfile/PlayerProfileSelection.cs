using System;
using TMPro;
using UnityEngine;

public class PlayerProfileSelection : MonoBehaviour
{
    private string username;
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private ConfirmationWindow _confirmationWindow;
    private Action ChangePanelEvent;
    private PanelChange _panelChange;

    private SceneLoaderWithTransition _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoaderWithTransition>();
    }

    public void SetUser(string user)
    {
        username = user;
        usernameText.text = username;
    }

    public void AssignOnClick(Action OnClickCallback)
    {
        ChangePanelEvent = OnClickCallback;
    }
    public void OnClick()
    {
        PlayerProfile.Instance.CurrentPlayer = SaveSystem.LoadData<PlayerProfileData>(username);
        // ChangePanelEvent?.Invoke();
        _sceneLoader.Load("TrackSelectMenu");
    }

    public void OnRemoveClick()
    {
        string title = $"Are you sure you want to delete <b>{username}</b>?";
        string message = "The player profile will be permanently deleted.";
        
        //TODO: Remove Asset file
        
        ConfirmationWindow.InitConfirmation(null, Delete, title, message);
    }

    private void Delete()
    {
        SaveSystem.DeleteFile(username);
        Destroy(gameObject);
    }
    
}
