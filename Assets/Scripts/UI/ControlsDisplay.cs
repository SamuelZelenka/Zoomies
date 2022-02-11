using UnityEngine;
using Image = UnityEngine.UI.Image;
public class ControlsDisplay : MonoBehaviour
{
    public bool isEnabled;

    [SerializeField] private Animator _animator;
    [SerializeField] private Image _wKey;
    [SerializeField] private Image _aKey;
    [SerializeField] private Image _sKey;
    [SerializeField] private Image _dKey;
    [SerializeField] private Image _spaceKey;
    
    
    private Color _baseColor;

    private void Awake()
    {
        _baseColor = _spaceKey.color;
        gameObject.SetActive(true);
        isEnabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleEnable();
        }

        _wKey.color = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? Color.white : _baseColor;
        _aKey.color = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? Color.white : _baseColor;
        _sKey.color = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? Color.white : _baseColor;
        _dKey.color = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? Color.white : _baseColor;
        _spaceKey.color = Input.GetKey(KeyCode.Space) ? Color.white : _baseColor;
    }

    public void ToggleEnable()
    {
        isEnabled = !isEnabled;
        _animator.SetBool("Enabled", isEnabled);
    }
}
