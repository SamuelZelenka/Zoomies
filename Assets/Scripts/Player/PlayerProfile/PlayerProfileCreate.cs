using TMPro;
using UnityEngine;

public class PlayerProfileCreate : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private PlayerProfilePicker _profilePicker;

    public void CreateProfile()
    {
        if (_usernameInput.text != "")
        {
            PlayerProfileData newProfile = new PlayerProfileData(_usernameInput.text);
            SaveSystem.SaveData(newProfile.username, newProfile);
            
            _profilePicker.UpdatePlayerProfiles();
            _usernameInput.text = "";
        }
    }
}
