using GameScene.EnemiesModule;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace GameScene.GameMaster
{
	public class GameOver : MonoBehaviour
	{
		public Text roundsText;
		void OnEnable()
		{
			roundsText.text = WaveSpawner.roundsPassed.ToString();
		}
		public void Retry()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		public void Menu()
		{
			SceneManager.LoadScene(MainMenu.mainMenu);
		}
	}
}