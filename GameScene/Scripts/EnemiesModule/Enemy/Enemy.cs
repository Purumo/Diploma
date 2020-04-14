using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.EnemiesModule
{
    public class Enemy : MonoBehaviour
    {
        private Image healthBar;
        private float varHealth;
        [HideInInspector] public float slowActionTime;
        [HideInInspector] public float varSpeed;

        public float speed = 2.5f;
        public float health = 13;

        void Start()
        {
            healthBar = GetComponentsInChildren<Image>()[1];
            varHealth = health;
            varSpeed = speed;
        }

        public void TakeDamage(int amount)
        {
            varHealth -= amount;

            healthBar.fillAmount = varHealth / health;

            if(varHealth <= 0)
            {
                Die();
            }
        }
        public void Slow(float pct, float slowTime) //0 <= pct <= 1 : 0 - normal speed, 1 - stop
        {
            varSpeed = speed * (1f - pct);
            //print("varSpeed: " + varSpeed);
            slowActionTime = slowTime;
            //print("slowActionTime: " + slowActionTime);
        }
        void Die()
        {
            WaveSpawner.EnemiesAlive--;
            Destroy(gameObject);
        }
    }
}
