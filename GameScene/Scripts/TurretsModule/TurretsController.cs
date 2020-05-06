using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.TurretsModule
{
    [System.Serializable]
    public struct Turret
    {
        public GameObject gameObject;
        public Transform firePoint;
        public Text coundownText;
    }

    public class TurretsController : MonoBehaviour
    {
        private static TurretsController instance;
        private TurretsState currentState;

        private float countdownReloadTime = 0f;

        [HideInInspector] public float varSpeed;

        [Header("Attributes")]
        public float moveTurretsSpeed = 0f;
        public float reloadTime = 0f;
        public float showReloadTime = 0f;

        [Header("Horizontal turrets Setup")]
        public Rigidbody2D turretsHorizontal;
        public CanvasGroup horizCanvasGroup;
        public Turret rightTurret, leftTurret;

        [Header("Vertical turrets Setup")]
        public Rigidbody2D turretsVertical;
        public CanvasGroup verticalCanvasGroup;
        public Turret topTurret, botTurret;

        void Start()
        {
            instance = this;

            varSpeed = moveTurretsSpeed;

            SetState(new HorizontalState(this));
        }
        void Update()
        {
            if (countdownReloadTime > 0)
            {
                StartCoroutine(TurretsRecharge());
            }

            countdownReloadTime -= Time.deltaTime;
        }
        public void ShootAction()
        {
            if (countdownReloadTime <= 0)//1) прошло кд -> игрок может стрелять
            {
                currentState.Shoot();

                countdownReloadTime = reloadTime;//2) "перезаряжаем"
            }
        }
        private IEnumerator TurretsRecharge()
        {
            yield return new WaitForSeconds(showReloadTime);
        }
        void FixedUpdate()
        {
            currentState.Move();
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
        public void SwitchAction()
        {
            currentState.Sleep();
            currentState.Switching();
        }
    }
}
