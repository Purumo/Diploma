using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameScene.GameMaster.Statistic;

namespace GameScene.EnemiesModule
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        [Range(0, 15)] public int count;
        [Range(0, 5)] public float rate;
    }
    public class WaveSpawner : MonoBehaviour
    {
        private int waveIndex = 0;

        public static int enemiesAlive;
        public static int currentWaveCount;

        private float nextWaveCountdown = 0;
        public Text nextWaveCountdownText;

        public BoxCollider2D movementBorder;

        [Range(0, 30)] public float timeBetweenWaves = 5f;
        public Wave[] waves;

        void Start()
        {
            enemiesAlive = 0;
            currentWaveCount = 0;
        }
        void Update()
        {
            if (enemiesAlive > 0)
            {
                return;                
            }
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
                nextWaveCountdownText.text = Mathf.Round(nextWaveCountdown).ToString();
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
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        void SpawnEnemy(GameObject enemy)
        {
            float xTo = (movementBorder.offset.x + movementBorder.size.x / 2) * movementBorder.transform.localScale.x;
            float yTo = (movementBorder.offset.y + movementBorder.size.y / 2) * movementBorder.transform.localScale.y;
            Vector2 spawnPoint = SelectRandomRectangleSpawnPoint(-xTo, xTo, -yTo, yTo);
            Instantiate(enemy, spawnPoint, Quaternion.identity, transform);

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