using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets;

public class TurretsController : MonoBehaviour
{
    private const string
        PathButtonUp = "Canvas/Up",
        PathButtonDown = "Canvas/Down",
        PathButtonRight = "Canvas/Right",
        PathButtonLeft = "Canvas/Left",
        PathButtonSwitch = "Canvas/Switch",
        PathButtonShoot = "Canvas/Shoot",
        PathTurretsHorizontal = "Turrets/Horizontal",
        PathTurretsVertical = "Turrets/Vertical";

    private EventTrigger triggerUp, triggerDown, triggerRight, triggerLeft, triggerSwitch;
    public enum ActionType
    {
        None = 0,
        MoveRight = 1,
        MoveLeft = 2,
        MoveUp = 3,
        MoveDown = 4,
        Switch = 5,
        Shoot = 6,
        //EndGame = 7
    }
    private Rigidbody2D rigidbodyTurretsCurrent;
    private Rigidbody2D getRigidbody(string pathSceneObject)
    {
        Rigidbody2D objBody = null;
        GameObject rigidbodyHorizontal = GameObject.Find(pathSceneObject);
        if (objBody == null) objBody = rigidbodyHorizontal.GetComponent<Rigidbody2D>();
        return objBody;
    }
    void Start()
    {
        triggerUp = EventTriggerAdder.getEventTrigger(PathButtonUp);
        triggerDown = EventTriggerAdder.getEventTrigger(PathButtonDown);
        triggerRight = EventTriggerAdder.getEventTrigger(PathButtonRight);
        triggerLeft = EventTriggerAdder.getEventTrigger(PathButtonLeft);
        triggerSwitch = EventTriggerAdder.getEventTrigger(PathButtonSwitch);

        EventTrigger.Entry entryNone = new EventTrigger.Entry();
        entryNone.eventID = EventTriggerType.PointerExit;
        entryNone.callback.AddListener((data) => { OnDoAction(ActionType.None); });

        EventTriggerAdder.addTrigger(triggerUp, EventTriggerType.PointerDown, (data) => { OnDoAction(ActionType.MoveUp); });
        EventTriggerAdder.addTrigger(triggerUp, entryNone);
        EventTriggerAdder.addTrigger(triggerDown, EventTriggerType.PointerDown, (data) => { OnDoAction(ActionType.MoveDown); });
        EventTriggerAdder.addTrigger(triggerDown, entryNone);
        EventTriggerAdder.addTrigger(triggerRight, EventTriggerType.PointerDown, (data) => { OnDoAction(ActionType.MoveRight); });
        EventTriggerAdder.addTrigger(triggerRight, entryNone);
        EventTriggerAdder.addTrigger(triggerLeft, EventTriggerType.PointerDown, (data) => { OnDoAction(ActionType.MoveLeft); });
        EventTriggerAdder.addTrigger(triggerLeft, entryNone);
        EventTriggerAdder.addTrigger(triggerSwitch, EventTriggerType.PointerEnter, (data) => { OnDoAction(ActionType.Switch); });

        rigidbodyTurretsCurrent = getRigidbody(PathTurretsHorizontal);
    }

    private bool onMoving = false;
    private bool direction = true; //true: for vectical in right and for horizontal in up
    private bool isHorizontalTurretsSelected = true;
    public void OnDoAction(ActionType action)
    {
        Debug.Log("Выполнить действие: " + action);

        switch (action)
        {
            case ActionType.MoveRight:
                onMoving = direction = true;
                break;
            case ActionType.MoveUp:
                onMoving = direction = true;
                break;
            case ActionType.MoveLeft:
                direction = false;
                onMoving = true;
                break;
            case ActionType.MoveDown:
                direction = false;
                onMoving = true;
                break;
            case ActionType.Switch:
                onMoving = false;
                isHorizontalTurretsSelected = !isHorizontalTurretsSelected;
                print(isHorizontalTurretsSelected);//
                break;
            case ActionType.None:
                onMoving = false;
                break;
            default:
                Debug.LogError("Неверно настроенное действие для испольуемого объекта");
                break;
        }
    }
    //Передавать команды в виде их названий через кнопки нельзя, 
    //поэтому возвращаемся в скрипт Character и создадим еще один, похожий на OnDoAction метод, в 
    //который будем передавать числовой номер команды actionId.
    public void OnDoAction(int actionId)
    {
        OnDoAction((ActionType)actionId);
    }

    Vector3 currentDirection;
    [Range(0, 10)]
    public float moveSpeed = 1;
    void FixedUpdate()
    {
        if (isHorizontalTurretsSelected)
        {
            /*for transparent buttons
             * Color c = renderer.material.color;
            c.a = f;
            */
            triggerUp.enabled = true;
            triggerDown.enabled = true;
            triggerLeft.enabled = false;
            triggerRight.enabled = false;
            rigidbodyTurretsCurrent = getRigidbody(PathTurretsHorizontal);
        }
        else
        {
            triggerUp.enabled = false;
            triggerDown.enabled = false;
            triggerLeft.enabled = true;
            triggerRight.enabled = true;
            rigidbodyTurretsCurrent = getRigidbody(PathTurretsVertical);
        }
        
        if (onMoving)
        {
            if (isHorizontalTurretsSelected)
            {
                if (direction)
                    currentDirection = Vector3.up;
                else
                    currentDirection = Vector3.down;
            }
            else
            {
                if (direction)
                    currentDirection = Vector3.right;
                else
                    currentDirection = Vector3.left;
            }
            rigidbodyTurretsCurrent.AddForce(currentDirection * moveSpeed);
        }
        else
        {
            rigidbodyTurretsCurrent.Sleep();//stops addforce by disabling rigidbidy
            rigidbodyTurretsCurrent.WakeUp();//enable rigidbody again
        }
    }
}
