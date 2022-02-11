using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent", fileName = "GameEvent", order = 51)]
public class GameEvent : ScriptableObject
{
    private HashSet<GameEventListener> _listeners = new();
    
    public void Invoke()
    {
        foreach (var listener in _listeners)
        {
            listener.RaiseEvent();
        }
    }
    public void Register(GameEventListener listener) => _listeners.Add(listener);
    public void Deregister(GameEventListener listener) => _listeners.Remove(listener);
}