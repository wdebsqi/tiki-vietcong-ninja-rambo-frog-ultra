using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region References
    public Rigidbody2D bulletRb;
    #endregion

    #region Bullet settings variables
    [Header("Bullet settings")]
    public float speed = 20f;
    public int damage = 1;
    #endregion


    // Start is called before the first frame update
    void Start()
    {

        bulletRb.velocity = transform.right * speed;
    }
    // Function - Health - deals damage on trigger
    #region OnTriggerEnter2D(Collider2D whatHit)
    void OnTriggerEnter2D(Collider2D whatHit)
    {
        EnemyHealthManager enemy = whatHit.GetComponent<EnemyHealthManager>();

        // check if hit an enemy
        if (enemy != null)
        {
            Debug.Log("Enemy hit!");
            if(enemy.isDead == false)
            {
                enemy.TakeDamage(damage);
            }
        }
        // destroy bullet
        Destroy(gameObject);
    }
    #endregion
}
