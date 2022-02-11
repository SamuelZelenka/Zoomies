using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheckpoints : MonoBehaviour
{
    public delegate void CheckPointHandler(string checkpointName);
    public static CheckPointHandler OnCheckPointEnter;
    
    [SerializeField] private GameEvent _raceFinished;
    [SerializeField] private List <CheckpointSingle> _checkpointSingleList;
    [SerializeField] private ParticleSystem _particleSystemPrefab;
    
    public string lastClearedCheckpointName;
    
    private Action<string> _checkPointDone;
    private ParticleSystem _particleSystem;
    private int _nextCheckpointSingleIndex;

    private void Awake() 
    {
        _checkpointSingleList = new List <CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in transform) 
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            
            checkpointSingle.SetTrackCheckpoints(this);
            _checkpointSingleList.Add(checkpointSingle);
        }
        
        _nextCheckpointSingleIndex = 0;
        _particleSystem = Instantiate(_particleSystemPrefab);
    }

    private void Start()
    {
        for (int i = 1; i < _checkpointSingleList.Count; i++)
        {
            _checkpointSingleList[i].gameObject.SetActive(false);
        }
    }
    
    public void RestartLevel()
    {
        _nextCheckpointSingleIndex = 0;
        _checkpointSingleList[0].gameObject.SetActive(true);
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle) 
    {
        if (_checkpointSingleList.IndexOf(checkpointSingle) == _nextCheckpointSingleIndex) 
        {
            if (_nextCheckpointSingleIndex == _checkpointSingleList.Count - 1)
            {
                _raceFinished.Invoke();
                checkpointSingle.gameObject.SetActive(false);
            }
            else
            {
                _particleSystem.transform.position = checkpointSingle.transform.position;
                _particleSystem.transform.rotation = new Quaternion(0, 0, 0, 0);
                _particleSystem.Play();
                lastClearedCheckpointName = checkpointSingle.name;
                _nextCheckpointSingleIndex++;
                OnCheckPointEnter?.Invoke(checkpointSingle.name);
                checkpointSingle.GetComponent<Collider>().enabled = false;
                _checkpointSingleList[_nextCheckpointSingleIndex].gameObject.SetActive(true);
            }
        }
    }
}
