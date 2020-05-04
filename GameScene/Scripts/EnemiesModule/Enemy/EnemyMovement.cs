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

        [HideInInspector] public int[] moveTrajectory;

        void Start()
        {
            moveTrajectory = EnemyMovementPaths.moveTrajectories[Random.Range(0, EnemyMovementPaths.moveTrajectories.Length)];
            currentTarget = EnemyMovementPaths.movementPoints[moveTrajectory[0]];

            cldr = GetComponent<Collider2D>();
            enemy = GetComponent<Enemy>();
        }

        void Update()
        {
            if (wavepointIndex < moveTrajectory.Length)
            {
                direction = currentTarget.position - transform.position;
                transform.Translate(direction.normalized * enemy.varSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, currentTarget.position) <= 0.4f)
                {
                    GetNextWaypoint();
                }
            }
            else
            {
                cldr.enabled = false;
                Destroy(gameObject, enemy.lifeTimeAtEndPath);//sec
            }
        }
        
        void GetNextWaypoint()
        {
            wavepointIndex++;
            if (wavepointIndex >= moveTrajectory.Length)
            {
                WaveSpawner.enemiesAlive--;
                return;
            }
            else
            {
                currentTarget = EnemyMovementPaths.movementPoints[moveTrajectory[wavepointIndex]];
            }
        }
    }
}
