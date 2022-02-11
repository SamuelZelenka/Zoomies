using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private RaceTimer _raceTimer;
    private TMP_Text _timerBox;

    private void Awake()
    {
        _timerBox = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (_raceTimer.raceState.Equals(RaceTimer.RaceState.Running))
        {
            _timerBox.text = RaceTimer.GetTimeString(RaceTimer.GetElapsedTime(), 2);
        }
        else
        {
            _timerBox.text = RaceTimer.GetTimeString(RaceTimer._finishTime, 2);

        }
    }
}
