using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitRaceButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private TMP_Text _timerBox;
    [SerializeField] private GameEvent _pauseGame;
    [SerializeField] private GameEvent _resumeGame;
    [SerializeField] private TMP_Text _goldTimeText;
    [SerializeField] private TMP_Text _silverTimeText;
    [SerializeField] private TMP_Text _bronzeTimeText;
    [SerializeField] private Image _goldImage;
    [SerializeField] private Image _silverImage;
    [SerializeField] private Image _bronzeImage;
    
    private TrackData _trackData;
    private float _playerBestTime;
    private bool _isPaused = false;
    private SceneLoaderWithTransition _sceneLoader;
    
    
    private void Awake()
    {
        _trackData = PlayerProfile.Instance.GetTrack();
        _playerBestTime = PlayerProfile.GetBestTimeCurrent();
        
        _goldTimeText.text = RaceTimer.GetTimeString(_trackData.goldTime, 2);
        _silverTimeText.text = RaceTimer.GetTimeString(_trackData.silverTime, 2);
        _bronzeTimeText.text = RaceTimer.GetTimeString(_trackData.bronzeTime, 2);

        _sceneLoader = FindObjectOfType<SceneLoaderWithTransition>();
        
        SetMedalsTransparency();
    }
    
    private void SetMedalsTransparency()
    {
        _goldImage.color = SetTransparency(_goldImage.color, _trackData.goldTime);
        _silverImage.color = SetTransparency(_silverImage.color, _trackData.silverTime);
        _bronzeImage.color = SetTransparency(_bronzeImage.color, _trackData.bronzeTime);
        
    }

    private Color SetTransparency(Color color, float medalTime)
    {
        color.a = _playerBestTime < medalTime ? 1.0f : 0.5f;
        return color;
    }
    


    public void TogglePause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            _pauseGame.Invoke();
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _resumeGame.Invoke(); 
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        FindObjectOfType<SceneLoaderWithTransition>().Load(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // _restartLevel.Invoke();
        // _countdownBox.enabled = true;
        // _restartButton.gameObject.SetActive(false);
    }

    public void QuitRace()
    {
        _sceneLoader.Load("TrackSelectMenu");
        // SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
