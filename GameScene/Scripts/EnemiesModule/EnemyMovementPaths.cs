using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            moveTrajectories = new int[4][];
            moveTrajectories[0] = new int[8] { 5, 4, 2, 3, 5, 3, 0, 10 };
            moveTrajectories[1] = new int[10] { 2, 9, 4, 2, 3, 5, 6, 5, 7, 13 };
            moveTrajectories[2] = new int[8] { 9, 2, 3, 2, 4, 9, 8, 12 };
            moveTrajectories[3] = new int[7] { 6, 5, 4, 9, 2, 1, 11 };

            movementPoints = new Transform[transformMovementPoints.childCount];
            for (int i = 0; i < movementPoints.Length; i++)
            {
                movementPoints[i] = transformMovementPoints.GetChild(i);
            }
        }
    }
}
