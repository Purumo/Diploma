using GameScene.EnemiesModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class StandartBullet : MonoBehaviour, IMovableBullet
    {
        private Vector3 dir;
        private Rigidbody2D rb;

        private Bullet bullet;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            bullet = new Bullet(BulletsController.GetInstance().standartBullet);
        }
        protected void Update()
        {
            rb.AddForce(dir * bullet.Speed * Time.deltaTime);

            Destroy(gameObject, bullet.LifeTime);
        }
        public void Seek(Vector3 direction)
        {
            dir = direction.normalized;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            float rotX = dir.x != 0 ? dir.x * -90 : (dir.y + 1) * -90;
            Vector4 vec = new Vector4(rotX, 90, 45, 1);
            GameObject effectIns = Instantiate(bullet.ImpactEffect, transform.position,
                Quaternion.Euler(vec), transform.parent.GetChild(0));//?

            float lifeTime = bullet.ImpactEffect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
            Destroy(effectIns, lifeTime);

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(bullet.Damage);

            Destroy(gameObject);
        }
    }
}
