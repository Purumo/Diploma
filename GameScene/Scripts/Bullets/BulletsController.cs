using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.TurretsModule;
using GameScene.EnemiesModule;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    /*public interface IBonuseSprite
    {
        void CallSprite(Bullet bonuseBullet);
    }*/
    public interface IMovableBullet
    {
        void Seek(Vector3 direction);
    }
    [System.Serializable]
    public struct Bullet
    {
        public readonly float LifeTime;
        [HideInInspector] public Text CountdownText;
        public GameObject Object;
        public GameObject Sprite;
        public GameObject Icon;
        public GameObject ImpactEffect;
        public float ActionTime;
        public float Speed;
        public int Damage;

        public Bullet(Bullet bullet)
        {
            Object = bullet.Object;
            Sprite = bullet.Sprite;
            Icon = bullet.Icon;
            ImpactEffect = bullet.ImpactEffect;
            ActionTime = bullet.ActionTime;
            Speed = bullet.Speed;
            Damage = bullet.Damage;

            CountdownText = null;            
            LifeTime = 10f;//
        }
    }
    public class BulletsController : MonoBehaviour
    {
        private static BulletsController instance;

        [HideInInspector] public const int layerMaskEnemy = 1 << 10;

        private float bonuseCountdown;
        private float timeBetweenBonuse = 4f;

        [Header("Unity Setup Fields")]
        public Transform bulletsPool;
        public Transform effectsPool;

        public Transform bonusesPool;
        public Transform bonusesPanelUI;

        [Header("Standart bullet")]
        public Bullet standartBullet;

        [Header("Missile bonuse bullet")]
        public Bullet missileBullet;
        //need to set borders
        public float explosionRadius = 1f;
        
        [HideInInspector] public Bullet currentBullet;

        void Awake()
        {
            instance = this;

            currentBullet = new Bullet(standartBullet);
            bonuseCountdown = WaveSpawner.countdown + timeBetweenBonuse;
        }
        void Update()
        {
            if (bonuseCountdown <= 0f)
            {
                SpawnBonuse();
                bonuseCountdown = timeBetweenBonuse;
            }
            bonuseCountdown -= Time.deltaTime;
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
            IMovableBullet movableBullet = bullet.GetComponent<IMovableBullet>();
            movableBullet.Seek(dir);
        }
        void SpawnBonuse()
        {
            float xTo = TurretsController.GetInstance().rightTurret.gameObject.transform.position.x;
            float yTo = TurretsController.GetInstance().topTurret.gameObject.transform.position.y;
            Vector2 spawnPoint = WaveSpawner.SelectRandomRectangleSpawnPoint(-xTo, xTo, -yTo, yTo);

            //GameObject bornedSprite = 
            Instantiate(missileBullet.Sprite, spawnPoint, Quaternion.identity, bonusesPool);
            //IBonuseSprite bonuseSprite = bornedSprite.GetComponent<IBonuseSprite>();
            //bonuseSprite.CallSprite(missileBullet);
        }
    }
}
