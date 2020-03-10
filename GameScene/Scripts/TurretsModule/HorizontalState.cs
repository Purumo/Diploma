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
            rigidbodyTurretsHorizontal = controller.turretsHorizontal.GetComponent<Rigidbody2D>();
            rigidbodyTurretsHorizontal.WakeUp();
        }
        public override void Move()
        {
            if (!rigidbodyTurretsHorizontal.IsSleeping())
            {
                //need to try velocity for deleting move limits
                rigidbodyTurretsHorizontal.AddForce(currentDirection * controller.MoveTurretsSpeed);
            }
        }
        public override void Sleep() => rigidbodyTurretsHorizontal.Sleep();
        public override void Up()
        {
            rigidbodyTurretsHorizontal.WakeUp();
            currentDirection = Vector3.up;
        }
        public override void Down()
        {
            rigidbodyTurretsHorizontal.WakeUp();
            currentDirection = Vector3.down;
        }
        public override void Right() { }
        public override void Left() { }
        public override void Shoot()
        {
            BulletShoot(TurretsController.GetInstance().leftTurret);
            BulletShoot(TurretsController.GetInstance().rightTurret);
        }
        public override void Switching()
        {
            controller.SetState(new VerticalState(controller));
        }
    }
}