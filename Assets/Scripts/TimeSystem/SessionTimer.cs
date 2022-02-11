using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class SessionTimer : MonoBehaviour
{
    private TMP_Text _timerText;
    
    private bool _isRunning = true;    
    public float startTime;

    private static SessionTimer _instance;
    public static SessionTimer Instance => _instance;
    private void Awake()
    {
        _instance = _instance == null ? _instance = this : _instance;
        if (_instance != this)
        {
            Destroy(gameObject);
        }

        _timerText = GetComponent<TMP_Text>();
        
    }

    private void Update()
    {

        _timerText.text = GetTimeFormatString(GetElapsedTime());
    }

    public static string GetTimeFormatString(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return timeSpan.ToString("m\\:ss\\:fff");
    }
    
    public static float GetElapsedTime() => Instance._isRunning ? Time.time - Instance.startTime : 0;

    public void StartTimer()
    {
        startTime = Time.time;
        _isRunning = true;
    }
    public void StopTimer()
    {
        startTime = GetElapsedTime();
        _isRunning = false;
    }
}
