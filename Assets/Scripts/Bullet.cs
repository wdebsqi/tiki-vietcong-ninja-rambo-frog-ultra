using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D whatHit)
    {
        EnemyController enemy = whatHit.GetComponent<EnemyController>();

        // check if hit an enemy
        if (enemy != null)
        {
            Debug.Log("Enemy hit!");
            enemy.TakeDamage(damage);
        }
        // destroy bullet
        Destroy(gameObject);
    }
}
