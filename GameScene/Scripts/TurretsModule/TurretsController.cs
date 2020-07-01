using System.Collections;
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
        [Range(0, 10)] public float moveTurretsSpeed = 0f;
        [Range(0, 5)] public float reloadTime = 0f;

        [Header("Horizontal turrets Setup")]
        public Rigidbody2D rigidbodyTurretsHorizontal;
        public CanvasGroup horizButtonsCanvasGroup;
        public Turret rightTurret, leftTurret;

        [Header("Vertical turrets Setup")]
        public Rigidbody2D rigidbodyTurretsVertical;
        public CanvasGroup verticalButtonsCanvasGroup;
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
            if (countdownReloadTime <= 0)
            {
                currentState.Shoot();
                countdownReloadTime = reloadTime;
            }
        }
        private IEnumerator TurretsRecharge()
        {
            yield return new WaitForSeconds(reloadTime);
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
