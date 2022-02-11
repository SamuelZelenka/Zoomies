using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class TracksGridItem : MonoBehaviour
{
    [SerializeField] private TrackData trackData;
    [SerializeField] private TMP_Text trackDetailsName;
    [SerializeField] private TMP_Text _goldTime;
    [SerializeField] private TMP_Text _silverTime;
    [SerializeField] private TMP_Text _bronzeTime;
    [SerializeField] private TMP_Text _personalBest;
    [SerializeField] private Image _achievedMedal;
    [SerializeField] private SpriteLibraryAsset _medalLibrary;

    private SceneLoaderWithTransition _sceneLoader;
    private TMP_Text _nameBox;


    private void Awake()
    {
        _sceneLoader = FindObjectOfType<SceneLoaderWithTransition>();
    }

    private void Start()
    {
        _nameBox = GetComponentInChildren<TMP_Text>();
        _nameBox.text = trackData.trackName;
        
    }

    
    public void ShowDetails()
    {
        float bestTime = PlayerProfile.GetBestTime(trackData.trackName);
        trackDetailsName.text = trackData.trackName;
        _goldTime.text = RaceTimer.GetTimeString(trackData.goldTime, 3);
        _silverTime.text = RaceTimer.GetTimeString(trackData.silverTime, 3);
        _bronzeTime.text = RaceTimer.GetTimeString(trackData.bronzeTime, 3);
        _personalBest.text = RaceTimer.GetTimeString(bestTime, 3);
        switch (Medal.GetTrackMedal(trackData, bestTime))
        {
            case MedalType.Bronze:
                _achievedMedal.sprite = _medalLibrary.GetSprite("Medal", "Bronze");
                break;
            case MedalType.Silver:
                _achievedMedal.sprite = _medalLibrary.GetSprite("Medal", "Silver");
                break;
            case MedalType.Gold:
                _achievedMedal.sprite = _medalLibrary.GetSprite("Medal", "Gold");
                break;
            default:
                _achievedMedal.sprite = _medalLibrary.GetSprite("Medal", "None");;
                break;
        }
    }

    
    public void PlayScene()
    {
        PlayerProfile.Instance.SetTrack(trackData);
        _sceneLoader.Load(trackData.sceneName);
        // SceneManager.LoadScene(trackData.sceneName);
    }
}
