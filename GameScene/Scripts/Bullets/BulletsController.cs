using UnityEngine;
using GameScene.TurretsModule;
using System.Collections.Generic;
using System;

namespace GameScene.BulletsModule
{
    public class BulletsController : MonoBehaviour
    {
        private static BulletsController instance;

        [HideInInspector] public const int layerMaskEnemy = 1 << 10;

        [Header("Unity Setup Fields")]
        public Transform bulletsPool;
        public Transform effectsPool;

        public Transform bonusesPool;
        public Transform bonusesPanelUI;

        [Header("Standart bullet")]
        public Bullet standartBullet;

        [Header("Bonuses bullet")]
        public Bullet missileBullet;
        public Bullet freezingBullet;

        [HideInInspector] public Bullet currentBullet;

        void Start()
        {
            instance = this;

            currentBullet = new Bullet(standartBullet);
        }
        public static BulletsController GetInstance()
        {
            return instance;
        }
        public void ResetBullet()
        {
            currentBullet = new Bullet(standartBullet);
        }

        public static void Shoot(Turret turret)
        {
            Vector3 dir = turret.firePoint.position - turret.gameObject.transform.position;

            GameObject bullet = Instantiate(
                GetInstance().currentBullet.Object, turret.firePoint.position, turret.firePoint.rotation, GetInstance().bulletsPool);
            MovableBullet movableBullet = bullet.GetComponent<MovableBullet>();

            movableBullet.Seek(dir, GetInstance().currentBullet);
        }
    }
}
