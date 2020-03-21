using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class MissileController : MonoBehaviour
    {
        private static MissileController instance;

        private GameObject icon;
        private float countdownActionTime = 0f;

        [HideInInspector] public Bullet bullet;

        void Start()
        {
            instance = this;

            bullet = new Bullet(BulletsController.GetInstance().missileBullet);
        }
        protected void Update()
        {
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
                print((Mathf.Round(countdownActionTime) + 1));///
                countdownActionTime -= Time.deltaTime;
            }
        }
        public void Activate()
        {
            if (!icon)
            {
                BulletsController.GetInstance().currentBullet = bullet;

                icon = Instantiate(bullet.Icon, BulletsController.GetInstance().bonusesPanelUI);
            }
            bullet.CountdownText = icon.GetComponentInChildren<Text>();

            countdownActionTime = bullet.ActionTime - 1;
        }
        public static MissileController GetInstance()
        {
            return instance;
        }
    }
}