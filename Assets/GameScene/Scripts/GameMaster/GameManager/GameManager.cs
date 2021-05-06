using UnityEngine;
using GameScene.GameMaster.UI;

namespace GameScene.GameMaster.GameManager
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;

		private int layerIdxTower;
		private int layerIdxEnemy;

		private bool GameIsOver;

		public GameObject gameOverUI;

		void Start()
		{
			instance = this;

			GameIsOver = false;

			layerIdxTower = LayerMask.NameToLayer("Tower");
			layerIdxEnemy = LayerMask.NameToLayer("Enemy");
			Physics2D.IgnoreLayerCollision(layerIdxTower, layerIdxEnemy, false);
		}
		void Update()
		{
			if (GameIsOver)
				return;
		}
		public static GameManager GetInstance()
		{
			return instance;
		}
		public void FinishGame()
		{
			GameIsOver = true;
			gameOverUI.SetActive(true);

			Physics2D.IgnoreLayerCollision(layerIdxTower, layerIdxEnemy, true);
		}
		void OnApplicationFocus(bool isFocused)
		{
			if (!isFocused && !GameIsOver)
				PauseMenu.GetInstance().Toggle();
		}
	}
}