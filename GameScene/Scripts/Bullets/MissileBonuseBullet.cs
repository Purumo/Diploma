using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class MissileBonuseBullet : MonoBehaviour, IMovableBullet
    {
        //варианты "поднятия" бонусов - через касание пальцем, через попадание пули
        //x видов бонусных "пуль" - ракета со взрывом, отталкивающая пуля
        //x видов бонусов
        //"хорошие" - замедление врагов,
        //"плохие" - 

        private Text countdownBonuseUI;

        protected const int layerMaskEnemy = 1 << 10;
        private float countdownActionTime;

        //need to set borders
        public float explosionRadius = 1f;

        protected void Damage(GameObject enemy)
        {
            Explode();
        }
        void Explode()
        {
            //reqiures in refactoring
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMaskEnemy);
            foreach (Collider2D collider in colliders)
            {
                Destroy(collider.gameObject);
            }
        }

        protected Vector3 dir;
        //protected static List<RaycastHit2D> shootsRaycast = new List<RaycastHit2D>();
        //protected ContactFilter2D raycastFilter;

        protected Rigidbody2D rb;
        protected float lifeTime = 10f;//, 1.5 * screenSize / speed);

        //public Transform effectsPool;

        public float speed = 50f;
        public GameObject impactEffect;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        protected void Update()
        {
            rb.AddForce(dir * speed * Time.deltaTime);

            if (countdownActionTime <= 0f)
            {
                BulletsController.GetInstance().ResetBullet();
            }
            else
            {
                countdownBonuseUI.text = Mathf.Round(countdownActionTime).ToString();
                //print(countdownActionTime);///
            }
            countdownActionTime -= Time.deltaTime;

            Destroy(gameObject, lifeTime);
        }
        public void Seek(Vector3 direction, Bullet bullet)
        {
            dir = direction.normalized;
            countdownBonuseUI = bullet.icon.GetComponentInChildren<Text>();
            countdownActionTime = bullet.actionTime;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            float rotX = dir.x != 0 ? dir.x * -90 : (dir.y + 1) * -90;
            Vector4 vec = new Vector4(rotX, 0, 0, 1);
            GameObject effectIns = Instantiate(impactEffect, transform.position,
                Quaternion.Euler(vec), transform.parent.GetChild(0));//public Transform effectsPool;

            float lifeTime = impactEffect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
            Destroy(effectIns, lifeTime);

            Damage(collision.gameObject);

            Destroy(gameObject);
        }
    }
}