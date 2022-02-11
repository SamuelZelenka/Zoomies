using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region Fields

    [Header("Movement")]
    [SerializeField] private AnimationCurve _accelerationGraph;
    [SerializeField] private AnimationCurve _backAccelerationGraph;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _accelerationGrowth;
    [SerializeField] private float _decelerationGrowth;
    [SerializeField] private float _counterMoveMultiplier = 5;
    
    
    [Header("Rotation")]
    [SerializeField] private float _rotationSpeed;
    [Range(0, 1)] [SerializeField] private float _minRotationPercentage = 0.66f;
    
    [Header("Jump")]
    [SerializeField] private AnimationCurve _jumpGraph;
    [Range(0, 1)] public float _jumpPower01; // 0 - 1
    [SerializeField] private float _jumpMaxPowerTime;
    [SerializeField] private float _jumpUpForce;
    [SerializeField] private float _jumpForwardForce;
    [Range(0, 1)] [SerializeField] private float _decelerationJumpPercentage;
    [Range(0, 1)] [SerializeField] private float _minForwardForcePercentage;
    
    private bool _onGround;
    private float _jumpPower;
    private float _acceleration; 

    private float _horizontalInput;
    private float _verticalInput;
    private float _jumpInput;

    private float _lastJumpTime;

    private Rigidbody _rigidbody;
    private Vector3 releaseForwardDirection;
    
    #endregion
    
    #region Properties
    public float Acceleration
    {
        get => _acceleration;
    }

    private float AccelerationSpeed
    {
        get
        {
            float addDeltaTime = 0;
            
            if (_verticalInput > 0 && _onGround)
            {
                addDeltaTime = _acceleration < 0 ? _accelerationGrowth * _counterMoveMultiplier
                    : _accelerationGrowth;
            }
            else if(_verticalInput < 0 && _onGround)
            {
                addDeltaTime = -_decelerationGrowth;
            }
            else if(_verticalInput == 0 && _acceleration != 0 || !_onGround)
            {
                float decelerationOnJump =
                    !_onGround ? _decelerationGrowth * _decelerationJumpPercentage : _decelerationGrowth;
                
                addDeltaTime = _acceleration > 0 ? -decelerationOnJump
                    : _accelerationGrowth * _counterMoveMultiplier;
            }
            
            _acceleration += addDeltaTime * Time.deltaTime;
            _acceleration = Mathf.Clamp(_acceleration, -1f, 1f);

            return _acceleration > 0 ? _accelerationSpeed * _accelerationGraph.Evaluate(_acceleration)
                : _accelerationSpeed * -_backAccelerationGraph.Evaluate(Mathf.Abs(_acceleration));
        }
    }    
    
    private float ClampedRotationSpeed => Mathf.Clamp(1f - _acceleration, _minRotationPercentage, 1f) * _rotationSpeed;

    private Vector3 ForwardVelocity
    {
        get
        {
            Vector3 velocity = transform.forward * AccelerationSpeed * TurnFriction();
            velocity.Set(velocity.x, _rigidbody.velocity.y, velocity.z);
            return velocity;
        }
    }

    private Quaternion RotationVelocity
    {
        get
        {
            Quaternion velocity = Quaternion.Euler(0f, _horizontalInput * Time.deltaTime * ClampedRotationSpeed, 0f);
            return velocity;
        }
    }
    #endregion
    
    #region Movement
    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        GetInput();

        bool slowDown = _verticalInput == 0 && _acceleration != 0;
        
        if(slowDown)
        {
            const float THRESHOLD = 0.03f;
            
            _acceleration = _acceleration.ApproximatelyWithin(0, THRESHOLD) ? 0 : _acceleration;
        }
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();

        _onGround = false;
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        JumpInput();
    }
    private float TurnFriction()
    {
        releaseForwardDirection = _verticalInput != 0 ? transform.forward : releaseForwardDirection;
        float directionDot = ExtensionMethods.Dot01(releaseForwardDirection, transform.forward);

        return directionDot;
    }

    private void ApplyMovement()
    {
        _rigidbody.velocity = ForwardVelocity;
        JumpVelocity();
    }

    private void ApplyRotation()
    {
        _rigidbody.rotation *= RotationVelocity;
    }
    #endregion

    #region Jump
    private void JumpInput()
    {
        _jumpInput = Input.GetAxis("Jump");
        _jumpPower01 = _jumpPower / _jumpMaxPowerTime;

        if (_jumpInput > 0 && _onGround && _jumpPower < _jumpMaxPowerTime)
        {
            _jumpPower += Time.deltaTime;
            _jumpPower = Mathf.Min(_jumpPower, _jumpMaxPowerTime);
        }
    }
    
    private void JumpVelocity()
    {
        bool isJumping = (_jumpInput == 0) && (_jumpPower > 0) && _onGround;
        
        if (isJumping)
        {
            float t = _jumpPower / _jumpMaxPowerTime;
            float jumpHeight = _jumpUpForce * _jumpGraph.Evaluate(t);
            float clampedAcceleration = Mathf.Clamp(_acceleration, _minForwardForcePercentage, _acceleration);
            
            _rigidbody.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            _rigidbody.AddForce(transform.forward * clampedAcceleration * _jumpForwardForce, ForceMode.Impulse);
            
            _jumpPower = 0;
        }
    }
    #endregion

    #region GroundCheck

    private void OnCollisionEnter(Collision other) => EvaluateCollision(other);
    private void OnCollisionStay(Collision other) => EvaluateCollision(other);

    private void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            _onGround |= normal.y >= 0.9;
        }
    }
#endregion
}
