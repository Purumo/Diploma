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
            rigidbodyTurretsVertical = controller.turretsVertical;//controller.turretsVertical.GetComponent<Rigidbody2D>();
            controller.varSpeed = controller.MoveTurretsSpeed;
            //rigidbodyTurretsVertical.WakeUp();
        }
        public override void Move()
        {
            //if (!rigidbodyTurretsVertical.IsSleeping())
            //{
                //need to try velocity for deleting move limits
                rigidbodyTurretsVertical.velocity = controller.varSpeed * currentDirection;
            //}
        }
        public override void Sleep() => controller.varSpeed = 0;//rigidbodyTurretsVertical.Sleep();
        public override void Up() { }
        public override void Down() { }
        public override void Right()
        {
            //rigidbodyTurretsVertical.WakeUp();
            currentDirection = Vector3.right;
            controller.varSpeed = controller.MoveTurretsSpeed;
        }
        public override void Left()
        {
            //rigidbodyTurretsVertical.WakeUp();
            controller.varSpeed = controller.MoveTurretsSpeed;
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