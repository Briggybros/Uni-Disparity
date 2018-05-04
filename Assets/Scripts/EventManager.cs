using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEvent : UnityEvent<string> {}

public class EventManager : MonoBehaviour {

    private Dictionary<string, GameEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance{
        get {
            if (eventManager == null) {
                eventManager = FindObjectOfType<EventManager>();

                if (eventManager == null) {
                    Debug.LogError("There isn't an event manager in the scene");
                } else {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    private void Init () {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, GameEvent>();
        }
    }

    public static void AddEventListener (string eventName, UnityAction<string> listener) {
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new GameEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void RemoveEventListener (string eventName, UnityAction<string> listener) {
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        } else {
            Debug.LogError("No such event found");
        }
    }
}
