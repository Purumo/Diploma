using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameScene.TurretsModule
{
    public class VerticalState : TurretsState
    {
        private Rigidbody2D rigidbodyTurretsVertical;//??

        public VerticalState(TurretsController controller) : base(controller) 
        {
            rigidbodyTurretsVertical = controller.turretsVertical;
            controller.varSpeed = controller.moveTurretsSpeed;

            controller.horizCanvasGroup.alpha = 0.5f;
            controller.verticalCanvasGroup.alpha = 1f;
        }
        public override void Move()
        {
            rigidbodyTurretsVertical.velocity = controller.varSpeed * currentMoveDirection;
        }
        public override void Sleep() => controller.varSpeed = 0;
        public override void Up() { }
        public override void Down() { }
        public override void Right()
        {
            currentMoveDirection = Vector3.right;
            controller.varSpeed = controller.moveTurretsSpeed;
        }
        public override void Left()
        {
            controller.varSpeed = controller.moveTurretsSpeed;
            currentMoveDirection = Vector3.left;
        }
        public override void Shoot()
        {
            Shoot(TurretsController.GetInstance().topTurret);
            Shoot(TurretsController.GetInstance().botTurret);
        }
        public override IEnumerator ShowInfoWithWaiting(string info)
        {
            controller.topTurret.coundownText.text = info;
            controller.botTurret.coundownText.text = info;
            yield return new WaitForSeconds(controller.showReloadTime);
            controller.topTurret.coundownText.text = "";
            controller.botTurret.coundownText.text = "";
        }
        public override void Switching()
        {
            //ShowInfo("");
            controller.SetState(new HorizontalState(controller));
        }
    }
}