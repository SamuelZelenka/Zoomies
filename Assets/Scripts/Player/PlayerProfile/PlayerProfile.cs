using System.Collections.Generic;
using UnityEngine;


public class PlayerProfile : MonoBehaviour
{
    public delegate void PlayerProfileHandler(PlayerProfileData playerData);
    public PlayerProfileHandler OnProfileChanged;
    public delegate void RecordHandler(float time);
    public static RecordHandler OnNewBestTime;
    
    private PlayerProfileData _currentPlayer;
    private TrackData _selectedTrack;

    private static PlayerProfile _instance;
    public static PlayerProfile Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerProfile>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<PlayerProfile>();
                }
            }
            return _instance;
        }
        private set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else
            {
                Destroy(value.gameObject);
            }
        }
    }

    public PlayerProfileData CurrentPlayer
    {
        get
        {
            return _currentPlayer;
        }
        set
        {
            _currentPlayer = value;
            OnProfileChanged?.Invoke(_currentPlayer);
        }
    }
    
    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }

    public static void SaveProfile()
    {
        PlayerProfileData data = Instance.CurrentPlayer;
        string filename = data.username;

        SaveSystem.SaveData(filename, data);
    }

    public static float GetBestTime(string trackName)
    {
        List<TrackTime> tracktimes = Instance.CurrentPlayer.trackTimes;

        foreach (TrackTime trackTime in tracktimes)
        {
            if (trackTime.trackName == trackName)
            {
                return trackTime.time;
            }
        }
        return -1; // -1 if no score is recorded
    }
    public static float GetBestTimeCurrent()
    {
        return GetBestTime(Instance._selectedTrack.trackName);
    }

    public static void UpdateBestTime()
    {
        TrackData track = Instance._selectedTrack;
        float bestTime = GetBestTimeCurrent();
        float elapsedTime = RaceTimer.GetElapsedTime();
        
        if (bestTime < 0 || bestTime > elapsedTime)
        {
            List<TrackTime> trackTimes = Instance.CurrentPlayer.trackTimes;

            for (int i = 0; i < trackTimes.Count; i++)
            {
                if (trackTimes[i].trackName == track.trackName)
                {
                    Instance.CurrentPlayer.trackTimes[i] = new TrackTime(track.trackName, elapsedTime, Medal.GetTrackMedal(track, elapsedTime));
                    SaveProfile();
                    OnNewBestTime?.Invoke(elapsedTime);
                    return;
                }
            }

            trackTimes.Add(new TrackTime(track.trackName, elapsedTime, Medal.GetTrackMedal(track, elapsedTime)));
            SaveProfile();
            OnNewBestTime?.Invoke(elapsedTime);
        }
    }

    public void SetTrack(TrackData trackData) => _selectedTrack = trackData;
    public TrackData GetTrack() => _selectedTrack;

}
