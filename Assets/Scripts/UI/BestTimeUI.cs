using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class BestTimeUI : MonoBehaviour
{
    [SerializeField] private Sprite[] _medalSprites = new Sprite[3];
    [SerializeField] private Image _medalImage;
    [SerializeField] private TMP_Text _medalTime;
    [SerializeField] private TMP_Text _bestTimeBox;
    [SerializeField] private TMP_Text _personalBestMessage;

    private TrackData _trackData;
    private bool _raceRunning;


    
    private void Start()
    {
        UpdateBestTime(PlayerProfile.GetBestTimeCurrent());
        _trackData = PlayerProfile.Instance.GetTrack();
        _raceRunning = false;
        _personalBestMessage.text = "";
        PlayerProfile.OnNewBestTime += ShowPersonalBestMessage;
        PlayerProfile.OnNewBestTime += UpdateBestTime;
    }

    private void OnDisable()
    {
        PlayerProfile.OnNewBestTime -= ShowPersonalBestMessage;
        PlayerProfile.OnNewBestTime -= UpdateBestTime;
    }

    public void UpdateBestTime(float time)
    {
        _bestTimeBox.text = "Best Time: " + RaceTimer.GetTimeString(time, 3);
    }

    public void StartRace() => _raceRunning = true;
    public void StopRace() => _raceRunning = false;

    private void Update()
    {
        if (_raceRunning)
        {
            UpdateMedal(); 
        }
    }
    
    public void ShowPersonalBestMessage(float time)
    {
        StartCoroutine(ShowPersonalBestMessageFor(1));
    }
    
    private IEnumerator ShowPersonalBestMessageFor(float time)
    {
        _personalBestMessage.text = "New Personal Best";
        yield return new WaitForSeconds(time);
        _personalBestMessage.text = "";
    }

    private void UpdateMedal()
    {
        float elapsedTime = RaceTimer.GetElapsedTime();

        bool goldCheck = elapsedTime < _trackData.goldTime;
        bool silverCheck = elapsedTime > _trackData.goldTime && elapsedTime < _trackData.silverTime;
        bool bronzeCheck = elapsedTime > _trackData.silverTime && elapsedTime < _trackData.bronzeTime;
        if (goldCheck)
        {
            _medalImage.sprite = _medalSprites[0];
            _medalTime.text = RaceTimer.GetTimeString(_trackData.goldTime, 3);
        }
        else if (silverCheck)
        {
            _medalImage.sprite = _medalSprites[1];
            _medalTime.text = RaceTimer.GetTimeString(_trackData.silverTime, 3);
        }
        else if (bronzeCheck)
        {
            _medalImage.sprite = _medalSprites[2];
            _medalTime.text = RaceTimer.GetTimeString(_trackData.bronzeTime, 3);
        }
        else
        {
            _medalImage.gameObject.SetActive(false);
            _medalTime.text = "--:--:--";
        }
    }
}
