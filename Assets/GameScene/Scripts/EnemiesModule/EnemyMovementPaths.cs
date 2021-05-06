using UnityEngine;

namespace GameScene.EnemiesModule
{
    public class EnemyMovementPaths : MonoBehaviour
    {
        public static int[][] moveTrajectories;
        public static Transform[] movementPoints;

        public Transform transformMovementPoints;

        void Awake()
        {
            moveTrajectories = new int[6][];
            moveTrajectories[0] = new int[8] { 16, 13, 18, 9, 19, 11, 10, 3};
            moveTrajectories[1] = new int[8] { 12, 4, 6, 8, 4, 10, 20, 2};
            moveTrajectories[2] = new int[8] { 14, 18, 7, 8, 15, 20, 17, 3};
            moveTrajectories[3] = new int[7] { 19, 16, 11, 8, 4, 6, 0};
            moveTrajectories[4] = new int[8] { 7, 8, 16, 19, 16, 14, 13, 2};
            moveTrajectories[5] = new int[8] { 5, 9, 4, 10, 13, 18, 6, 1};

            movementPoints = new Transform[transformMovementPoints.childCount];
            for (int i = 0; i < movementPoints.Length; i++)
            {
                movementPoints[i] = transformMovementPoints.GetChild(i);
            }
        }
    }
}
