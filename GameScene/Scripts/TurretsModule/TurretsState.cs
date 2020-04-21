using UnityEngine;
using GameScene.BulletsModule;
using UnityEngine.UI;
using System.Collections;

namespace GameScene.TurretsModule
{
    public abstract class TurretsState
    {
        protected Vector3 currentMoveDirection;

        protected TurretsController controller;
        public TurretsState(TurretsController controller) => this.controller = controller;
        protected void Shoot(Turret turret)
        {
            Vector3 dir = turret.firePoint.position - turret.gameObject.transform.position;

            GameObject bullet = Object.Instantiate(
                BulletsController.GetInstance().currentBullet.Object, turret.firePoint.position, 
                turret.firePoint.rotation, BulletsController.GetInstance().bulletsPool);
            ClassicBullet movableBullet = bullet.GetComponent<ClassicBullet>();

            movableBullet.Seek(dir, BulletsController.GetInstance().currentBullet);
        }
        //protected IEnumerator ShowInfoWithWaiting(string info, Text textTurret0, Text textTurret1)
        //{
        //    textTurret0.text = info;
        //    textTurret1.text = info;
        //    yield return new WaitForSeconds(controller.showReloadTime);
        //    textTurret0.text = "";
        //    textTurret1.text = "";
        //}
        public abstract void Move();
        public abstract void Sleep();
        public abstract void Up();
        public abstract void Down();
        public abstract void Right();
        public abstract void Left();
        public abstract void Shoot();
        public abstract IEnumerator ShowInfoWithWaiting(string info);
        public abstract void Switching();
    }
}
