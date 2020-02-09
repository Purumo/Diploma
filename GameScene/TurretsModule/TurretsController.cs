using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameScene.Helpers;

namespace GameScene.TurretsModule
{
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

        protected static Rigidbody2D rigidbodyTurretsHorizontal, rigidbodyTurretsVertical;
        public static float MoveSpeed = 3f;

        Context context;
        void Awake()
        {
            rigidbodyTurretsHorizontal = BasicUnityFunctions.GetRigidbody(PathTurretsHorizontal);
            rigidbodyTurretsVertical = BasicUnityFunctions.GetRigidbody(PathTurretsVertical);
        }
        private void OnEnable()
        {
            EventTrigger.Entry entryNone = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            entryNone.callback.AddListener((data) => { context.NoneAction(); });

            EventTriggerAdder.AddTrigger(PathButtonUp, EventTriggerType.PointerDown, (data) => { context.ClickUp(); });
            EventTriggerAdder.AddTrigger(PathButtonUp, entryNone);
            EventTriggerAdder.AddTrigger(PathButtonDown, EventTriggerType.PointerDown, (data) => { context.ClickDown(); });
            EventTriggerAdder.AddTrigger(PathButtonDown, entryNone);
            EventTriggerAdder.AddTrigger(PathButtonRight, EventTriggerType.PointerDown, (data) => { context.ClickRight(); });
            EventTriggerAdder.AddTrigger(PathButtonRight, entryNone);
            EventTriggerAdder.AddTrigger(PathButtonLeft, EventTriggerType.PointerDown, (data) => { context.ClickLeft(); });
            EventTriggerAdder.AddTrigger(PathButtonLeft, entryNone);
            EventTriggerAdder.AddTrigger(PathButtonSwitch, EventTriggerType.PointerEnter, (data) => { context.ClickSwitch(); });
        }
        void Start()
        {
            context = new Context(new HorizontalState());
        }
        void FixedUpdate()
        {
            context.MoveAction();
        }
        /*void OnDisable()
        {
            EventTriggerAdder.ClearAllTriggers(PathButtonUp);
            EventTriggerAdder.ClearAllTriggers(PathButtonDown);
            EventTriggerAdder.ClearAllTriggers(PathButtonRight);
            EventTriggerAdder.ClearAllTriggers(PathButtonLeft);
            EventTriggerAdder.ClearAllTriggers(PathButtonSwitch);
        }*/

        public class Context
        {
            private TurretsState _turretsState = null;
            public Context(TurretsState turretsState) => this.ChangeState(turretsState);

            public void ChangeState(TurretsState turretsState)
            {
                _turretsState = turretsState;
                _turretsState.SetContext(this);//
            }
            public void MoveAction() => _turretsState.Move();
            public void NoneAction() => _turretsState.Sleep();
            public void ClickUp() => _turretsState.Up();
            public void ClickDown() => _turretsState.Down();
            public void ClickRight() => _turretsState.Right();
            public void ClickLeft() => _turretsState.Left();
            public void ClickSwitch()
            {
                _turretsState.Sleep();
                _turretsState.Switching();
            }
            public void ClickShoot() { }
        }
        public abstract class TurretsState
        {
            protected Context _context;
            protected Vector3 currentDirection;
            protected Rigidbody2D rigidbodyTurretsCurrent;

            public void SetContext(Context context) => _context = context;
            public void Move()
            {
                if (!rigidbodyTurretsCurrent.IsSleeping())
                    rigidbodyTurretsCurrent.AddForce(currentDirection * MoveSpeed);
            }
            public void Sleep() => rigidbodyTurretsCurrent.Sleep();
            public abstract void Up();
            public abstract void Down();
            public abstract void Right();
            public abstract void Left();
            public abstract void Switching();
            public abstract void Shoot();
        }
        class HorizontalState : TurretsState
        {
            public HorizontalState()
            {
                rigidbodyTurretsCurrent = rigidbodyTurretsHorizontal;
                rigidbodyTurretsCurrent.WakeUp();
            }
            public override void Up()
            {
                rigidbodyTurretsCurrent.WakeUp();
                currentDirection = Vector3.up;
            }
            public override void Down()
            {
                rigidbodyTurretsCurrent.WakeUp();
                currentDirection = Vector3.down;
            }
            public override void Right() { }
            public override void Left() { }
            public override void Switching() => _context.ChangeState(new VerticalState());
            public override void Shoot() { }
        }
        class VerticalState : TurretsState
        {
            public VerticalState()
            {
                rigidbodyTurretsCurrent = rigidbodyTurretsVertical;
                rigidbodyTurretsCurrent.WakeUp();
            }
            public override void Up() { }
            public override void Down() { }
            public override void Right()
            {
                rigidbodyTurretsCurrent.WakeUp();
                currentDirection = Vector3.right;
            }
            public override void Left()
            {
                rigidbodyTurretsCurrent.WakeUp();
                currentDirection = Vector3.left;
            }
            public override void Switching() => _context.ChangeState(new HorizontalState());
            public override void Shoot() { }
        }
    }
}
