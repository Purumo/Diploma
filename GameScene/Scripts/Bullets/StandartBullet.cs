using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BulletsModule
{
    public interface IMovableBullet
    {
        void Seek(Vector3 direction, Bullet bullet);
    }
    public class StandartBullet : MonoBehaviour, IMovableBullet
    {
        protected void Damage(GameObject enemy)
        {
            Destroy(enemy);
        }

        protected Vector3 dir;
        protected Rigidbody2D rb;
        protected float lifeTime = 10f;

        public float speed = 50f;
        public GameObject impactEffect;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        protected void Update()
        {
            rb.AddForce(dir * speed * Time.deltaTime);

            Destroy(gameObject, lifeTime);
        }
        public void Seek(Vector3 direction, Bullet bullet)
        {
            dir = direction.normalized;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            float rotX = dir.x != 0 ? dir.x * -90 : (dir.y + 1) * -90;
            Vector4 vec = new Vector4(rotX, 90, 45, 1);
            GameObject effectIns = Instantiate(impactEffect, transform.position,
                Quaternion.Euler(vec), transform.parent.GetChild(0));//?

            float lifeTime = impactEffect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
            Destroy(effectIns, lifeTime);

            Damage(collision.gameObject);

            Destroy(gameObject);
        }
    }
}
