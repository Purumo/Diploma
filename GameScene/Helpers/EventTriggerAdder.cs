using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace GameScene.Helpers
{
    public static class EventTriggerAdder
    {
        public static EventTrigger GetEventTrigger(string pathSceneObject)
        {
            GameObject gameObj = GameObject.Find(pathSceneObject);
            return gameObj.GetComponent<EventTrigger>();
        }
        public static void AddTrigger(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> call)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventTriggerType };
            entry.callback.AddListener(call);
            eventTrigger.triggers.Add(entry);
        }
        public static void AddTrigger(EventTrigger eventTrigger, EventTrigger.Entry entry)
        {
            eventTrigger.triggers.Add(entry);
        }
        public static void AddTrigger(string pathSceneObject, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> call)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventTriggerType };
            entry.callback.AddListener(call);

            GetEventTrigger(pathSceneObject).triggers.Add(entry);
        }
        public static void AddTrigger(string pathSceneObject, EventTrigger.Entry entry)
        {
            GetEventTrigger(pathSceneObject).triggers.Add(entry);
        }
        public static void ClearAllTriggers(string pathSceneObject)
        {
            GetEventTrigger(pathSceneObject).triggers.Clear();
        }
    }
}
