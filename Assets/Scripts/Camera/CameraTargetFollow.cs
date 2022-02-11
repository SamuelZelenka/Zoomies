using UnityEngine;

public class CameraTargetFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _lookSpeed = 10;
    [SerializeField] private float _followSpeed = 10;

    [SerializeField] private AnimationCurve _fadeCurve;
    [SerializeField] private float _accelerationFadePower = 1f;
    [SerializeField] private float _accelerationFadePowerFOV = 1f;

    private float _startFieldOfView;
    
    private Camera _cam;
    private RaycastHit _camHit;
    private Vector3 _startOffset;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _cam = Camera.main;
        _startFieldOfView = _cam.fieldOfView;
        _playerMovement = _target.parent.GetComponent<PlayerMovement>();
        _startOffset = _offset;
    }

    private void FixedUpdate()
    {
        LookAtTarget();
        FollowTarget();
        AccelerationFade();
        WallClipping();
    }

    private void AccelerationFade()
    {
        float accelerationFade = _accelerationFadePower * _fadeCurve.Evaluate(_playerMovement.Acceleration);
        float fieldOfViewFade = _accelerationFadePowerFOV * _fadeCurve.Evaluate(_playerMovement.Acceleration);
        
        _offset.z = _startOffset.z - accelerationFade * Time.deltaTime;
        _cam.fieldOfView = _startFieldOfView + fieldOfViewFade * Time.deltaTime;
    }

    private void LookAtTarget()
    {
        Vector3 lookDirection = _target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _lookSpeed * Time.deltaTime);
    }

    private void FollowTarget()
    {
        Vector3 xOffset = _target.right * _offset.x;
        Vector3 yOffset = _target.up * _offset.y;
        Vector3 zOffset = _target.forward * _offset.z;
        
        Vector3 pos = _target.position + xOffset + yOffset + zOffset;

        transform.position = Vector3.Lerp(transform.position, pos, _followSpeed * Time.deltaTime);
    }
    
    private void WallClipping()
    {
        const float CLIPPING_OFFSET = 0.1f;

        GameObject tempObject = new GameObject();
        Transform camTransform = _cam.transform;
        Vector3 camLocalPosition = camTransform.localPosition;
        
        tempObject.transform.SetParent(_cam.transform.parent);
        tempObject.transform.localPosition = new Vector3(camLocalPosition.x, camLocalPosition.y, camLocalPosition.z - CLIPPING_OFFSET);

        bool isInsideWall = Physics.Linecast(_target.transform.position, tempObject.transform.position, out _camHit);
        
        if (isInsideWall)
        {
            camTransform.position = _camHit.point;
            camLocalPosition = camTransform.localPosition;
            
            _cam.transform.localPosition = new Vector3(camLocalPosition.x, camLocalPosition.y, camLocalPosition.z + CLIPPING_OFFSET);
        }

        Destroy(tempObject);
    }
}
