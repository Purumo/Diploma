using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Settings;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	private int layerIdxTower;
	private int layerIdxEnemy;

	private bool GameIsOver;

	public GameObject gameOverUI;

	//IsPointerOverGameObject - code note to avoid click over others objects
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

		if (!Application.isFocused)
		{
			PauseMenu.GetInstance().Toggle();
		}
	}
	public static GameManager GetInstance()
	{
		return instance;
	}
	public void FinishGame()
	{
		//uncomment to have a GameOver
		//GameIsOver = true;
		//gameOverUI.SetActive(true);

		Physics2D.IgnoreLayerCollision(
			layerIdxTower, layerIdxEnemy, true);
	}

	//void OnAplicationFocus(bool hasFocus)
	//{
	//	//if (!hasFocus)
	//	//{
	//	PauseMenu.GetInstance().Toggle();
	//	//}
	//}
	//void OnApplicationPause(bool isPaused)
	//{
	//	PauseMenu.GetInstance().Toggle();
	//}
}
