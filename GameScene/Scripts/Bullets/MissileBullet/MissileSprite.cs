using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class MissileSprite : MonoBehaviour//, IBonuseSprite
    {
        //private Bullet bullet;
        private float waitCollectionTime = 4f;

        void Update()
        {
            Destroy(gameObject, waitCollectionTime);
            
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
        /*public void CallSprite(Bullet bonuseBullet)
        {
            bullet = new Bullet(bonuseBullet);
        }*/
        public void OnClick()
        {
            MissileController.GetInstance().Activate();
            
            Destroy(gameObject);
        }
    }
}