using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSmoke : MonoBehaviour
{
    [SerializeField] private ParticleSystem _leftLeg;
    [SerializeField] private ParticleSystem _rightLeg;

    [SerializeField] [Range(0,1)] private float startSpeed = 0.6f;
    [SerializeField] [Range(0,1)] private float stopSpeed = 0.02f;
    
    private PlayerMovement _playerMovement;
    
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        PlaySmokeParticle();
    }

    void PlaySmokeParticle()
    {
        if (_playerMovement.Acceleration >= startSpeed && !_leftLeg.isPlaying && !_rightLeg.isPlaying)
        {
            _leftLeg.Play();
            _rightLeg.Play();
        }
        
        if (_playerMovement.Acceleration <= stopSpeed && _leftLeg.isPlaying && _rightLeg.isPlaying)
        {
            _leftLeg.Stop();
            _rightLeg.Stop();
        }
    }
}
