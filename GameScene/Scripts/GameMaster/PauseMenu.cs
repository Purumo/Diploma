using UnityEngine;
using UnityEngine.SceneManagement;
namespace GameScene.GameMaster
{        public class PauseMenu : MonoBehaviour
    {
        private static PauseMenu instance;
        public GameObject ui;
        public GameObject delayUi;
        private void Start()
        {
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void Menu()        {
            SceneManager.LoadScene(MainMenu.mainMenu);
        }
        public static PauseMenu GetInstance()
        {
            return instance;
        }
    }
}