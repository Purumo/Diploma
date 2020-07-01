using GameScene.GameMaster.Statistic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.EnemiesModule
{
    public class Enemy : MonoBehaviour
    {
        private Image healthBar;
        private float varHealth;

        [HideInInspector] public float varSpeed;
        [HideInInspector] public float lifeTimeAtEndPath = 60f;

        [Range(0, 10)] public float speed = 0f;
        [Range(0, 100)] public float health = 0f;
        [Range(0, 500)] public int killPoints = 0;

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
        public void ChangeSpeed(float pct, float slowTime) 
        {
            if (pct != 0 && slowTime != 0)
            {
                StartCoroutine(Slow(pct, slowTime));
            }
        }
        private IEnumerator Slow(float pct, float slowTime)
        {
            varSpeed = speed * (100 - pct) / 100;
            yield return new WaitForSeconds(slowTime);
            varSpeed = speed;
        }
    }
}
