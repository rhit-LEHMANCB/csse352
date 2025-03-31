using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventBus : MonoBehaviour
{
    public enum EventType { Button1, Button2, Button3, Button4 };

    private static IDictionary<EventType, UnityEvent> _events = new Dictionary<EventType, UnityEvent>();

    public static void Publish(EventType eventType)
    {
        Debug.Log("EventBus - EventType just happened: " + eventType.ToString());
        UnityEvent thisEvent;
        if (_events.TryGetValue(eventType, out thisEvent))
        {
            //calls each method tied to this event type
            thisEvent.Invoke();
        }

    }

    public static void Subscribe(EventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (_events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            _events.Add(eventType, thisEvent);
        }

    }

    public static void Unsubscribe(EventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (_events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

}
