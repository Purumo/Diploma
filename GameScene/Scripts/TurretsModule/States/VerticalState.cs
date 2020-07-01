using UnityEngine;

namespace GameScene.TurretsModule
{
    public class VerticalState : TurretsState
    {
        public VerticalState(TurretsController controller) : base(controller) 
        {
            controller.varSpeed = controller.moveTurretsSpeed;

            controller.horizButtonsCanvasGroup.alpha = 0.5f;
            controller.verticalButtonsCanvasGroup.alpha = 1f;
        }
        public override void Move()
        {
            controller.rigidbodyTurretsVertical.velocity = controller.varSpeed * currentMoveDirection;
        }
        public override void Sleep() => controller.varSpeed = 0;
        public override void Up() { }
        public override void Down() { }
        public override void Right()
        {
            currentMoveDirection = Vector2.right;
            controller.varSpeed = controller.moveTurretsSpeed;
        }
        public override void Left()
        {
            controller.varSpeed = controller.moveTurretsSpeed;
            currentMoveDirection = Vector2.left;
        }
        public override void Shoot()
        {
            Shoot(TurretsController.GetInstance().topTurret);
            Shoot(TurretsController.GetInstance().botTurret);
        }
        public override void Switching()
        {
            controller.SetState(new HorizontalState(controller));
        }
    }
}