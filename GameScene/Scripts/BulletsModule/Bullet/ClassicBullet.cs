using GameScene.EnemiesModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using GameScene.Effects;

namespace GameScene.BulletsModule
{
    public class ClassicBullet : MonoBehaviour, IMovableBullet
    {
        private Vector3 dir;
        private Rigidbody2D rb;

        private Bullet bullet;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            bullet = new Bullet(BulletsController.GetInstance().currentBullet);
        }
        protected void FixedUpdate()
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
            #region Code node for effect rotate
            /*
            quaternion.euler(vec)
            st
            float rotx = dir.x != 0 ? dir.x * -90 : (dir.y + 1) * -90;
            vector4 vec = new vector4(rotx, 90, 45, 1);
            nonst
            float rotx = dir.x != 0 ? dir.x * -90 : (dir.y + 1) * -90;
            vector4 vec = new vector4(rotx, 0, 0, 1);
            */
            #endregion

            //GameObject effectIns = Instantiate(bullet.ImpactEffect, transform.position,
            //    Quaternion.identity, BulletsController.GetInstance().effectsPool);

            //float lifeTime = bullet.ImpactEffect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
            //Destroy(effectIns, lifeTime);

            if (bullet.ExplosionRadius <= 0)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(bullet.Damage);
                enemy.Slow(bullet.SlowEnemyAmount, bullet.SlowActionTime);
            }
            else
            {
                //paste circle effect here
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
                enemy.Slow(bullet.SlowEnemyAmount, bullet.SlowActionTime);
            }
        }
    }
}