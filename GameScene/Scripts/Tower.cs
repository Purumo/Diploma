using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        GameManager.GetInstance().GameIsOver = true;
        GameManager.GetInstance().gameOverUI.SetActive(true);
        */

        Physics2D.IgnoreLayerCollision(
            GameManager.GetInstance().layerIdxTower, GameManager.GetInstance().layerIdxEnemy, true);
    }
}
