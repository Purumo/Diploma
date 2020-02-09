using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameScene.Helpers;

namespace GameScene.EnemiesModule
{
    public class EnemyMovement : MonoBehaviour
    {
        private const string PathSceneMovementPoints = "Movement/MovementPoints";

        public static int[][] moveTrajectories;
        
        public static Transform[] movementPoints;

        void Awake()
        {
            moveTrajectories = new int[4][];
            moveTrajectories[0] = new int[8] { 5, 4, 2, 3, 5, 3, 0, 10 };
            moveTrajectories[1] = new int[10] { 2, 9, 4, 2, 3, 5, 6, 5, 7, 13 };
            moveTrajectories[2] = new int[8] { 9, 2, 3, 2, 4, 9, 8, 12 };
            moveTrajectories[3] = new int[7] { 6, 5, 4, 9, 2, 1, 11 };

            Transform trMovementPoints = BasicUnityFunctions.GetTransform(PathSceneMovementPoints);
            movementPoints = new Transform[trMovementPoints.childCount];
            for(int i=0; i< movementPoints.Length; i++)
            {
                movementPoints[i] = trMovementPoints.GetChild(i);
            }
        }
    }
}
