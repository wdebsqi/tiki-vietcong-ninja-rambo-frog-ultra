using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerController : MonoBehaviour
{
    public BoxCollider2D triggerCollider;
    public Rigidbody2D playerRb;
    public Rigidbody2D enemyRb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D whatHit)
    {
        PlayerController player = whatHit.GetComponent<PlayerController>();

        if (player != null)
        {
            Debug.Log("Player hit!");
            player.TakeDamage(1);
        }
    }
}
