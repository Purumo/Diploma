using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameScene.TurretsModule
{
    public class HorizontalState : TurretsState
    {
        public HorizontalState(TurretsController controller) : base(controller)
        {
            controller.varSpeed = controller.moveTurretsSpeed;

            controller.verticalCanvasGroup.alpha = 0.5f;
            controller.horizCanvasGroup.alpha = 1f;
        }
        public override void Move()
        {
            controller.turretsHorizontal.velocity = controller.varSpeed * currentMoveDirection;
        }
        public override void Sleep() => controller.varSpeed = 0;
        public override void Up()
        {
            controller.varSpeed = controller.moveTurretsSpeed;
            currentMoveDirection = Vector2.up;
        }
        public override void Down()
        {
            controller.varSpeed = controller.moveTurretsSpeed;
            currentMoveDirection = Vector2.down;
        }
        public override void Right() { }
        public override void Left() { }
        public override void Shoot()
        {
            Shoot(TurretsController.GetInstance().leftTurret);
            Shoot(TurretsController.GetInstance().rightTurret);
        }
        public override void Switching()
        {
            controller.SetState(new VerticalState(controller));
        }

    }
}