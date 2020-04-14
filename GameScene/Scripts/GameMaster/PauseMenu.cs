using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene.GameMaster
{
    public class PauseMenu : MonoBehaviour
    {
        private static PauseMenu instance;

        public GameObject ui;
        public GameObject delayUi;

        private void Start()
        {
            //Time.timeScale = 1f;
            instance = this;
        }
        void Update()
        {
            //to delete on build
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Toggle();
            }
        }
        public void Toggle()
        {
            ui.SetActive(!ui.activeSelf);

            if (delayUi.activeSelf)
            {
                delayUi.SetActive(false);
            }

            if (ui.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                delayUi.SetActive(true);
            }
        }
        public void Retry()
        {
            //Toggle();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void Menu()
        {
            //Toggle();
            SceneManager.LoadScene(MainMenu.mainMenu);
        }
        public static PauseMenu GetInstance()
        {
            return instance;
        }
    }
}