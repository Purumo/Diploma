using GameScene.EnemiesModule;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Examples;
//using BayatGames.SaveGameFree.Examples;

namespace GameScene.GameMaster
{
	public class DataSaver : MonoBehaviour
	{
		[System.Serializable]
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

		private static DataSaver instance;

		private HighScore highScore = new HighScore();
		//private string encodePassword = "Don'tLetGrabPassword";

		public string identifier = "Don'tLetGrabDataSaver";

		public Text highScoreText;
		public Text roundsText;

		void Start()
		{
			instance = this;

			//SaveGame.EncodePassword = encodePassword;
			//SaveGame.Encode = true;
			//SaveGame.Serializer = 
			//	new BayatGames.SaveGameFree.Serializers.SaveGameBinarySerializer();
			Load();
		}
		private void Load()
		{
			if (SaveGame.Exists(identifier))
			{
				highScore = SaveGame.Load<HighScore>(identifier, new HighScore());
					//, SerializerDropdown.Singleton.ActiveSerializer);
				//, highScore);//, SerializerDropdown.Singleton.ActiveSerializer);
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
					//, SerializerDropdown.Singleton.ActiveSerializer);//xtdfycuvgyibhonjkml

				//highScoreText.text = highScore.score.ToString();
				//roundsText.text = highScore.rounds.ToString();
			}
		}
		public static DataSaver GetInstance()
		{
			return instance;
		}

		public void DebugClearStatistic()
		{
			SaveGame.Clear();
			highScore.score = 0;
			highScore.rounds = 0;
			highScoreText.text = "0";
			roundsText.text = "0";
		}
	}
}