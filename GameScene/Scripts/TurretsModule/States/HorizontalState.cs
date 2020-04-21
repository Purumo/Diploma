using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameScene.TurretsModule
{
    public class HorizontalState : TurretsState
    {
        private Rigidbody2D rigidbodyTurretsHorizontal;//??

        public HorizontalState(TurretsController controller) : base(controller)
        {
            rigidbodyTurretsHorizontal = controller.turretsHorizontal;
            controller.varSpeed = controller.moveTurretsSpeed;

            controller.verticalCanvasGroup.alpha = 0.5f;
            controller.horizCanvasGroup.alpha = 1f;
        }
        public override void Move()
        {
            rigidbodyTurretsHorizontal.velocity = controller.varSpeed * currentMoveDirection;
        }
        public override void Sleep() => controller.varSpeed = 0;
        public override void Up()
        {
            controller.varSpeed = controller.moveTurretsSpeed;
            currentMoveDirection = Vector3.up;
        }
        public override void Down()
        {
            controller.varSpeed = controller.moveTurretsSpeed;
            currentMoveDirection = Vector3.down;
        }
        public override void Right() { }
        public override void Left() { }
        public override void Shoot()
        {
            //MonoBehaviour.print(horizontalCountdownTexts.Length);
            Shoot(TurretsController.GetInstance().leftTurret);
            Shoot(TurretsController.GetInstance().rightTurret);
        }
        public override IEnumerator ShowInfoWithWaiting(string info)
        {
            controller.rightTurret.coundownText.text = info;
            controller.leftTurret.coundownText.text = info;
            yield return new WaitForSeconds(controller.showReloadTime);
            controller.rightTurret.coundownText.text = "";
            controller.leftTurret.coundownText.text = "";
        }
        public override void Switching()
        {
            //ShowInfo("");
            controller.SetState(new VerticalState(controller));
        }

    }
}