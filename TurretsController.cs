using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets;
using Assets.State;

public class TurretsController : MonoBehaviour
{
    private const string
        PathButtonUp = "Canvas/Up",
        PathButtonDown = "Canvas/Down",
        PathButtonRight = "Canvas/Right",
        PathButtonLeft = "Canvas/Left",
        PathButtonSwitch = "Canvas/Switch",
        PathButtonShoot = "Canvas/Shoot";

    private EventTrigger triggerUp, triggerDown, triggerRight, triggerLeft, triggerSwitch;
    Context context;
    void Start()
    {
        context = new Context(new HorizontalState());

        triggerUp = EventTriggerAdder.getEventTrigger(PathButtonUp);
        triggerDown = EventTriggerAdder.getEventTrigger(PathButtonDown);
        triggerRight = EventTriggerAdder.getEventTrigger(PathButtonRight);
        triggerLeft = EventTriggerAdder.getEventTrigger(PathButtonLeft);
        triggerSwitch = EventTriggerAdder.getEventTrigger(PathButtonSwitch);

        EventTrigger.Entry entryNone = new EventTrigger.Entry();
        entryNone.eventID = EventTriggerType.PointerExit;
        entryNone.callback.AddListener((data) => { context.noneAction(); });

        EventTriggerAdder.addTrigger(triggerUp, EventTriggerType.PointerDown, (data) => { context.clickUp(); });
        EventTriggerAdder.addTrigger(triggerUp, entryNone);
        EventTriggerAdder.addTrigger(triggerDown, EventTriggerType.PointerDown, (data) => { context.clickDown(); });
        EventTriggerAdder.addTrigger(triggerDown, entryNone);
        EventTriggerAdder.addTrigger(triggerRight, EventTriggerType.PointerDown, (data) => { context.clickRight(); });
        EventTriggerAdder.addTrigger(triggerRight, entryNone);
        EventTriggerAdder.addTrigger(triggerLeft, EventTriggerType.PointerDown, (data) => { context.clickLeft(); });
        EventTriggerAdder.addTrigger(triggerLeft, entryNone);
        EventTriggerAdder.addTrigger(triggerSwitch, EventTriggerType.PointerEnter, (data) => { context.clickSwitch(); });
        
        //context.noneAction();
    }

    void FixedUpdate()
    {
        context.moveAction();
    }
}
