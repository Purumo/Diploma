using UnityEngine;

namespace GameScene.EnemiesModule
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform currentTarget;
        private int wavepointIndex = 0;
        private Vector3 direction;

        private Collider2D cldr;

        private Enemy enemy;
        private float slowCountdown;

        [HideInInspector] public int[] moveTrajectory;

        void Start()
        {
            moveTrajectory = EnemyMovementPaths.moveTrajectories[Random.Range(0, EnemyMovementPaths.moveTrajectories.Length)];
            currentTarget = EnemyMovementPaths.movementPoints[moveTrajectory[0]];

            cldr = GetComponent<Collider2D>();
            enemy = GetComponent<Enemy>();
            slowCountdown = enemy.slowActionTime;
        }

        void Update()
        {
            if (slowCountdown <= 0)// && enemy.slowActionTime != 0)//?
            {
                enemy.varSpeed = enemy.speed;
                slowCountdown = enemy.slowActionTime;
            }
            else
            {
                slowCountdown -= Time.deltaTime;
            }
            if (wavepointIndex < moveTrajectory.Length)
            {
                //if (enemy.varSpeed == 0)
                //{
                //    cldr.enabled = false;
                //}
                //else
                //{
                    //cldr.enabled = true;

                    direction = currentTarget.position - transform.position;
                    transform.Translate(direction.normalized * enemy.varSpeed * Time.deltaTime);

                    //print(enemy.varSpeed);
                //}
                if (Vector3.Distance(transform.position, currentTarget.position) <= 0.4f)
                {
                    GetNextWaypoint();
                }
            }
            else
            {
                cldr.enabled = false;

                Destroy(gameObject, 60f);
            }
        }
        
        void GetNextWaypoint()
        {
            wavepointIndex++;
            if (wavepointIndex >= moveTrajectory.Length)
            {
                WaveSpawner.EnemiesAlive--;
                return;
            }
            else
            {
                currentTarget = EnemyMovementPaths.movementPoints[moveTrajectory[wavepointIndex]];
            }
        }
    }
}
