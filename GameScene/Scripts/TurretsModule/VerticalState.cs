using GameScene.BulletsModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene.TurretsModule
{
    public class VerticalState : TurretsState
    {
        private Rigidbody2D rigidbodyTurretsVertical;

        public VerticalState(TurretsController controller) : base(controller) 
        {
            rigidbodyTurretsVertical = controller.turretsVertical.GetComponent<Rigidbody2D>();
            rigidbodyTurretsVertical.WakeUp();
        }
        public override void Move()
        {
            if (!rigidbodyTurretsVertical.IsSleeping())
            {
                //need to try velocity for deleting move limits
                rigidbodyTurretsVertical.AddForce(currentDirection * controller.MoveTurretsSpeed);
            }
        }
        public override void Sleep() => rigidbodyTurretsVertical.Sleep();
        public override void Up() { }
        public override void Down() { }
        public override void Right()
        {
            rigidbodyTurretsVertical.WakeUp();
            currentDirection = Vector3.right;
        }
        public override void Left()
        {
            rigidbodyTurretsVertical.WakeUp();
            currentDirection = Vector3.left;
        }
        public override void Shoot()
        {
            BulletsController.Shoot(TurretsController.GetInstance().topTurret);
            BulletsController.Shoot(TurretsController.GetInstance().botTurret);
        }
        public override void Switching()
        {
            controller.SetState(new HorizontalState(controller));
        }
    }
}