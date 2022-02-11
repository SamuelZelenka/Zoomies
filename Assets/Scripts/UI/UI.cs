using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField] private RaceTimer _raceTimer;
    [SerializeField] private TMP_Text _timerBox;
    [SerializeField] private TMP_Text _countdownBox;
    [SerializeField] private TMP_Text _messageBox;
    [SerializeField] private TMP_Text _bestTimeBox;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameEvent _raceStarted;
    [SerializeField] private GameEvent _raceFinished;
    [SerializeField] private GameEvent _restartLevel;
    [SerializeField] private GameEvent _pauseGame;
    [SerializeField] private GameEvent _resumeGame;
    [SerializeField] private int _messageTime = 1;
    [SerializeField] private LevelCheckpoints _levelCheckpoints;
    [SerializeField] private TrackData _trackData;

    

    private bool _isPaused = false;
    

    private void Start()
    {
        _timerBox.enabled = false;
        _restartButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
        _messageBox.text = "Get Ready!";
        _countdownBox.text = _raceTimer.countdownTime.ToString();
        UpdateBestTime();
    }

    
    private void Update()
    {
        if (_raceTimer.raceState.Equals(RaceTimer.RaceState.Running))
        {
            _timerBox.text = RaceTimer.GetTimeString(RaceTimer.GetElapsedTime(), 2);
        }
        
        _countdownBox.text = _raceTimer.countdownTime.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused) { Resume(); }
            else { Pause(); }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }
    
    public void UpdateBestTime()
    {
        _bestTimeBox.text = "Best Time: " + RaceTimer.GetTimeString(_trackData.recordTime, 3);
    }
    
    public void Pause()
    {
        _pauseGame.Invoke();
        _isPaused = true;
        _restartButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(true);
    }
    
    public void Resume()
    {
        _resumeGame.Invoke();
        _isPaused = false;
        _restartButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);

    }
    
    public void ShowTime()
    {
        _timerBox.enabled = true;
        _bestTimeBox.enabled = true;
    }
    
    public void HideTime()
    {
        _timerBox.enabled = false;
        _bestTimeBox.enabled = false;
    }
    
    
    public void DisableCountdown()
    {
        _countdownBox.enabled = false;
    }
    
    
    public void ClearMessage()
    {
        _messageBox.text = "";
    }
    
    
    public void ShowMessageGo()
    {
        StartCoroutine(PostMessage("GO!"));
    }
    
    public void ShowMessageNewBestTime()
    {
        StartCoroutine(PostMessage("New Best Time!"));
    }
    
    public void ShowMessageChoreDone()
    {
        StopCoroutine(PostMessage(""));
        StartCoroutine(PostMessage(_levelCheckpoints.lastClearedCheckpointName + " done!"));
    }
    
    public void ShowMessageAllDone()
    {
        StopCoroutine(PostMessage(""));
        StartCoroutine(PostMessage("All chores done!"));
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // _restartLevel.Invoke();
        // _countdownBox.enabled = true;
        // _restartButton.gameObject.SetActive(false);
    }
    
    public void EnableMenu()
    {
        _restartButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(true);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    
    public IEnumerator PostMessage(string message)
    {
        _messageBox.text = message;
        var timer = _messageTime;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        ClearMessage();
    }
    
}
