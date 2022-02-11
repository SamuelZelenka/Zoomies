using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] public int _countdownTimePreset = 3;
    [SerializeField] private GameEvent _countdownStarted;
    [SerializeField] private GameEvent _raceStarted;
    [SerializeField] private GameEvent _raceFinished;
    [SerializeField] private GameEvent _countdownUpdated;
    
    public int countdownTime;
    public RaceState raceState = RaceState.PreRace;
    
    private static float _startTime;
    public static float _finishTime;
    private static float _pauseTime;
    private static float _pauseStartTime;
    

    public enum RaceState {
        PreRace, Running, Paused, PostRace
    }
    
    
    private void Start()
    {
        StartCountdown();
    }



    public void StartCountdown()
    {
        StartCoroutine(Countdown());
        _countdownStarted.Invoke();
    }

    
    public void StartRace()
    {
        _startTime = Time.time;
        raceState = RaceState.Running;
        StopCoroutine(Countdown());
        _raceStarted.Invoke();
    }
    
    
    public void FinishRace()
    {
        _finishTime = GetElapsedTime();
        raceState = RaceState.PostRace;
    }
    
    public float GetFinishTime()
    {
        return _finishTime;
    }
    
    
    public void RestartLevel()
    {
        raceState = RaceState.PreRace;
        StartCountdown();
    }
    
    
    public static float GetElapsedTime()
    {
        return Time.time - _startTime - _pauseTime;
    }


    public static string GetTimeString(float time, int decimalCount)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string decimals = "";
        for (int i = 0; i < decimalCount; i++)
        {
            decimals += 'f';
        }
        if (time < 0)
        {
            return "--:--:--";
        }
        return timeSpan.ToString($"m\\:ss\\:{decimals}");
    }
    
    public void Pause()
    {
        raceState = RaceState.Paused;
        _pauseStartTime = Time.time;
    }
    
    public void Resume()
    {
        raceState = RaceState.Running;
        _pauseTime += Time.time - _pauseStartTime;
    }
    
    private IEnumerator Countdown()
    {
        countdownTime = _countdownTimePreset;
        while(countdownTime > 0)
        {
            _countdownUpdated.Invoke();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }
        StartRace();
    }
}
