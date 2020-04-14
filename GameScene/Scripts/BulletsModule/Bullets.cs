using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public interface IBonuseController
    {
        void Activate();
    }
    public interface IMovableBullet//??
    {
        void Seek(Vector3 direction, Bullet bullet);
    }
    [System.Serializable]
    public class Bullet
    {
        [HideInInspector] public Text CountdownText;
        public string Name;
        public GameObject Object;
        public GameObject ImpactEffect;
        public float ExplosionRadius;
        public float Speed;
        public int Damage;
        public float LifeTime;

        public float SlowEnemyAmount;
        public float SlowActionTime;

        public GameObject Sprite;
        public GameObject Icon;

        public Bullet(Bullet bullet)
        {
            Name = bullet.Name;
            Object = bullet.Object;
            ImpactEffect = bullet.ImpactEffect;
            Speed = bullet.Speed;
            Damage = bullet.Damage;
            ExplosionRadius = bullet.ExplosionRadius;
            SlowEnemyAmount = bullet.SlowEnemyAmount;
            SlowActionTime = bullet.SlowActionTime;

            CountdownText = null;
            LifeTime = bullet.LifeTime;//

            Sprite = bullet.Sprite;
            Icon = bullet.Icon;
        }
    }
}
