using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameScene.Helpers;

namespace GameScene.EnemiesModule
{
    public class EnemyController : MonoBehaviour
    {
        private Transform currentTarget;
        private int wavepointIndex = 0;
        private Vector3 direction;

        public int[] moveTrajectory;
        public float moveSpeed = 1f;

        void Start()
        {
            moveTrajectory = EnemyMovement.moveTrajectories[Random.Range(0, EnemyMovement.moveTrajectories.Length)];
            currentTarget = EnemyMovement.movementPoints[moveTrajectory[0]];
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
    }
}
