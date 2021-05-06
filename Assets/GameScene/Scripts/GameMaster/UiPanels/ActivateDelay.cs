using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.GameMaster.UI
{
    public class ActivateDelay : MonoBehaviour
    {
        private float pauseEndTime = 0f;

        public Text countdownText;
        [Range(0, 10)] public float activateDelay = 3f;        

        void Update()
        {
            countdownText.text = Mathf.Round(pauseEndTime - Time.realtimeSinceStartup + 1).ToString();
        }
        void OnEnable()
        {
            StartCoroutine(Delay(activateDelay));
        }
        private IEnumerator Delay(float delayTime)
        {
            Time.timeScale = 0f;
            pauseEndTime = Time.realtimeSinceStartup + delayTime - 1;

            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }

            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}