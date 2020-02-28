using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameScene.EnemiesModule;

namespace GameScene.TurretsModule
{
    public class TurretsController : MonoBehaviour
    {
        private float countdown;

        [System.Serializable]
        public struct Turret
        {
            public Transform body;
            public Transform firePoint;
        }

        [Header("Attributes")]
        public float MoveTurretsSpeed = 3f;
        public float fireRate = 1f;

        [Header("Unity Setup Fields")]
        public GameObject bulletPrefab;
        public Transform bulletPool;

        [Header("Horizontal turrets Setup")]
        public GameObject turretsHorizontal;
        public Turret rightTurret, leftTurret;

        [Header("Vertical turrets Setup")]
        public GameObject turretsVertical;
        public Turret topTurret, botTurret;


        private TurretsState currentState;
        void Start()
        {
            countdown = WaveSpawner.countdown;

            SetState(new HorizontalState(this));
        }
        void Update()
        {
            if (countdown >= 0)
            {
                countdown -= Time.deltaTime;
                return;
            }
            else
            {
                currentState.Move();

                ShootAction();// for deleting
            }
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
