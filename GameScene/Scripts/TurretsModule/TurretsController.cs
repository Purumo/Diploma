using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameScene.EnemiesModule;

namespace GameScene.TurretsModule
{
    [System.Serializable]
    public struct Turret
    {
        public GameObject gameObject;
        public Transform firePoint;
    }

    public class TurretsController : MonoBehaviour
    {
        private static TurretsController instance;

        //private float countdown;

        //[Header("Unity Setup Fields")]
        //public Transform bulletPool;
        //public GameObject bulletPrefab;

        [HideInInspector] public float varSpeed;

        [Header("Attributes")]
        public float MoveTurretsSpeed = 3f;

        [Header("Horizontal turrets Setup")]
        public Rigidbody2D turretsHorizontal;
        public CanvasGroup horizCanvasGroup;
        public Turret rightTurret, leftTurret;

        [Header("Vertical turrets Setup")]
        public Rigidbody2D turretsVertical;
        public CanvasGroup verticalCanvasGroup;
        public Turret topTurret, botTurret;


        private TurretsState currentState;
        void Start()
        {
            instance = this;

            varSpeed = MoveTurretsSpeed;

            //countdown = WaveSpawner.countdown;

            SetState(new HorizontalState(this));
        }
        void FixedUpdate()
        {
            //if (countdown > 0)
            //{
            //    countdown -= Time.deltaTime;
            //    return;
            //}
            //else
            //{
                currentState.Move();
            //}
        }

        public static TurretsController GetInstance()
        {
            return instance;
        }
        public void SetState(TurretsState state)
        {
            currentState = state;
        }
        public void NoneAction() => currentState.Sleep();
        public void UpAction() => currentState.Up();
        public void DownAction() => currentState.Down();
        public void LeftAction() => currentState.Left();
        public void RightAction() => currentState.Right();
        public void ShootAction() => currentState.Shoot();
        public void SwitchAction()
        {
            currentState.Sleep();
            currentState.Switching();
        }
    }
}
