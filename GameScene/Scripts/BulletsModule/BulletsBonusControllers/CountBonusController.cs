using GameScene.EnemiesModule;
using GameScene.BulletsModule.PointerEnterExitAction;
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

        [Range(0, 600)] public float timeBetweenBonuse;
        [Range(0, 10)] public int actionCount;
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
            countdownActionCount = actionCount;
            bullet.countdownText = icon.GetComponentInChildren<Text>();
            bullet.countdownText.text = countdownActionCount.ToString() + " в";
        }
        public void OnPointerEnter(PointerEventData data)        {
            if (countdownActionCount > 0
                && BulletsController.GetInstance().GetBonusAtIndex(0) == bullet)
            {
                countdownActionCount--;
                bullet.countdownText.text = countdownActionCount.ToString() + " в";
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