using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Settings;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	[HideInInspector] public int layerIdxTower;
	[HideInInspector] public int layerIdxEnemy;

	[HideInInspector] public bool GameIsOver;

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
	}
	public static GameManager GetInstance()
	{
		return instance;
	}
}
