using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Track : MonoBehaviour
{

    [SerializeField] public TrackData _trackData;
    [SerializeField] private RaceTimer _raceTimer;


    
    public string trackName;
    public string sceneName;
    public float bestTime;
    public string bestPlayer;

 

    public void CheckAndSaveTrackRecord()
    {
        if (_trackData.recordTime > _raceTimer.GetFinishTime())
        {
            _trackData.recordTime = RaceTimer.GetElapsedTime();
            
        }
    }
    

}
