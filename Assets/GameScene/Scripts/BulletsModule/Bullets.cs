using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public interface IBonuseController
    {
        void Activate();
    }
    [System.Serializable]
    public class Bullet
    {
        [HideInInspector] public int bonusPanelIndex;

        [HideInInspector] public Text countdownText;
        public string name;
        public GameObject gameObj;

        [Range(0, 10)] public float actionRadius;
        [Range(0, 500)] public float speed;
        [Range(0, 100)] public int damage;
        [Range(0, 60)] public float lifeTime;

        [Range(0, 100)] public float slowdownPercentage;
        [Range(0, 120)] public float slowActionTime;

        public GameObject sprite;
        public GameObject icon;

        public Bullet(Bullet bullet)
        {
            name = bullet.name;
            gameObj = bullet.gameObj;
            speed = bullet.speed;
            damage = bullet.damage;

            actionRadius = bullet.actionRadius;

            slowdownPercentage = bullet.slowdownPercentage;

            slowActionTime = bullet.slowActionTime;

            countdownText = null;
            lifeTime = bullet.lifeTime;

            sprite = bullet.sprite;

            icon = bullet.icon;
        }
    }
}
