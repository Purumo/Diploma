using GameScene.EnemiesModule;
using GameScene.TurretsModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class CountBonusController : MonoBehaviour, IBonuseController
    {
        private float bonuseCountdown;
        private float countdownActionCount = 0f;

        private GameObject icon;
        
        public float timeBetweenBonuse = 4f;

        public int ActionCount = 3;

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
            countdownActionCount = ActionCount;

            bullet.CountdownText = icon.GetComponentInChildren<Text>();
            bullet.CountdownText.text = countdownActionCount.ToString();
        }

        //event trigger on Shoot button
        public void CountdownAction()
        {
            if (countdownActionCount > 0)
            {
                countdownActionCount--;
                bullet.CountdownText.text = countdownActionCount.ToString();

                if (countdownActionCount == 0)
                {
                    if (icon)
                    {
                        BulletsController.GetInstance().ResetBullet();
                        Destroy(icon);
                    }
                }
            }
        }
    }
}