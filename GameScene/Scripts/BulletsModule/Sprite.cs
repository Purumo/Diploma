using UnityEngine;

namespace GameScene.BulletsModule
{
    public delegate void ActivateBonuseFunc();
    public class Sprite : MonoBehaviour
    {
        [HideInInspector] public ActivateBonuseFunc activateFunc;

        [Range(0, 60)] public float waitCollectionTime = 0f;

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