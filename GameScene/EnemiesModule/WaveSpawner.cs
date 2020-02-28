using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace GameScene.EnemiesModule
{
    public class WaveSpawner : MonoBehaviour
    {
        private const string PathResourcesCharacters = "Enemies";

        private Object[] spritesFromResource;
        private GameObject newEnemy;

        private Vector2 spawnPoint;

        private int waveIndex = 0;

        [HideInInspector]
        public static float countdown = 3f;

        public float timeBetweenWaves = 5.4f;//??

        public Text waveCountdownText;
        public BoxCollider2D movementBorder;

        void Start()
        {
            spritesFromResource = Resources.LoadAll(PathResourcesCharacters).ToArray();
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
            newEnemy = Instantiate(newEnemy, spawnPoint, Quaternion.identity, gameObject.transform);//enemiesPool.transform
        }
        Vector2 SelectRandomSpawnPoint()
        {
            Vector2 spawnPoint;
            float xFrom, xTo, yFrom, yTo;
            xTo = (movementBorder.offset.x + movementBorder.size.x / 2) * movementBorder.transform.localScale.x;
            yTo = (movementBorder.offset.y + movementBorder.size.y / 2) * movementBorder.transform.localScale.y;
            xFrom = -xTo;
            yFrom = -yTo;
            spawnPoint.x = Random.Range(xFrom, xTo);
            spawnPoint.y = Random.Range(yFrom, yTo);
            return spawnPoint;
        }
    }
}