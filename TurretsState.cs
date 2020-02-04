using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.State
{
    public class Context
    {
        private TurretsState _turretsState = null;
        public Context(TurretsState turretsState)
        {
            this.changeState(turretsState);
        }

        // Контекст позволяет изменять объект Состояния во время выполнения.
        public void changeState(TurretsState turretsState)
        {
            //Debug.Log($"Context: Transition to {_turretsState.GetType().Name}.");
            _turretsState = turretsState;
            _turretsState.SetContext(this);//
        }
        public void moveAction()
        {
            _turretsState.move();
        }
        public void noneAction()
        {
            _turretsState.sleep();
        }
        public void clickUp()
        {
            _turretsState.up();
        }
        public void clickDown()
        {
            _turretsState.down();
        }
        public void clickRight()
        {
            _turretsState.right();
        }
        public void clickLeft()
        {
            _turretsState.left();
        }
        public void clickSwitch()
        {
            _turretsState.sleep();
            _turretsState.switching();
        }
        public void clickShoot()
        {
        }
    }
    public abstract class TurretsState
    {
        protected const string
            PathTurretsHorizontal = "Turrets/Horizontal",
            PathTurretsVertical = "Turrets/Vertical";

        protected Context _context;
        protected float moveSpeed = 1;//??

        protected Vector3 currentDirection;
        protected Rigidbody2D rigidbodyTurretsCurrent;

        protected Rigidbody2D getRigidbody(string pathSceneObject)
        {
            Rigidbody2D objBody = null;
            GameObject rigidbodyHorizontal = GameObject.Find(pathSceneObject);
            if (objBody == null) objBody = rigidbodyHorizontal.GetComponent<Rigidbody2D>();
            return objBody;
        }
        public void SetContext(Context context)
        {
            _context = context;
        }
        public void move()
        {
            if(!rigidbodyTurretsCurrent.IsSleeping())
                rigidbodyTurretsCurrent.AddForce(currentDirection * moveSpeed);
        }
        public void sleep()
        {
            rigidbodyTurretsCurrent.Sleep();
        }
        public abstract void up();
        public abstract void down();
        public abstract void right();
        public abstract void left();
        public abstract void switching();
        public abstract void shoot();
    }
    class HorizontalState : TurretsState
    {
        public HorizontalState()
        {
            rigidbodyTurretsCurrent = getRigidbody(PathTurretsHorizontal);
            rigidbodyTurretsCurrent.WakeUp();
        }
        public override void up()
        {
            rigidbodyTurretsCurrent.WakeUp();
            currentDirection = Vector3.up;
        }
        public override void down()
        {
            rigidbodyTurretsCurrent.WakeUp();
            currentDirection = Vector3.down;
        }
        public override void right()
        {
        }
        public override void left()
        {
        }
        public override void switching()
        {
            _context.changeState(new VerticalState());
        }
        public override void shoot()
        {
        }
    }
    class VerticalState : TurretsState
    {
        public VerticalState()
        {
            rigidbodyTurretsCurrent = getRigidbody(PathTurretsVertical);
            rigidbodyTurretsCurrent.WakeUp();
        }
        public override void up()
        {
        }
        public override void down()
        {
        }
        public override void right()
        {
            rigidbodyTurretsCurrent.WakeUp();
            currentDirection = Vector3.right;
        }
        public override void left()
        {
            rigidbodyTurretsCurrent.WakeUp();
            currentDirection = Vector3.left;
        }
        public override void switching()
        {
            _context.changeState(new HorizontalState());
        }
        public override void shoot()
        {
        }
    }
}