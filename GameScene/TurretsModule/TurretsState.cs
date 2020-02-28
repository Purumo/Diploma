using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameScene.TurretsModule
{
    public abstract class TurretsState
    {
        protected Vector3 currentDirection;

        //protected static List<RaycastHit2D> shootsRaycast = new List<RaycastHit2D>();
        //protected ContactFilter2D raycastFilter;
        protected float fireCountdown = 0f;

        protected TurretsController controller;
        public TurretsState(TurretsController controller) => this.controller = controller;

        public void BulletShoot(GameObject bulletPrefab, Transform bulletPool, Transform firePoint, Vector3 dir)
        {
            GameObject bulletGO = GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, bulletPool);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Seek(dir);
        }

        public abstract void Move();
        public abstract void Sleep();
        public abstract void Up();
        public abstract void Down();
        public abstract void Right();
        public abstract void Left();
        //public abstract int FindEnemy();
        public abstract void Shoot();
        public abstract void Switching();
    }
}
