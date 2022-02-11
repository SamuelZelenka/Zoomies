using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _startPosition;
    private PlayerMovement _playerMovement;
    private Vector3 _velocityOnPause = new Vector3(0,0,0);

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        DisableCatMovement();
        MoveToStartPos();
    }


    public void MoveToStartPos()
    {
        transform.position = _startPosition.transform.position;
        transform.rotation = _startPosition.transform.rotation;
    }
    
    public void DisableCatMovement()
    {
        _playerMovement.enabled = false;
        PauseVelocity();
    }
    
    public void EnableCatMovement()
    {
        _playerMovement.enabled = true;
        ResumeVelocity();
    }
    
    private void PauseVelocity()
    {
        _velocityOnPause = _playerMovement.GetComponent<Rigidbody>().velocity;
        _playerMovement.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        StartCoroutine(StopVelocityWithDelay(0.2f));
    }
    
    private void ResumeVelocity()
    {
        StopCoroutine(StopVelocityWithDelay(0.2f));
        _playerMovement.GetComponent<Rigidbody>().velocity = _velocityOnPause;
    }

    private IEnumerator StopVelocityWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _playerMovement.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

    }
    
}
