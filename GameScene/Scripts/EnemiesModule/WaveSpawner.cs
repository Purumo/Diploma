using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private const string PathResourcesCharacters = "Enemies";

        //private Object[] spritesFromResource;
        //private GameObject newEnemy;

        private int waveIndex = 0;

        public static float countdown;// = 3f;
        public static int roundsPassed;
        public static int EnemiesAlive;// = 0;

        public Wave[] waves;

        public float timeBetweenWaves = 5.4f;//??

        public Text waveCountdownText;
        public BoxCollider2D movementBorder;

        void Start()
        {
            //spritesFromResource = Resources.LoadAll(PathResourcesCharacters).ToArray();

            countdown = 3f;
            roundsPassed = 0;
            EnemiesAlive = 0;
        }
        void Update()
        {
            if (EnemiesAlive > 0)
            {
                return;
            }

            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
            countdown -= Time.deltaTime;

            waveCountdownText.text = Mathf.Round(countdown).ToString();//string.Format("{0:00.00}", countdown);???
        }

        IEnumerator SpawnWave()
        {
            Wave wave = waves[waveIndex];

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(0.5f);
            }

            roundsPassed++; //???
            waveIndex++;
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