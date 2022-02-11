using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;

public enum MedalType {None, Bronze, Silver, Gold}
public static class Medal
{
    public static MedalType GetTrackMedal(TrackData track, float time)
    {
        bool goldCheck = time < track.goldTime && time > 0;
        bool silverCheck = time > track.goldTime && time < track.silverTime;
        bool bronzeCheck = time > track.silverTime && time < track.bronzeTime;
        
        if (goldCheck) return MedalType.Gold;
        if (silverCheck) return MedalType.Silver;
        if (bronzeCheck) return MedalType.Bronze;
        return MedalType.None;
    }
}