using UnityEngine;
using GameScene.GameMaster;
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
	void OnApplicationFocus(bool isFocused)
	{
		if (!isFocused)
			PauseMenu.GetInstance().Toggle();
	}
}
