using GameScene.EnemiesModule;
using GameScene.TurretsModule;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class TimeBonusController : MonoBehaviour, IBonuseController
    {
        private float countdownActionTime = 0f;

        private GameObject icon;
        
        public float timeBetweenBonuse;
        public float actionTime;

        public Bullet bullet;

        void Start()
        {
            InvokeRepeating("SpawnBonuse", timeBetweenBonuse, timeBetweenBonuse);
        }
        void Update()
        {
            if (countdownActionTime > 0)
            {
                countdownActionTime -= Time.deltaTime;
                bullet.CountdownText.text = (Mathf.Round(countdownActionTime) + 1).ToString();

                if (countdownActionTime <= 0f && icon)
                {
                    Destroy(icon);
                    BulletsController.GetInstance().RemoveBonusAction(bullet);
                }
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
                BulletsController.GetInstance().AddNewBonusAction(bullet);

                icon = Instantiate(bullet.Icon, BulletsController.GetInstance().bonusesPanelUI);
            }

            icon.transform.SetAsFirstSibling();

            countdownActionTime = actionTime - 1;

            bullet.CountdownText = icon.GetComponentInChildren<Text>();
        }
    }
}