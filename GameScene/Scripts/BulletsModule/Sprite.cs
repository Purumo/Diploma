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
            Destroy(gameObject, waitCollectionTime);
        }
        public void OnMouseDown()
        {
            activateFunc();
            
            Destroy(gameObject);
        }
    }
}