using UnityEngine;

public class SpeedLineParticles : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ParticleSystem _spikeParticle;
    [SerializeField] private float _minVal = 13.9f;
    [SerializeField] private float _maxVal = 14.6f;
    
    private ParticleSystem.ShapeModule _shape;
    private void Start()
    {
        _shape = _spikeParticle.shape;
    }
    void Update()
    {
        _shape.radius = Mathf.Lerp(_maxVal, _minVal, _playerMovement.Acceleration);
    }
}
