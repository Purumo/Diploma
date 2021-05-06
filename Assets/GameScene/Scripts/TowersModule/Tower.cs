using GameScene.GameMaster.GameManager;
using UnityEngine;

namespace GameScene.TowersModule
{
    public class Tower : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            GameManager.GetInstance().FinishGame();
        }
    }
}

