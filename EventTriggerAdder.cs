using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets
{
    public static class EventTriggerAdder
    {
        public static EventTrigger getEventTrigger(string pathSceneObject)
        {
            GameObject gameObj = GameObject.Find(pathSceneObject);
            return gameObj.GetComponent<EventTrigger>();
        }
        public static void addTrigger(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> call)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventTriggerType;
            entry.callback.AddListener(call);
            eventTrigger.triggers.Add(entry);
        }
        public static void addTrigger(string pathSceneObject, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> call)
        {
            GameObject gameObj = GameObject.Find(pathSceneObject);
            EventTrigger et = gameObj.GetComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventTriggerType;
            entry.callback.AddListener(call);

            et.triggers.Add(entry);
        }
        public static void addTrigger(EventTrigger eventTrigger, EventTrigger.Entry entry)
        {
            eventTrigger.triggers.Add(entry);
        }
    }
}
