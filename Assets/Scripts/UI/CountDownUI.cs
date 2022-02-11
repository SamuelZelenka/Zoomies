using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
   
    [SerializeField] private RaceTimer _raceTimer;
    
    private TMP_Text _countdownBox;
    
    void Start()
    {
        _countdownBox = GetComponent<TMP_Text>();
        _countdownBox.text = _raceTimer.countdownTime.ToString();
        _countdownBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _countdownBox.text = _raceTimer.countdownTime.ToString(); 
    }
    
    public void HideCountdown()
    {
        _countdownBox.enabled = false;
    }
    
    public void ShowCountdown()
    {
        _countdownBox.enabled = true;
    }

}
