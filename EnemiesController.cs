using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets;

public class EnemiesController : MonoBehaviour
{
    private const string
           PathResourcesCharacters = "Enemies",
           PathSceneParentEnemies = "EnemiesPool",
           PathSceneBorderMovementTriggerRightTop = "MovementBorder/TriggerRT",
           PathSceneBorderMovementTriggerRightBot = "MovementBorder/TriggerRB",
           PathSceneBorderMovementTriggerLeftTop = "MovementBorder/TriggerLT",
           PathSceneBorderMovementTriggerLeftBot = "MovementBorder/TriggerLB"; 

    //equals randomX & randomY
    private Vector2 randomPoint = new Vector2();

    public Vector2 walkingChangeX = new Vector2();
    public Vector2 walkingChangeY = new Vector2();

    //Мы используем float tChange для следующего изменения после текущего кадра.
    private float tChange = 0;
    //С помощью Vector2 timeChange мы определили временной диапазон. 
    public Vector2 timeChange = new Vector2(0.5f, 1.5f);
    //С этим значением мы получим случайное время и установим tChange на это значение.
    
    public float moveSpeed = 1f;
    //public Vector2 randomSpeedChange = new Vector2(2f, 2f);

    public float moveThreshold = 0.5f;

    //Vector3 newSpawnPoint = new Vector3();

    /*class Square: Point
    {
        private Point[] points = new Point[4];
        public Square()
        {
            points[0] = points[1] = points[2] = points[3] = new Point(0, 0);
        }
        //создаёт квадрат в точке startingPoint, 
        //которая находится в позиции PointPosition
        //а на основе size вычисляются остальные 3 точки квадрата
        public Square(Point startingPoint, PointPosition pointPosition, float size)
        {
            points[(int) pointPosition] = startingPoint;
            int xHighBit = (int) pointPosition >> 1;
            int yLowBit = (int) pointPosition << 1;
            points[Mathf.Abs((int)pointPosition - 3)].X
                = points[Mathf.Abs((int)pointPosition - 1)].X
                = startingPoint.X + xHighBit * size;
            points[Mathf.Abs((int)pointPosition - 3)].Y
                = points[Mathf.Abs((int)pointPosition - 2)].Y
                = startingPoint.Y + yLowBit * size;
            points[Mathf.Abs((int)pointPosition - 2)].X = startingPoint.X;
            points[Mathf.Abs((int)pointPosition - 1)].Y = startingPoint.Y;
        }
        public enum PointPosition
        {
            TopRight = 0,
            BotRight = 1,
            TopLeft = 2,
            BotLeft = 3
        }
    }*/


    Object[] spritesFromResource;
    GameObject newSprite;
    GameObject enemiesPool;
    Rigidbody2D rbNewSprite;
    void Start()
    {
        EventTriggerAdder.addTrigger(PathSceneBorderMovementTriggerRightTop, UnityEngine.EventSystems.EventTriggerType.PointerEnter, (data) => { vfere});

        spritesFromResource = Resources.LoadAll(PathResourcesCharacters).ToArray();

        GameObject triggerRT = GameObject.Find(PathSceneBorderMovementTriggerRightTop);
        BoxCollider2D bc = triggerRT.GetComponent<BoxCollider2D>();
        walkingChangeX.y = (bc.offset.x + bc.size.x / 2) * bc.transform.localScale.x;
        walkingChangeY.y = (bc.offset.y + bc.size.y / 2) * bc.transform.localScale.y;
        walkingChangeX.x = -walkingChangeX.y;
        walkingChangeY.x = -walkingChangeY.y;

        newSprite = (GameObject) spritesFromResource[Random.Range(0, spritesFromResource.Length)];
        randomPoint.x = Random.Range(walkingChangeX.x, walkingChangeX.y);
        randomPoint.y = Random.Range(walkingChangeY.x, walkingChangeY.y);
        enemiesPool = GameObject.Find(PathSceneParentEnemies);
        newSprite = Instantiate(newSprite, randomPoint, Quaternion.identity, enemiesPool.transform);

        rbNewSprite = newSprite.GetComponent<Rigidbody2D>();
    }
    Vector2 direction = new Vector2();
    void Update()
    {
        if (Time.time >= tChange)
        {
            randomPoint.x = Random.Range(walkingChangeX.x, walkingChangeX.y);
            randomPoint.y = Random.Range(walkingChangeY.x, walkingChangeY.y);
            // set a random interval between minTimeChange and maxTimeChange in seconds   
            tChange = Time.time + Random.Range(timeChange.x, timeChange.y);
        }
        if (randomPoint.x > moveThreshold)
            direction.x = 1;
        if (randomPoint.x < moveThreshold)
            direction.x = -1;
        if (randomPoint.y > moveThreshold)
            direction.y = 1;
        if (randomPoint.y < moveThreshold)
            direction.y = -1;
    }
    private void FixedUpdate()
    {
        rbNewSprite.velocity = direction * moveSpeed;
        rbNewSprite.transform.position = new Vector3(
            Mathf.Clamp(
                rbNewSprite.transform.position.x,
                walkingChangeX.x,
                walkingChangeX.y),
            Mathf.Clamp(
                rbNewSprite.transform.position.y,
                walkingChangeY.x,
                walkingChangeY.y),
            enemiesPool.transform.position.z);
    }
}
