using GameScene.EnemiesModule;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace GameScene.GameMaster
{
	public class GameOver : MonoBehaviour
	{		public Text pointsText;
		public Text roundsText;
		void OnEnable()
		{
			pointsText.text = PlayerStatistic.GetInstance().pointsScored.score.ToString();
			roundsText.text = PlayerStatistic.GetInstance().roundsPassed.score.ToString();

			DataSaver.GetInstance().TrySetHighScore(
				PlayerStatistic.GetInstance().pointsScored.score,
				PlayerStatistic.GetInstance().roundsPassed.score);
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