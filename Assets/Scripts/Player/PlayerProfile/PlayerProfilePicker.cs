using System.IO;
using UnityEngine;

public class PlayerProfilePicker : MonoBehaviour
{
    [SerializeField] private PlayerProfileSelection _profileSelectionPrefab;
    [SerializeField] private GameObject _currentPanel;
    [SerializeField] private GameObject _nextPanel;

    public void ChangePanel()
    {
        _currentPanel.SetActive(false);
        _nextPanel.SetActive(true);
    }

    private void Awake()
    {
        UpdatePlayerProfiles();
    }

    public void UpdatePlayerProfiles()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SaveSystem.SESSION_SAVE_DIRECTORY);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.json");

        Utility.DestroyChildObjects(transform);
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (fileInfo != null)
            {
                string username = fileInfo.Name.Split('.')[0];
                PlayerProfileSelection newProfile = Instantiate(_profileSelectionPrefab, transform);
                newProfile.AssignOnClick(ChangePanel);
                newProfile.SetUser(username);
            }
        }
    }
}
