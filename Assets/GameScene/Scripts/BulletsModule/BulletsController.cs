using UnityEngine;
using System.Collections.Generic;

namespace GameScene.BulletsModule
{
    public class BulletsController : MonoBehaviour
    {
        private static BulletsController instance;

        public const int layerMaskEnemy = 1 << 8;

        private List<Bullet> bonusBullets = new List<Bullet>();

        [Header("Unity Setup Fields")]
        public Transform bulletsPool;

        public Transform bonusesPool;
        public Transform bonusesPanelUI;

        [Header("Standart bullet")]
        public Bullet standartBullet;

        [HideInInspector] public Bullet currentBullet;

        void Start()
        {
            instance = this;

            currentBullet = new Bullet(standartBullet);
        }
        public static BulletsController GetInstance()
        {
            return instance;
        }
        public void AddNewBonusAction(Bullet bullet)
        {
            bonusBullets.Insert(0, bullet);

            currentBullet = new Bullet(bonusBullets[0]);
        }
        public void RemoveBonusAction(Bullet bulletToRemove)
        {
            int idxToRemove = bonusBullets.FindIndex(item => item == bulletToRemove);            
            bonusBullets.RemoveAt(idxToRemove);

            if (bonusBullets.Count != 0)
                currentBullet = new Bullet(bonusBullets[0]);
            else
                currentBullet = new Bullet(standartBullet);
        }
        public Bullet GetBonusAtIndex(int idx)
        {
            return bonusBullets[idx];
        }
    }
}
