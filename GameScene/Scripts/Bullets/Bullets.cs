using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    [System.Serializable]
    public class Bullet
    {
        public readonly float LifeTime;
        [HideInInspector] public Text CountdownText;
        public GameObject Object;
        public GameObject ImpactEffect;
        public float Speed;
        public int Damage;

        public float ExplosionRadius;
        public GameObject Sprite;
        public GameObject Icon;

        public Bullet(Bullet bullet)
        {
            Object = bullet.Object;
            ImpactEffect = bullet.ImpactEffect;
            Speed = bullet.Speed;
            Damage = bullet.Damage;

            CountdownText = null;            
            LifeTime = 10f;//

            ExplosionRadius = bullet.ExplosionRadius;
            Sprite = bullet.Sprite;
            Icon = bullet.Icon;
        }
    }
}
