using UnityEngine;
using GameScene.BulletsModule;

namespace GameScene.TurretsModule
{
    public abstract class TurretsState
    {
        protected Vector2 currentMoveDirection;

        protected TurretsController controller;
        public TurretsState(TurretsController controller) => this.controller = controller;
        protected void Shoot(Turret turret)
        {
            Vector2 dir = turret.firePoint.position - turret.gameObject.transform.position;

            GameObject bullet = Object.Instantiate(
                BulletsController.GetInstance().currentBullet.gameObj, turret.firePoint.position, 
                turret.firePoint.rotation, BulletsController.GetInstance().bulletsPool);
            ClassicBullet movableBullet = bullet.GetComponent<ClassicBullet>();

            movableBullet.Seek(dir, BulletsController.GetInstance().currentBullet);
        }
        public abstract void Move();
        public abstract void Sleep();
        public abstract void Up();
        public abstract void Down();
        public abstract void Right();
        public abstract void Left();
        public abstract void Shoot();
        public abstract void Switching();
    }
}
