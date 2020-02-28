using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Settings;

public class Bullet : MonoBehaviour
{
    private Vector3 dir;
    private Rigidbody2D rb;

    public float speed = 10f;
    public GameObject impactEffect;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.AddForce(dir * speed * Time.deltaTime);

        Destroy(gameObject, 10f);//, 1.5 * screenSize / speed);
    }
    public void Seek(Vector3 direction)
    {
        dir = direction.normalized;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        float rotX;
        if (dir.x != 0)//horizontal
        {
            rotX = dir.x * -90;
        }
        else//vertical
        {
            rotX = (dir.y + 1) * -90;
        }
         * */
        float rotX = dir.x != 0 ? dir.x * -90 : (dir.y + 1) * -90;
        Vector4 vec = new Vector4(rotX, 90, 45, 1);
        GameObject effectIns = Instantiate(impactEffect, transform.position, 
            Quaternion.Euler(vec), transform.parent.GetChild(0));

        float lifeTime = impactEffect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
        Destroy(effectIns, lifeTime);
        Destroy(collision.gameObject);
    }
}
