using System;
using System.Collections.Generic;

[Serializable]
public class PlayerProfileData : DataContainer
{
    public string username;
    public List<TrackTime> trackTimes = new ();

    public PlayerProfileData(string username)
    {
        this.username = username;
    }
}

[Serializable]
public struct TrackTime
{
    public MedalType medal;
    public string trackName;
    public float time;
    public TrackTime(string trackName, float time, MedalType medalAchieved)
    {
        this.trackName = trackName;
        this.time = time;
        medal = medalAchieved;
    }
}
