using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public Event gameEvent;
    public UnityEvent response;

    private void OnEnable()
    {
        gameEvent.register(this);
    }

    private void OnDisable()
    {
        gameEvent.unRegister(this);
    }

    public void onEventOccurred()
    {
        response.Invoke();
    }
}