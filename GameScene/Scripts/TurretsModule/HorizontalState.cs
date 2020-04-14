using GameScene.BulletsModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene.TurretsModule
{
    public class HorizontalState : TurretsState
    {
        private Rigidbody2D rigidbodyTurretsHorizontal;

        public HorizontalState(TurretsController controller) : base(controller)
        {
            rigidbodyTurretsHorizontal = controller.turretsHorizontal;//.GetComponent<Rigidbody2D>();
            controller.varSpeed = controller.MoveTurretsSpeed;
            //rigidbodyTurretsHorizontal.WakeUp();

            controller.verticalCanvasGroup.alpha = 0.5f;
            controller.horizCanvasGroup.alpha = 1f;
        }
        public override void Move()
        {
            //if (!rigidbodyTurretsHorizontal.IsSleeping())
            //{
                rigidbodyTurretsHorizontal.velocity = controller.varSpeed * currentDirection;
            //}
        }
        public override void Sleep() => controller.varSpeed = 0;//rigidbodyTurretsHorizontal.Sleep();
        public override void Up()
        {
            //rigidbodyTurretsHorizontal.WakeUp();
            controller.varSpeed = controller.MoveTurretsSpeed;
            currentDirection = Vector3.up;
        }
        public override void Down()
        {
            //rigidbodyTurretsHorizontal.WakeUp();
            controller.varSpeed = controller.MoveTurretsSpeed;
            currentDirection = Vector3.down;
        }
        public override void Right() { }
        public override void Left() { }
        public override void Shoot()
        {
            BulletsController.Shoot(TurretsController.GetInstance().leftTurret);
            BulletsController.Shoot(TurretsController.GetInstance().rightTurret);
        }
        public override void Switching()
        {
            controller.SetState(new VerticalState(controller));
        }
    }
}