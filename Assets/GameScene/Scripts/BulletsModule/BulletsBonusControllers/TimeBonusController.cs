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

        [Range(0, 600)] public float timeBetweenBonuse;
        [Range(0, 120)] public float actionTime;
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
                bullet.countdownText.text = (Mathf.Round(countdownActionTime) + 1).ToString() + " с";

                if (countdownActionTime <= 0f)
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

            GameObject spriteObject = Instantiate(bullet.sprite, spawnPoint,
                Quaternion.identity, BulletsController.GetInstance().bonusesPool);
            Sprite sprite = spriteObject.GetComponent<Sprite>();
            sprite.activateFunc = delegate { Activate(); };
        }
        public void Activate()
        {
            if (!icon)
            {
                BulletsController.GetInstance().AddNewBonusAction(bullet);

                icon = Instantiate(bullet.icon, BulletsController.GetInstance().bonusesPanelUI);
            }

            icon.transform.SetAsFirstSibling();
            countdownActionTime = actionTime - 1;
            bullet.countdownText = icon.GetComponentInChildren<Text>();
        }
    }
}