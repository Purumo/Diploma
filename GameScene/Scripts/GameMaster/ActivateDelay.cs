using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.GameMaster
{
    public class ActivateDelay : MonoBehaviour
    {
        private float pauseEndTime;
        //private float countdown;

        public Text countdownText;
        public float activateDelay = 3f;
        
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