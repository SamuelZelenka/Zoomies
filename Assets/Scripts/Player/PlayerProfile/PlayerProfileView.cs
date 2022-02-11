using TMPro;
using UnityEngine;

public class PlayerProfileView : MonoBehaviour
{
    [SerializeField] private TMP_Text _username;
    private void OnEnable() => PlayerProfile.Instance.OnProfileChanged += UpdateProfileView;
    private void OnDisable() => PlayerProfile.Instance.OnProfileChanged -= UpdateProfileView;
    private void UpdateProfileView(PlayerProfileData data) => _username.text = data.username;
}
