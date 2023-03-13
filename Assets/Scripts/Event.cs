using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Event : ScriptableObject
{
    private List<EventListener> eventListeners = new List<EventListener>();

    public void register(EventListener eventListener)
    {
        eventListeners.Add(eventListener);
    }

    public void unRegister(EventListener eventListener)
    {
        eventListeners.Remove(eventListener);
    }

    public void occurred()
    {
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].onEventOccurred();
        }
    }
}