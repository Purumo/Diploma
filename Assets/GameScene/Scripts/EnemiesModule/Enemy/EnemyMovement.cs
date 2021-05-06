using UnityEngine;

namespace GameScene.EnemiesModule
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform currentTarget;
        private int wavepointIndex = 0;
        private Vector2 direction;

        private Collider2D cldr;
        private Enemy enemy;

        private int[] moveTrajectory;
        void Start()
        {
            //необходимо выбрать точку из movementPoints с индексом первого элемента случайно выбранной строки (траектории)
            //вдобавок по этой выбранной траектории и пойдёт новый спрайт

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
                if (Vector2.Distance(transform.position, currentTarget.position) <= 0.4f)
                {
                    GetNextWaypoint();
                }
            }
            else
            {
                cldr.enabled = false;
                Destroy(gameObject, enemy.lifeTimeAtEndPath);
            }
        }        
        void GetNextWaypoint()
        {
            wavepointIndex++;
            if (wavepointIndex < moveTrajectory.Length)
            {
                currentTarget = EnemyMovementPaths.movementPoints[moveTrajectory[wavepointIndex]];
            }
        }
    }
}
