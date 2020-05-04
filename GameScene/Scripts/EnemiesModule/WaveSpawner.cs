using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.EnemiesModule
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        public int count;
        public float rate;
    }
    public class WaveSpawner : MonoBehaviour
    {
        private int waveIndex = 0;

        public static int enemiesAlive;// = 0;

        public static int currentWaveCount;
        //public static bool currentWaveSpawned;

        private float nextWaveCountdown = 0;
        public Text nextWaveCountdownText;

        //public Text waveCountdownText;
        public BoxCollider2D movementBorder;

        public float timeBetweenWaves = 5.4f;//??
        public Wave[] waves;

        void Start()
        {
            enemiesAlive = 0;
            currentWaveCount = 0;

            //currentKilledEnemyWaveIdx = 0;
            //currentWaveSpawned = false;
        }
        void Update()
        {
            if (enemiesAlive > 0)
            {
                return;                
            }

            //if (currentWaveSpawned)// & enemiesAlive == 0
            //{
            //    PlayerStatistic.GetInstance().RoundPassed();
            //    currentWaveSpawned = false;
            //}

            if (nextWaveCountdown <= 0f)
            {
                PlayerStatistic.GetInstance().nextWaveCountdownPanel.SetActive(false);
                PlayerStatistic.GetInstance().pointsPerRoundPanel.SetActive(true);

                StartCoroutine(SpawnWave());
                nextWaveCountdown = timeBetweenWaves;
                return;
            }
            else
            {
                nextWaveCountdownText.text = Mathf.Round(nextWaveCountdown).ToString();//string.Format("{0:00.00}", countdown);???
            }
            nextWaveCountdown -= Time.deltaTime;
        }

        IEnumerator SpawnWave()
        {
            waveIndex = Random.Range(0, waves.Length);
            Wave wave = waves[waveIndex];

            currentWaveCount = wave.count;
            for (int i = 0; i < currentWaveCount; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);//с заданной частотой появления
            }
            //currentWaveSpawned = true;
        }
        void SpawnEnemy(GameObject enemy)
        {
            //необходимо выбрать точку из movementPoints с индексом первого элемента случайно выбранной строки (траектории)
            //вдобавок по этой выбранной траектории и пойдёт новый спрайт
            //newEnemy = (GameObject)spritesFromResource[Random.Range(0, spritesFromResource.Length)];

            float xTo = (movementBorder.offset.x + movementBorder.size.x / 2) * movementBorder.transform.localScale.x;
            float yTo = (movementBorder.offset.y + movementBorder.size.y / 2) * movementBorder.transform.localScale.y;
            Vector2 spawnPoint = SelectRandomRectangleSpawnPoint(-xTo, xTo, -yTo, yTo);
            Instantiate(enemy, spawnPoint, Quaternion.identity, transform);//enemiesPool.transform

            enemiesAlive++;
        }
        public static Vector2 SelectRandomRectangleSpawnPoint(float xFrom, float xTo, float yFrom, float yTo)
        {
            Vector2 spawnPoint;
            spawnPoint.x = Random.Range(xFrom, xTo);
            spawnPoint.y = Random.Range(yFrom, yTo);
            return spawnPoint;
        }
    }
}