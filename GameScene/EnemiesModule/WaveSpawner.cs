using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace GameScene.EnemiesModule
{
    public class WaveSpawner : MonoBehaviour
    {
        private const string
            PathResourcesCharacters = "Enemies",
            PathSceneEnemiesPool = "EnemiesPool",
            PathSceneMovementBorder = "Movement/MovementBorder";

        private Object[] spritesFromResource;
        private GameObject newEnemy;

        private GameObject enemiesPool;
        private Vector2 spawnPoint;

        private int waveIndex = 0;

        private float countdown = 3f;
        public float timeBetweenWaves = 5.6f;//??

        public Text waveCountdownText;

        void Start()
        {
            spritesFromResource = Resources.LoadAll(PathResourcesCharacters).ToArray();
            enemiesPool= GameObject.Find(PathSceneEnemiesPool);

        }
        void Update()
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;

            waveCountdownText.text = Mathf.Round(countdown).ToString();
        }

        IEnumerator SpawnWave()
        {
            waveIndex = Random.Range(1, 3);

            for (int i = 0; i < waveIndex; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }
        }
        void SpawnEnemy()
        {
            //необходимо выбрать точку из movementPoints с индексом первого элемента случайно выбранной строки (траектории)
            //вдобавок по этой выбранной траектории и пойдёт новый спрайт
            newEnemy = (GameObject)spritesFromResource[Random.Range(0, spritesFromResource.Length)];

            spawnPoint = SelectRandomSpawnPoint();
            newEnemy = Instantiate(newEnemy, spawnPoint, Quaternion.identity, enemiesPool.transform);
        }
        Vector2 SelectRandomSpawnPoint()
        {
            Vector2 spawnPoint;
            GameObject movementBorder = GameObject.Find(PathSceneMovementBorder);
            BoxCollider2D bc = movementBorder.GetComponent<BoxCollider2D>();
            float xFrom, xTo, yFrom, yTo;
            xTo = (bc.offset.x + bc.size.x / 2) * bc.transform.localScale.x;
            yTo = (bc.offset.y + bc.size.y / 2) * bc.transform.localScale.y;
            xFrom = -xTo;
            yFrom = -yTo;
            spawnPoint.x = Random.Range(xFrom, xTo);
            spawnPoint.y = Random.Range(yFrom, yTo);
            return spawnPoint;
        }
    }
}