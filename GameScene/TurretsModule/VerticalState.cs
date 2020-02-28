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
            //raycastFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
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
            if (fireCountdown <= 0f)
            {
                BulletShoot(controller.bulletPrefab, controller.bulletPool, controller.topTurret.firePoint, Vector3.down);
                BulletShoot(controller.bulletPrefab, controller.bulletPool, controller.botTurret.firePoint, Vector3.up);

                fireCountdown = 1f / controller.fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        public override void Switching()
        {
            controller.SetState(new HorizontalState(controller));
        }
    }
}