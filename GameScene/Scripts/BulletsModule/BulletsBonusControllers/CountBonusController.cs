using GameScene.EnemiesModule;
using GameScene.GameMaster;
using GameScene.TurretsModule;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GameScene.BulletsModule
{
    public class CountBonusController : MonoBehaviour, IBonuseController
    {
        private float countdownActionCount = 0f;
        private GameObject icon;

        public PointerEnterExitHandler pointerEnterExitHandler;

        public float timeBetweenBonuse;
        public int actionCount;
        public Bullet bullet;
        void Start()
        {
            pointerEnterExitHandler.onPointerEnterEvent.AddListener(OnPointerEnter);
            pointerEnterExitHandler.onPointerExitEvent.AddListener(OnPointerExit);
            InvokeRepeating("SpawnBonuse", timeBetweenBonuse, timeBetweenBonuse);
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
            countdownActionCount = actionCount;
            bullet.CountdownText = icon.GetComponentInChildren<Text>();
            bullet.CountdownText.text = countdownActionCount.ToString();
        }

        //event on Shoot button
        public void OnPointerEnter(PointerEventData data)        {
            if (countdownActionCount > 0
                && BulletsController.GetInstance().GetBonusAtIndex(0) == bullet)
            {
                countdownActionCount--;
                bullet.CountdownText.text = countdownActionCount.ToString();
            }
        }
        public void OnPointerExit(PointerEventData data)        {
            if (countdownActionCount == 0 && icon)
            {
                Destroy(icon);
                BulletsController.GetInstance().RemoveBonusAction(bullet);
            }
        }
    }
}