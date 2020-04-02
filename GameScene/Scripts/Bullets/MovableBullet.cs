using GameScene.EnemiesModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public class MovableBullet : MonoBehaviour//, IMovableBullet
    {
        private Vector3 dir;
        private Rigidbody2D rb;

        private Bullet bullet;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            bullet = new Bullet(BulletsController.GetInstance().currentBullet);
        }
        protected void Update()
        {
            rb.AddForce(dir * bullet.Speed * Time.deltaTime);
            Destroy(gameObject, bullet.LifeTime);
        }
        public void Seek(Vector3 direction, Bullet bullet)
        {
            dir = direction.normalized;
            this.bullet = new Bullet(bullet);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            //Quaternion.Euler(vec)
            //St
            //float rotX = dir.x != 0 ? dir.x * -90 : (dir.y + 1) * -90;
            //Vector4 vec = new Vector4(rotX, 90, 45, 1);
            //NonSt
            //float rotX = dir.x != 0 ? dir.x * -90 : (dir.y + 1) * -90;
            //Vector4 vec = new Vector4(rotX, 0, 0, 1);

            GameObject effectIns = Instantiate(bullet.ImpactEffect, transform.position,
                Quaternion.identity, BulletsController.GetInstance().effectsPool);

            float lifeTime = bullet.ImpactEffect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
            Destroy(effectIns, lifeTime);

            if(bullet.ExplosionRadius == 0)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(bullet.Damage);
            }
            else
            {
                Damage();
            }

            Destroy(gameObject);
        }
        void Damage()//Explode
        {
            //reqiures in refactoring
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,
                bullet.ExplosionRadius, BulletsController.layerMaskEnemy);
            Enemy enemy;
            foreach (Collider2D collider in colliders)
            {
                enemy = collider.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(bullet.Damage);
            }
        }
    }
}