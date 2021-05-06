using GameScene.EnemiesModule;
using System;
using UnityEngine;

namespace GameScene.BulletsModule
{
    public class ClassicBullet : MonoBehaviour
    {
        private Vector2 dir;
        private Rigidbody2D rb;

        private Bullet bullet;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            bullet = new Bullet(BulletsController.GetInstance().currentBullet);
        }
        protected void FixedUpdate()
        {
            rb.AddForce(dir * bullet.speed * Time.fixedDeltaTime);
            Destroy(gameObject, bullet.lifeTime);
        }
        public void Seek(Vector2 direction, Bullet bullet)
        {
            dir = direction.normalized;
            this.bullet = new Bullet(bullet);
        }
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (bullet.actionRadius == 0)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(bullet.damage);
                enemy.ChangeSpeed(bullet.slowdownPercentage, bullet.slowActionTime);
            }
            else
            {
                Damage();
            }

            Destroy(gameObject);
        }
        void Damage()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,
                bullet.actionRadius, BulletsController.layerMaskEnemy);

            Enemy enemy;

            try
            {
                foreach (Collider2D collider in colliders)
                {
                    enemy = collider.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(bullet.damage);
                    enemy.ChangeSpeed(bullet.slowdownPercentage, bullet.slowActionTime);
                }
            }
            catch (Exception e)
            {
                Debug.Log("fensjfn");
            }
        }
    }
}