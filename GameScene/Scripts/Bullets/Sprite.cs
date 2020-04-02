using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public delegate void ActivateFunc();
    public class Sprite : MonoBehaviour
    {
        [HideInInspector] public ActivateFunc activateFunc;

        public float waitCollectionTime = 4f;

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)//!hit.collider not working
                {
                    ClickedEvent();
                }
            }

            Destroy(gameObject, waitCollectionTime);
        }
        public void ClickedEvent()
        {
            activateFunc();
            
            Destroy(gameObject);
        }
    }
}