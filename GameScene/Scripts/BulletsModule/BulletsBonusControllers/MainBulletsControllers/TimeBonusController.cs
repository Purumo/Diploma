using GameScene.EnemiesModule;
using GameScene.TurretsModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class TimeBonusController : MonoBehaviour, IBonuseController
    {
        private float bonuseCountdown;
        private float countdownActionTime = 0f;

        private GameObject icon;
        
        public float timeBetweenBonuse;

        public float ActionTime;

        public Bullet bullet;

        void Start()
        {
            bonuseCountdown = timeBetweenBonuse;
        }
        void Update()
        {
            if (bonuseCountdown <= 0f)
            {
                SpawnBonuse();
                bonuseCountdown = timeBetweenBonuse;
            }
            bonuseCountdown -= Time.deltaTime;

            if (countdownActionTime <= 0f)
            {
                if (icon)
                {
                    BulletsController.GetInstance().ResetBullet();
                    Destroy(icon);
                }
            }
            else
            {
                bullet.CountdownText.text = (Mathf.Round(countdownActionTime) + 1).ToString();
                //print((Mathf.Round(countdownActionTime) + 1));///for removing
                countdownActionTime -= Time.deltaTime;
            }
        }
        void SpawnBonuse()
        {
            float xTo = TurretsController.GetInstance().rightTurret.gameObject.transform.position.x;
            float yTo = TurretsController.GetInstance().topTurret.gameObject.transform.position.y;
            Vector2 spawnPoint = WaveSpawner.SelectRandomRectangleSpawnPoint(-xTo, xTo, -yTo, yTo);

            GameObject spriteObject = Instantiate(bullet.Sprite, spawnPoint,
                Quaternion.identity, BulletsController.GetInstance().bonusesPool);
            Sprite sprite = spriteObject.GetComponent<Sprite>();
            sprite.activateFunc = delegate { Activate(); };
        }
        public void Activate()
        {
            if (!icon)
            {
                BulletsController.GetInstance().currentBullet = bullet;

                icon = Instantiate(bullet.Icon, BulletsController.GetInstance().bonusesPanelUI);
            }

            icon.transform.SetAsFirstSibling();

            bullet.CountdownText = icon.GetComponentInChildren<Text>();

            countdownActionTime = ActionTime - 1;
        }
    }
}