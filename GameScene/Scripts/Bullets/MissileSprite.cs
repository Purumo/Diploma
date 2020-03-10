using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class MissileSprite : MonoBehaviour
    {
        private Bullet bonuseBullet;
        ///private Text countdownText;

        private float waitCollectionCountdown = 4f;

        void Update()
        {
            if(waitCollectionCountdown <= 0f)
            {
                Destroy(gameObject);
            }
            waitCollectionCountdown -= Time.deltaTime;
            //countdownText.text = Mathf.Round(waitCollectionCountdown).ToString();

            if (Input.GetMouseButtonDown(0)) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    OnClick();
                }
            }
        }
        public void CallSprite(Bullet bonuseBullet)
        {
            this.bonuseBullet = bonuseBullet;
            //countdownText = this.bonuseBullet.icon.GetComponentInChildren<Text>();
        }
        public void OnClick()
        {
            GameObject icon = Instantiate(bonuseBullet.icon, BulletsController.GetInstance().bonusesPanelUI);
            BulletsController.GetInstance().currentBullet = bonuseBullet;

            Destroy(icon, bonuseBullet.actionTime);
            Destroy(gameObject);
        }
    }
}