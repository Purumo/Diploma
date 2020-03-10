using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.TurretsModule;
using GameScene.EnemiesModule;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    //[System.Serializable]
    //public struct Bullet
    //{
    //    public GameObject gameObject;
    //    //public GameObject impactEffect;
    //}
    [System.Serializable]
    public struct Bullet
    {
        public GameObject gameObject;
        public GameObject sprite;
        public GameObject icon;
        public float actionTime;
        //public Text countdownText;
    }
    public class BulletsController : MonoBehaviour
    {
        private static BulletsController instance;

        [HideInInspector]
        public Bullet currentBullet;
        private static Turret turret;
        private static Transform bulletsPool;

        private float countdown;
        private float timeBetweenMissileBonuse = 6f;//

        public Transform bonusesPool;
        public Transform bonusesPanelUI;/// 

        public Bullet standartBullet;
        public Bullet missileBullet;

        void Awake()
        {
            instance = this;

            currentBullet = standartBullet;
            bulletsPool = transform;
            countdown = WaveSpawner.countdown + timeBetweenMissileBonuse;
        }
        void Update()
        {
            if (countdown <= 0f)
            {
                SpawnMissileBonuse();
                countdown = timeBetweenMissileBonuse;
            }

            countdown -= Time.deltaTime;

            //missileCountdownText.text = Mathf.Round(countdown).ToString();
        }
        void SpawnMissileBonuse()//
        {
            float xTo = TurretsController.GetInstance().rightTurret.gameObject.transform.position.x;
            float yTo = TurretsController.GetInstance().topTurret.gameObject.transform.position.y;
            Vector2 spawnPoint = WaveSpawner.SelectRandomRectangleSpawnPoint(-xTo, xTo, -yTo, yTo);

            GameObject bornedSprite = Instantiate(missileBullet.sprite, spawnPoint, Quaternion.identity, bonusesPool);
            MissileSprite missileSprite = bornedSprite.GetComponent<MissileSprite>();
            missileSprite.CallSprite(missileBullet);
        }
        public static BulletsController GetInstance()
        {
            return instance;
        }
        /*public static void EnableMissileBonuse()
        {
            currentBullet = missileBullet;
        }*/

        //standart bullet functions
        public static void Call(Turret turret)
        {
            BulletsController.turret = turret;
            Shoot();
        }
        static void Shoot()
        {
            Vector3 dir = turret.firePoint.position - turret.gameObject.transform.position;

            GameObject bullet = Instantiate(
                GetInstance().currentBullet.gameObject, turret.firePoint.position, turret.firePoint.rotation, bulletsPool);
            IMovableBullet movableBullet = bullet.GetComponent<IMovableBullet>();
            movableBullet.Seek(dir, GetInstance().currentBullet);
        }
        public void ResetBullet()
        {
            currentBullet = standartBullet;
        }
    }
}
