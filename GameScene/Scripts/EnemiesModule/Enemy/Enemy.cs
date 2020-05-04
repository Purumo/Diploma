using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.EnemiesModule
{
    public class Enemy : MonoBehaviour
    {
        private Image healthBar;
        private float varHealth;

        //[HideInInspector] public float slowActionTime;
        [HideInInspector] public float varSpeed;
        [HideInInspector] public float lifeTimeAtEndPath = 60f;

        public float speed = 0f;
        public float health = 0f;
        public int killPoints = 0;

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
        void Die()
        {
            PlayerStatistic.GetInstance().KillEnemy(killPoints);
            
            Destroy(gameObject);
        }
        public void ChangeSpeed(float pct, float slowTime) //0 <= pct <= 1 : 0 - normal speed, 1 - stop
        {
            if (pct != 0 && slowTime != 0)
            {
                StartCoroutine(Slow(pct, slowTime));
            }
        }
        private IEnumerator Slow(float pct, float slowTime)
        {
            varSpeed = speed * (1f - pct);
            yield return new WaitForSeconds(slowTime);
            varSpeed = speed;
        }
    }
}
