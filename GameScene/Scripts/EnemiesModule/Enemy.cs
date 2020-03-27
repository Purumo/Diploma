using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.EnemiesModule
{
    public class Enemy : MonoBehaviour
    {
        private Transform currentTarget;
        private int wavepointIndex = 0;
        private Vector3 direction;
        private Image healthBar;
        private float health;


        [HideInInspector] public int[] moveTrajectory;
        public float moveSpeed = 1f;
        public float startHealth = 13;

        void Start()
        {
            moveTrajectory = EnemyMovement.moveTrajectories[Random.Range(0, EnemyMovement.moveTrajectories.Length)];
            currentTarget = EnemyMovement.movementPoints[moveTrajectory[0]];

            healthBar = GetComponentsInChildren<Image>()[1];
            health = startHealth;
        }

        void Update()
        {
            if (wavepointIndex < moveTrajectory.Length)
            {
                direction = currentTarget.position - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, currentTarget.position) <= 0.4f)
                {
                    GetNextWaypoint();
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().Sleep();
                GetComponent<Collider2D>().enabled = false;

                Destroy(gameObject, 60f);
            }
        }
        
        void GetNextWaypoint()
        {
            wavepointIndex++;
            if (wavepointIndex >= moveTrajectory.Length)
            {
                return;
            }
            else
            {
                currentTarget = EnemyMovement.movementPoints[moveTrajectory[wavepointIndex]];
            }
        }

        public void TakeDamage(int amount)
        {
            health -= amount;

            healthBar.fillAmount = health / startHealth;

            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
