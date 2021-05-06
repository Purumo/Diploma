using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

namespace GameScene.GameMaster.DataSaverModule
{
	public class HighScore
	{
		public int rounds;
		public int score;

		public HighScore()
		{
			rounds = 0;
			score = 0;
		}
	}
	public class DataSaver : MonoBehaviour
	{
		private static DataSaver instance;
		private HighScore highScore = new HighScore();

		[SerializeField] private string identifier = "Don'tLetGrabDataSaver";

		public Text highScoreText;
		public Text roundsText;

		void Start()
		{
			instance = this;
			Load();
		}
		private void Load()
		{
			if (SaveGame.Exists(identifier))
			{
				highScore = SaveGame.Load<HighScore>(identifier, new HighScore());
			}
			else
			{
				highScore.score = 0;
				highScore.rounds = 0;
			}
			highScoreText.text = highScore.score.ToString();
			roundsText.text = highScore.rounds.ToString();
		}
		public void TrySetHighScore(int newHighScore, int newRounds)
		{
			if(newHighScore > highScore.score)
			{
				highScore.score = newHighScore;
				highScore.rounds = newRounds;

				SaveGame.Save<HighScore>(identifier, highScore);
			}
		}
		public static DataSaver GetInstance()
		{
			return instance;
		}
	}
}