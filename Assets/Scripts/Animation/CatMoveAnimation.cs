using UnityEngine;

public class CatMoveAnimation : MonoBehaviour
{
    private Animator _animator;

    private float _verticalInput;
    private float _horizontalInput;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    { 
        bool isJumping = Input.GetKeyUp(KeyCode.Space);
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");

        _animator.SetBool("Jump", isJumping );
        _animator.SetFloat("HorizontalInput", _horizontalInput, 0.1f, Time.deltaTime);
        _animator.SetFloat("VerticalInput", _verticalInput, 0.1f, Time.deltaTime);
    }
}
