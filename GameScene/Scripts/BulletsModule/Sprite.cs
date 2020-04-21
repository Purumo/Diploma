using UnityEngine;

namespace GameScene.BulletsModule
{
    public delegate void ActivateFunc();
    public class Sprite : MonoBehaviour
    {
        [HideInInspector] public ActivateFunc activateFunc;

        public float waitCollectionTime = 0f;

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